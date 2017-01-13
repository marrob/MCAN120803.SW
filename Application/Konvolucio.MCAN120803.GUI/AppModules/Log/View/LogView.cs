
using System.Runtime.Remoting.Channels;
using Konvolucio.MCAN120803.GUI.Services;

namespace Konvolucio.MCAN120803.GUI.AppModules.Log.View
{
    using System;
    using System.Windows.Forms;
    using System.Linq;
    using DataStorage;
    using Properties;
    using WinForms.Framework;

    public interface ILogView: IUiLayoutRestoring
    {
        ILogGridView LogGrid { get; }
        ILogDescriptionView DescriptionView { get; }
    }

    public partial class LogView : UserControl, ILogView
    {
        public ILogGridView LogGrid { get { return logGridView1; } }
        public ILogDescriptionView DescriptionView { get { return logDescriptionView1; } }

        public LogView()
        {
            InitializeComponent();

            #region Log File események
            EventAggregator.Instance.Subscribe<LogFileAppEvent>(e =>
            {
                switch (e.ChangingType)
                {
                    case FileChangingType.LoadComplete:
                        {
                            LogGrid.Source = e.LogFile.Messages;
                            DescriptionView.Content = e.LogFile.Info.Description;
                            DescriptionView.LogName = e.LogFile.Name;
                            LogGrid.Enabled = true;
                            //LogGrid.TimestampFormat = "HH";
                            break;
                        }
                    case FileChangingType.Loading:
                        {
                            LogGrid.Enabled = false;
                            LogGrid.Source = null;
                            DescriptionView.Content = "";
                            DescriptionView.LogName = "*";
                            break;
                        }
                    case FileChangingType.LoadCorrupted:
                        {
                            LogGrid.Enabled = true;
                            LogGrid.Source = null;
                            DescriptionView.Content = "";
                            DescriptionView.LogName = "Loading Corrupted... Please try again.";
                            break;
                        }
                    case FileChangingType.UnLoadComplete:
                        {
                            LogGrid.Enabled = true;
                            LogGrid.Source = null;
                            DescriptionView.Content = "";
                            DescriptionView.LogName = "*";
                            break;
                        }
                    default:
                        {
                            break;
                        }
                
                }
            });
            #endregion 

            #region Project File események
            EventAggregator.Instance.Subscribe<StorageAppEvent>((e1) =>
            {

                switch (e1.ChangingType)
                {
                    case FileChangingType.Loading:
                    {
                        LogGrid.GridLayout = null;
                        LogGrid.DefaultLayout();

                        break;
                    }

                    case FileChangingType.LoadComplete:
                    {
                        LogGrid.CustomArbIdColumns = e1.Storage.CustomArbIdColumns;
                        LogGrid.GridLayout = e1.Storage.LogGridLayout;
                        LogGrid.TimestampFormat = e1.Storage.Parameters.TimestampFormat;    

                        break;
                    }

                    case FileChangingType.Saving:
                    {
                        LogGrid.GridLayout.CopyTo(e1.Storage.LogGridLayout);
                        break;
                    }

                    case FileChangingType.ContentChanged:
                    {
                        if (e1.Details.DataObjects == DataObjects.ParameterProperty)
                        {
                            if (e1.Details.PropertyDescriptor.Name == PropertyPlus.GetPropertyName(() => e1.Storage.Parameters.ArbitrationIdFormat) ||
                                e1.Details.PropertyDescriptor.Name == PropertyPlus.GetPropertyName(() => e1.Storage.Parameters.DataFormat))
                            {
                                /*Converterek miatt újra kell rajzolni a Grid-et*/
                                /*Ez kritikus! ha egy sor törlésekor jön egy esemény ami frssíti a DGV-az DGV hibhoz vezet!*/
                                LogGrid.Refresh();
                            }

                            if (e1.Details.PropertyDescriptor.Name == PropertyPlus.GetPropertyName(() => e1.Storage.Parameters.TimestampFormat))
                                LogGrid.TimestampFormat = e1.Storage.Parameters.TimestampFormat;                         
                        }
                        break;
                    }
                }
                
            });
            #endregion 
        }


        private void UpdateLocalization()
        {
            toolStripLabelName.Text = CultureService.Instance.GetString(CultureText.text_LOGVIEW);
        }

        private void buttonToolStripAutoColumnWidth_Click(object sender, EventArgs e)
        {
            LogGrid.ColumnsAutoSizeAll();
        }

        public void LayoutSave()
        {
            Settings.Default.splitContainerLogDesciription_SplitterDistance = splitContainerLogDesciription.SplitterPrecent;
        }

        public void LayoutRestore()
        {
            splitContainerLogDesciription.SplitterPrecent = Settings.Default.splitContainerLogDesciription_SplitterDistance;
        }

        private void LogView_Load(object sender, EventArgs e)
        {
            UpdateLocalization();
        }
    }
}
