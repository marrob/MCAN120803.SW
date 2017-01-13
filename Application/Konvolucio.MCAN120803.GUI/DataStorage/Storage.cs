

namespace Konvolucio.MCAN120803.GUI.DataStorage
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using Common;
    using Properties;
    using WinForms.Framework;
    using AppModules.Filter.Model;
    using AppModules.Tools.Model;

    public partial class Storage
    {
        public event EventHandler Loading;
        public event EventHandler LoadCompleted;
        public event EventHandler Saving;
        public event EventHandler SaveCompleted;
        public event ProgressChangedEventHandler ProgressChange;
        public event EventHandler<StorageChanegdEventArgs> ContentChanged;

        public ProjectParameters Parameters { get; }

        public MessageFilterCollection Filters { get; }

        public CustomArbIdColumnCollection CustomArbIdColumns { get; }

        public ColumnLayoutCollection TraceGridLayout { get; }

        public ColumnLayoutCollection LogGridLayout { get; }

        public ColumnLayoutCollection StatisticsGridLayout { get; }

        public ColumnLayoutCollection FilterGridLayout { get; }

        public ToolTableCollection Tools { get; }

        public List<ColumnLayoutCollection> TableLayouts
        {
            get { return _fileStorage.ToolsLayouts; }
        }

        /// <summary>
        /// The file is already Saved.
        /// </summary>
        public bool IsSaved { get; private set; }
        /// <summary>
        /// Fils is changed.
        /// </summary>
        public bool IsChanged { get; private set; }
        /// <summary>
        /// Current Files name
        /// </summary>
        public string FileName { get; private set; }
        public string FullPath { get; private set; } = string.Empty;
        public string Loaction => System.IO.Path.GetDirectoryName(FullPath);

        private readonly FileStorage _fileStorage;

        /// <summary>
        /// Konstruktor
        /// </summary>
        public Storage(ProjectParameters parameters, MessageFilterCollection filters, CustomArbIdColumnCollection customArbIdColumns)
        {
            FileName = AppConstants.NewFileName;
            _fileStorage = new FileStorage();
            Parameters = parameters;
            Filters = filters;
            CustomArbIdColumns = customArbIdColumns;
            
            TraceGridLayout = new ColumnLayoutCollection();
            LogGridLayout = new ColumnLayoutCollection();   
            StatisticsGridLayout = new ColumnLayoutCollection();
            FilterGridLayout = new ColumnLayoutCollection();
            Tools = new ToolTableCollection();

            Parameters.PropertyChanged += new PropertyChangedEventHandler(Parameters_PropertyChanged);
            Filters.ListChanged += new ListChangedEventHandler(Filters_ListChanged);
            CustomArbIdColumns.ListChanged += new ListChangedEventHandler(CustomArbIdColumns_ListChanged);
            Tools.ListChanged += Tables_ListChanged;
            Tools.TableChanged += Tables_TableChanged;

            IsSaved = false;
            IsChanged = false;
        }

        /// <summary>
        /// Paramters Changed
        /// </summary>
        private void Parameters_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var obj = typeof(ProjectParameters).GetProperty(e.PropertyName).GetValue(Parameters, null);
            var str = "Change: Parameters, Property: " + e.PropertyName + ", NewValue: " + obj;
            IsChanged = true;
            var propertyDescriptor = TypeDescriptor.GetProperties(sender)[e.PropertyName];

            OnContentChanged(new StorageChanegdEventArgs(DataObjects.ParameterProperty, sender, propertyDescriptor));
        }

        /// <summary>
        /// Filters Changed.
        /// </summary>
        private void Filters_ListChanged(object sender, ListChangedEventArgs e)
        {
            IsChanged = true;

            var str = "ListChanged: FilterList, ";
            str += "Type: " + e.ListChangedType.ToString() + ", "; 
            switch (e.ListChangedType)
            {
                case ListChangedType.ItemChanged:
                    {
                        str += "Property: " + e.PropertyDescriptor.DisplayName + ", ";       
                        var bindingList = sender as IBindingList;
                        if (bindingList != null)
                        {
                            var valuet = e.PropertyDescriptor.GetValue(bindingList[e.NewIndex]);
                            if (valuet == null)
                                str += "NewValue: null";
                            else
                                str += "NewValue: " + valuet.ToString();
                        }
                        break;
                    }
            }
            OnContentChanged(new StorageChanegdEventArgs(DataObjects.FilterList, sender, e));
        }

        /// <summary>
        /// CustomArbIdColumnList Changed
        /// </summary>
        private void CustomArbIdColumns_ListChanged(object sender, ListChangedEventArgs e)
        {
            IsChanged = true;

            var str = "ListChanged: CustomArbIdColumnList, ";
            str += "Type: " + e.ListChangedType.ToString() + ", ";

            switch (e.ListChangedType)
            {
                case ListChangedType.ItemChanged:
                    {
                        str += "Property: " + e.PropertyDescriptor.DisplayName + ", ";
                        var bindingList = sender as IBindingList;
                        if (bindingList != null)
                        {
                            var valuet = e.PropertyDescriptor.GetValue(bindingList[e.NewIndex]);
                            if (valuet == null)
                                str += "NewValue: null";
                            else
                                str += "NewValue: " + valuet.ToString();
                        }
                        break;
                    }
            }
            OnContentChanged(new StorageChanegdEventArgs(DataObjects.CustomArbIdColumnList, sender, e));
        }

        /// <summary>
        /// Tables_ListChanged
        /// </summary>
        private void Tables_ListChanged(object sender, ListChangedEventArgs e)
        {
            IsChanged = true;

            var str = "ListChanged: Tables, ";
            str += "Type: " + e.ListChangedType.ToString() + ", ";

            switch (e.ListChangedType)
            {
                case ListChangedType.ItemChanged:
                    {
                        str += "Property: " + e.PropertyDescriptor.DisplayName + ", ";
                        var bindingList = sender as IBindingList;
                        if (bindingList != null)
                        {
                            var valuet = e.PropertyDescriptor.GetValue(bindingList[e.NewIndex]);
                            if (valuet == null)
                                str += "NewValue: null";
                            else
                                str += "NewValue: " + valuet.ToString();
                        }
                        break;
                    }
            }
            OnContentChanged(new StorageChanegdEventArgs(DataObjects.Tables, sender, e));
        }

        /// <summary>
        /// Tables_ListChanged
        /// </summary>
        private void Tables_TableChanged(object sender, ListChangedEventArgs e)
        {
            IsChanged = true;

            var str = "ListChanged: Table, ";
            str += "Type: " + e.ListChangedType.ToString() + ", ";

            switch (e.ListChangedType)
            {
                case ListChangedType.ItemChanged:
                    {
                        str += "Property: " + e.PropertyDescriptor.DisplayName + ", ";
                        var bindingList = sender as IBindingList;
                        if (bindingList != null)
                        {
                            var valuet = e.PropertyDescriptor.GetValue(bindingList[e.NewIndex]);
                            if (valuet == null)
                                str += "NewValue: null";
                            else
                                str += "NewValue: " + valuet.ToString();
                        }
                        break;
                    }
            }
            OnContentChanged(new StorageChanegdEventArgs(DataObjects.Table, sender, e));
        }

        /// <summary>
        /// Új project
        /// </summary>
        public void New(string deviceName, string badurate)
        {
            OnLoading();

            if (System.IO.Directory.Exists(Settings.Default.BrowseLocation))
                Environment.CurrentDirectory = Settings.Default.BrowseLocation;

            FileName = AppConstants.NewFileName;
            FullPath = Environment.CurrentDirectory + "\\" + FileName + AppConstants.FileExtension;
            IsSaved = false;

            _fileStorage.New();

            _fileStorage.Parameters.DeviceName = deviceName;
            _fileStorage.Parameters.Baudrate = badurate;
            _fileStorage.Parameters.CopyTo(Parameters);
            _fileStorage.Filters.CopyTo(Filters);
            _fileStorage.Tools.CopyTo(Tools);

            CustomArbIdColumns.Clear();
            LogGridLayout.Clear();
            TraceGridLayout.Clear();

            IsChanged = false;
            OnLoadCompleted();
        }

        /// <summary>
        /// Mentsd az aktuális projectet
        /// </summary>
        public void Save()
        {
            SaveAs(FullPath);
        }

        /// <summary>
        /// Mentsd másként az aktuális projectet
        /// </summary>
        public void SaveAs(string path)
        {

            OnSaving();

            if (System.IO.Path.IsPathRooted(path))
            {
                FullPath = path;
                FileName = System.IO.Path.GetFileNameWithoutExtension(path);
                Environment.CurrentDirectory = System.IO.Path.GetDirectoryName(path);
            }
            else
            {
                FullPath = path;
                FileName = path;
            }
            OnProgressChanged(new ProgressChangedEventArgs(30, "Saving..."));

            Parameters.CopyTo(_fileStorage.Parameters);
            OnProgressChanged(new ProgressChangedEventArgs(40, "Parameters Saving..."));

            Filters.CopyTo(_fileStorage.Filters);
            OnProgressChanged(new ProgressChangedEventArgs(60, "Filters Saving..."));

            CustomArbIdColumns.CopyTo(_fileStorage.CustomArbIdColumns);
            OnProgressChanged(new ProgressChangedEventArgs(80, "CustomArbIdColumns Saving..."));

            TraceGridLayout.CopyTo(_fileStorage.TraceGridLayout);
            OnProgressChanged(new ProgressChangedEventArgs(80, "TraceGridLayout Saving..."));

            LogGridLayout.CopyTo(_fileStorage.LogGridLayout);
            OnProgressChanged(new ProgressChangedEventArgs(80, "LogGridLayout Saving..."));

            StatisticsGridLayout.CopyTo(_fileStorage.StatisticsGridLayout);
            OnProgressChanged(new ProgressChangedEventArgs(80, "StatisticsGridLayout Saving..."));

            FilterGridLayout.CopyTo(_fileStorage.FilterGridLayout);
            OnProgressChanged(new ProgressChangedEventArgs(80, "FilterGridLayout Saving..."));

            Tools.CopyTo(_fileStorage.Tools);
            OnProgressChanged(new ProgressChangedEventArgs(80, "ToolTables Saving..."));

            _fileStorage.SaveToFile(path);

            IsSaved = true;
            IsChanged = false;
            OnSaveCompleted();

            OnProgressChanged(new ProgressChangedEventArgs(100, "Completed."));
        }

        /// <summary>
        /// Project betöltése az utvonalról
        /// </summary>
        public void Load(string path)
        {
            OnLoading();
    
            if (System.IO.Path.IsPathRooted(path))
            {
                FullPath = path;
                FileName = System.IO.Path.GetFileNameWithoutExtension(path);
                Environment.CurrentDirectory = System.IO.Path.GetDirectoryName(path);
            }
            else
            {
                FullPath = path;
                FileName = path;
            }
            _fileStorage.LoadFromFile(FullPath);
            OnProgressChanged(new ProgressChangedEventArgs(30, "File Loaded..."));

            OnProgressChanged(new ProgressChangedEventArgs(40, "Parameters Lodading..."));
            _fileStorage.Parameters.CopyTo(Parameters);

            OnProgressChanged(new ProgressChangedEventArgs(60, "Filters Loading..."));
            _fileStorage.Filters.CopyTo(Filters);

            OnProgressChanged(new ProgressChangedEventArgs(80, "CustomArbIdColumns Loading..."));
            _fileStorage.CustomArbIdColumns.CopyTo(CustomArbIdColumns);

            OnProgressChanged(new ProgressChangedEventArgs(80, "TraceGridLayout Loading..."));
            _fileStorage.TraceGridLayout.CopyTo(TraceGridLayout);

            OnProgressChanged(new ProgressChangedEventArgs(80, "LogGridLayout Loading..."));
            _fileStorage.LogGridLayout.CopyTo(LogGridLayout);

            OnProgressChanged(new ProgressChangedEventArgs(80, "StatisticsGridLayout Loading..."));
            _fileStorage.StatisticsGridLayout.CopyTo(StatisticsGridLayout);

            OnProgressChanged(new ProgressChangedEventArgs(80, "FilterGridLayout Loading..."));
            _fileStorage.FilterGridLayout.CopyTo(FilterGridLayout);

            OnProgressChanged(new ProgressChangedEventArgs(80, "ToolTables Loading..."));
            _fileStorage.Tools.CopyTo(Tools);

            IsSaved = true;
            IsChanged = false;


            OnLoadCompleted();
            
            OnProgressChanged(new ProgressChangedEventArgs(100, "Completed."));
        }

        /// <summary>
        /// Minden változás eldobása
        /// </summary>
        public void DropChanged()
        {
            IsChanged = false;
        }

        private void OnLoading()
        {
            if(Loading != null)
                Loading.Invoke(this, EventArgs.Empty);
        }

        private void OnLoadCompleted()
        {
            if(LoadCompleted != null)
                LoadCompleted.Invoke(this, EventArgs.Empty);
        }

        private void OnSaving()
        {
            if(Saving != null)
                Saving.Invoke(this, EventArgs.Empty);
        }

        private void OnSaveCompleted()
        {
            if(SaveCompleted!= null)
                SaveCompleted(this,EventArgs.Empty);
        }


        private void OnContentChanged(StorageChanegdEventArgs arg)
        {
            if (ContentChanged != null)
                ContentChanged(this, arg);
        }

        private void OnProgressChanged(ProgressChangedEventArgs e)
        {
            if(ProgressChange != null)
                ProgressChange(this, e);
        }

        /// <summary>
        /// 1. Untitled.MctTest - Equip-Test Kft. MCT (Pins: 2048)
        /// 2. Untitled.MctTest* - Equip-Test Kft. MCT (Pins: 2048)
        /// 3. Untitled.canx - Konvolúció Bt. MCAN120803 CAN BUS TOOL
        /// 4. Untitled.canx - Konvolúció Bt. MCAN120803 CAN BUS TOOL
        /// 5. Untitled.canx - Konvolúció Bt. MCAN120803 CAN BUS TOOL [Working directory: D\xxc\]
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var str = string.Empty;

            if (IsChanged)
                str = FileName + AppConstants.FileExtension + "*" + " - " + System.Windows.Forms.Application.CompanyName  + " " + System.Windows.Forms.Application.ProductName;
            else
                str = FileName + AppConstants.FileExtension + "" + " - " + System.Windows.Forms.Application.CompanyName + " " + System.Windows.Forms.Application.ProductName;

            if (Settings.Default.ShowWorkingDirectoryInTitleBar)
                str += " [Working directory: " + Environment.CurrentDirectory + "]";
            return str;
        }
    }
}
