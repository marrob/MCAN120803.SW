

namespace Konvolucio.MCAN120803.GUI.AppModules.Log.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.ComponentModel;

    using WinForms.Framework; /*IListChanging<ILogFileItem>*/

    /// <summary>
    /// A log fájlok listáját tárolja a felületnek (Log TreeView)
    /// </summary>
    public interface ILogFileCollection: IListChanging<ILogFileItem>
    {
        /// <summary>
        /// Logfájl_ok_ betöltését jelzi.
        /// </summary>
        event EventHandler CollectionLoading;

        /// <summary>
        /// Logfájl_ok_ betöltésének végét jelzi.
        /// </summary>
        event EventHandler CollectionLoadingComplete;

        /// <summary>
        /// Hosszú folyamat állaptát jelzi.
        /// </summary>
        event ProgressChangedEventHandler ProgressChanged;

        /// <summary>
        /// Log fájl lista változott.
        /// </summary>
        event ListChangedEventHandler ListChanged;

        /// <summary>
        /// Log fájlok száma.
        /// </summary>
        int Count { get; }

        /// <summary>
        /// A log fájlok listája már be van töltve.
        /// Optimalizálási célokra.
        /// </summary>
        bool IsLoaded { get; }

        /// <summary>
        /// A log fájl listájának betöltése.
        /// Az adott útvonal és projectnév alapján.
        /// location + "\\Log Storage for " + projectName;
        /// </summary>
        /// <param name="location">Project helye.</param>
        /// <param name="projectName">Project neve.</param>
        void Load(string location, string projectName);

        /// <summary>
        /// Új log fájl hozzáadása a listához.
        /// </summary>
        /// <param name="item">Új log fájl.</param>
        void Add(ILogFileItem item);

        /// <summary>
        /// Log fájl eltváolítása a listából és hozzá tartozó fájlt is törli.
        /// </summary>
        /// <param name="item"></param>
        void Remove(ILogFileItem item);

        /// <summary>
        /// Lista bejárható legyen...
        /// </summary>
        /// <returns></returns>
        IEnumerator<ILogFileItem> GetEnumerator();


    }

    /// <summary>
    /// A log fájlok listáját tárolja a felületnek (Log TreeView)
    /// </summary>
    public class LogFileCollection : BindingList<ILogFileItem>, ILogFileCollection
    {
        public event EventHandler CollectionLoading;
        public event EventHandler CollectionLoadingComplete;
        public event ProgressChangedEventHandler ProgressChanged;
        public event ListChangingEventHandler<ILogFileItem> ListChanging;
        public string Location { get; private set; }
        public bool IsLoaded { get; private set; }

        public void Load(string location, string projectName)
        {
            OnCollectionLoading();
            try
            {
                this.Location = location + "\\Log Storage for " + projectName;
                LoadFiles(this.Location);
                IsLoaded = true;
            }
            catch
            {
                throw;
            }
            finally
            {
                /*Meghívja ha még sem sikerülne...*/
                OnCollectionLoadingComplete();
            }
        }

        public new void Add(ILogFileItem item)
        {
            (item as LogFileItem).Guid = Guid.NewGuid().ToString();
            (item as LogFileItem).ProgressChanged += OnProgressChanged;
            base.Add(item);
        }

        public new void Remove(ILogFileItem item)
        {
            if (item != null)
            {
                System.IO.File.Delete(item.Path);
                (item as LogFileItem).ProgressChanged -= OnProgressChanged;
                OnItemRemove(item);
                base.Remove(item);
            }
        }
 
        /// <summary>
        /// Lista törlése
        /// </summary>
        public new void Clear()
        {
            OnReseting();
            base.Clear();
        }

        /// <summary>
        /// Jelezd, hogy minden elemet frssíteni kell.
        /// </summary>
        protected void OnReseting()
        {
            if (ListChanging != null)
                ListChanging(this, new ListChangingEventArgs<ILogFileItem>(ListChangingType.Clearing, null));
        }

        /// <summary>
        /// Jelezd, hogy elemet töröltéll.
        /// </summary>
        /// <param name="item"></param>
        protected void OnItemRemove(ILogFileItem item)
        {
            if (ListChanging != null)
                ListChanging(this, new ListChangingEventArgs<ILogFileItem>(ListChangingType.ItemRemoving, item));
        }

        /// <summary>
        /// Az könyvtárban található fájlok betöltése
        /// </summary>
        /// <param name="location">könyvtár útvonal</param>
        void LoadFiles(string location)
        {
            this.Clear();
            if (System.IO.Directory.Exists(location) && System.IO.Directory.GetFiles(location).FirstOrDefault(n => n.EndsWith(".s3db")) != null)
            {
                string[] fileArray = System.IO.Directory.GetFiles(location).Where(n => n.EndsWith(".s3db")).ToArray();
                int current = 0;
                foreach (var path in fileArray)
                {
                    /*Ha van olyan fájl aminek 0 bájt a méretet akkor azt nem listázzuk */
                    if (new System.IO.FileInfo(path).Length != 0)
                    {
                        LogFileItem log = new LogFileItem(path);
                        log.ProgressChanged += OnProgressChanged;
                        this.Add(log);
                    }
                    else
                    {
                        Console.WriteLine("Invalid Database file" + path);
                    }
                    OnProgressChanged(this, new ProgressChangedEventArgs((int)((++current / (double)fileArray.Length) * 100.0), "Log Loading:" + System.IO.Path.GetFileName(path) + " ..."));
                }
                OnProgressChanged(this, new ProgressChangedEventArgs(100, "Log Loading Complete..."));
            }
        }

        /// <summary>
        /// ProgessBar állapotát frssíti
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (ProgressChanged != null)
                ProgressChanged(sender, e);
        }

        /// <summary>
        /// Betöltés elejét jelzi
        /// </summary>
        protected void OnCollectionLoading()
        {
            if (CollectionLoading != null)
                CollectionLoading(this, EventArgs.Empty);
        }

        /// <summary>
        /// Betöltés végét jelzi
        /// </summary>
        protected void OnCollectionLoadingComplete()
        {
            if (CollectionLoadingComplete != null)
                CollectionLoadingComplete(this, EventArgs.Empty);        
        }
    }
}
