
namespace Konvolucio.MCAN120803.GUI
{
    using System;
    using System.Linq;
    using System.Windows.Forms;
    using System.Configuration;
    using System.Threading;

    using System.Diagnostics;
    using System.Reflection;

    using Microsoft.VisualBasic.ApplicationServices;
    using System.ComponentModel;
    using AppModules.Tools;
    using AppModules.Tools.Commands;
    using AppModules.Tools.TreeNodes;
    using WinForms.Framework;

    using Properties;
    using Common;
    using DataStorage;
    using Services;

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += ApplicationOnThreadException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;


            if (Settings.Default.EnableSingleInstance)
            {
                string[] args = Environment.GetCommandLineArgs();
                SingleInstanceController controller = new SingleInstanceController();
                /*A már látható ablak itt kapja meg a fájl agrumentumot.*/
                controller.Run(args);
            }
            else
            {
                /*A főablak itt jelenik meg előszrör*/
                Application.Run(new App().MainForm as Form);
            }
            
        }

        /// <summary>
        /// Globalis hiba elkaopó
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void CurrentDomainOnUnhandledException(object sender, System.UnhandledExceptionEventArgs e)
        {
            new ErrorHandlerService().Show(e);
        }
       
        /// <summary>
        /// Globális hiba elkapó minden egyéb szálára.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void ApplicationOnThreadException(object sender, ThreadExceptionEventArgs e)
        {
            new ErrorHandlerService().Show(e);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class SingleInstanceController : WindowsFormsApplicationBase
    {
        App _app;
       
        public SingleInstanceController()
        {
            IsSingleInstance = true;
            StartupNextInstance += this_StartupNextInstance;
        }

        void this_StartupNextInstance(object sender, StartupNextInstanceEventArgs e)
        {   /*Uj példány helyet az aktuális példányt indítja*/
            var form = _app.MainForm as AppModules.Main.View.MainForm;
            _app.Start(e.CommandLine.ToArray());
            if(form != null)
              form.Refresh();
        }

        protected override void OnCreateMainForm()
        {
            /*Alkalmazás indul*/
            _app = new App();
            MainForm = (Form)_app.MainForm;
        }
    }

    public interface IApp
    {
        void ShowLogInMainView();
        void ShowTraceInMainView();
        void ShowWorksapce();
    }
    /// <summary>
    /// TODO:
    /// 2016.06.13: - A Tab-al elválasztott data Frame-et nem jól doglozza fel.
    /// </summary>
    class App : IApp
    {
        private readonly TreeNode _startTreeNode;

        public static SynchronizationContext SyncContext = null;

        public AppModules.Main.View.IMainForm MainForm { get { return _mainForm; } }
        private readonly AppModules.Main.View.MainForm _mainForm;
        private readonly ProjectParameters _parameters;

        private readonly CustomArbIdColumnCollection _customArbIdColumns;
        private readonly Storage _storage;
        private readonly AppModules.Adapters.IAdapterService _adapterService;
        private readonly AppModules.Workspace.Service.IWorkspaceService _workspaceService;

        private readonly AppModules.Statistics.Adapter.Model.IAdapterStatistics _adapterStatistics;
        private readonly AppModules.Statistics.Message.Model.MessageStatistics _messageStatistics;

        private readonly AppModules.Tracer.Model.MessageTraceCollection _messageTrace;
        private readonly AppModules.Filter.Model.MessageFilterCollection _filters;
        private readonly AppModules.Log.Model.ILogFileCollection _logFiles;

        private readonly ToolStripMenuItem[] _traceSenderMainToolbarCommands;
        private readonly ToolStripMenuItem[] _workspaceMainToolbarCommands;

        #region Constructor

        /// <summary>
        /// Konstruktor
        /// </summary>
        public App()
        {
            /*Defult Culture set*/
            CultureService.Instance.CurrentCultureName = Settings.Default.CurrentCultureName;

            /*Application Settings Upgrade*/
            if (Settings.Default.ApplictionSettingsSaveCounter == 0)
            {
                Settings.Default.Upgrade();
                Settings.Default.ApplictionSettingsUpgradeCounter++;
            }
            Settings.Default.ApplictionSettingsSaveCounter++;
            Settings.Default.PropertyChanged += new PropertyChangedEventHandler(Settings_PropertyChanged);
            Settings.Default.SettingChanging += new SettingChangingEventHandler(Settings_SettingChanging);

            _customArbIdColumns = new CustomArbIdColumnCollection();

            _adapterStatistics = new AppModules.Statistics.Adapter.Model.AdapterStatistics();
            _messageStatistics = new AppModules.Statistics.Message.Model.MessageStatistics();

            _parameters = new ProjectParameters();
            _messageTrace = new AppModules.Tracer.Model.MessageTraceCollection();
            _filters = new AppModules.Filter.Model.MessageFilterCollection();


            _logFiles = new AppModules.Log.Model.LogFileCollection();

            _logFiles.ProgressChanged += new ProgressChangedEventHandler(LogFiles_ProgressChanged);
            _logFiles.CollectionLoading += new EventHandler(LogFiles_CollectionLoading);
            _logFiles.CollectionLoadingComplete += new EventHandler(LogFiles_CollectionLoadingComplete);

            EventAggregator.Instance.Subscribe<LogFileAppEvent>(e =>
            {
                /*Megakádolyozza a felhasználót a Log fájlok betöltése közben, csináljon.*/
                /*TODO: Ennek az egyesített TreeView-ban lesz a helye egy EventAggretaorban*/
                /*A fájlok betöltése közben ne lépkedjen a TreeView-ban*/

                if (e.ChangingType == FileChangingType.Loading)
                {
                    _mainForm.MainView.TreeView.Enabled = false;
                    Cursor.Current = Cursors.WaitCursor;
                }

                if (e.ChangingType == FileChangingType.LoadComplete)
                {
                    _mainForm.MainView.TreeView.Enabled = true;
                    Cursor.Current = Cursors.Default;

                }
            });

            _storage = new Storage(_parameters, _filters, _customArbIdColumns);
            _storage.ContentChanged += new EventHandler<StorageChanegdEventArgs>(ProjectService_ContentChanged);
            _storage.Loading += new EventHandler(ProjectService_Loading);
            _storage.Saving += new EventHandler(ProjectService_Saving);
            _storage.LoadCompleted += new EventHandler(ProjectService_LoadCompleted);
            _storage.SaveCompleted += new EventHandler(ProjectService_SaveCompleted);
            _storage.ProgressChange += new ProgressChangedEventHandler(ProjectService_ProgressChange);

            _workspaceService = new AppModules.Workspace.Service.WorkspaceService();
            _workspaceService.Load(Services.PathService.Instance.WorkspacePath);
            _workspaceService.WorkspaceItems.OpenProjectChanged += new EventHandler(WorkspaceService_OpenProjectChanged);

            _mainForm = new AppModules.Main.View.MainForm();
            MainForm.FormClosed += new FormClosedEventHandler(MainForm_FormClosed);
            MainForm.FormClosing += new FormClosingEventHandler(MainForm_FormClosing);
            MainForm.Disposed += new EventHandler(MainForm_Disposed);
            MainForm.KeyUp += new KeyEventHandler(MainForm_KeyUp);
            MainForm.Shown += new EventHandler(MainForm_Shown);

            MainForm.MainView.WorkspaceView.DataSource = _workspaceService.WorkspaceItems;
            MainForm.MainView.TreeView.AfterSelect += new TreeViewEventHandler(TreeView_AfterSelect);
            MainForm.MainView.TreeView.AfterExpand += new TreeViewEventHandler(TreeView_AfterExpand);
            MainForm.MainView.TreeView.AfterCollapse += new TreeViewEventHandler(TreeView_AfterCollapse);
            MainForm.MainView.TreeView.NodeMouseClick += new TreeNodeMouseClickEventHandler(TreeView_NodeMouseClick);
            MainForm.Text = _storage.ToString();


            #region Set TimeService
            Services.TimerService.Instance.Interval = Settings.Default.GuiRefreshRateMs;
            #endregion

            AppDiagService.ContentAdded += AppDiag_ContentAdded;
            EventAggregator.Instance.PublishEvent += EventAggregator_PublishEvent;

            _adapterService = new AppModules.Adapters.AdapterService(
                _messageTrace,
                _storage.Parameters,
                _adapterStatistics,
                _messageStatistics,
                _filters,
                _storage,
                _logFiles,
                _storage.Tools);

            #region  Presenters


            new AppModules.Tracer.Presenter(
                MainForm.MainView.TraceAndPagesView.TraceGridView,
                _messageTrace,
                _parameters);

            new AppModules.Statistics.Message.Presenter(
                MainForm.MainView.StatisticsView,
                _messageStatistics,
                _storage);

            new AppModules.Filter.Presenter(
               MainForm.MainView.DataGridView,
                _filters,
                _storage,
                _parameters);

            new Presenter(
                _storage,
                MainForm.MainView.TraceAndPagesView.Pages,
                _storage.Tools,
                _storage.CustomArbIdColumns);

            #endregion


            _adapterService.Stopped += Adapter_Stopped;
            _adapterService.Started += AdapterService_Started;

            #region Main Toolbar Menu

            _traceSenderMainToolbarCommands = new ToolStripMenuItem[]
            {
                new AppModules.Main.Commands.HamburgerCommand(this),
                new AppModules.Main.Commands.OpenCommand(_storage, _workspaceService),
                new AppModules.Main.Commands.NewCommand(_storage, _adapterService),
                new AppModules.Main.Commands.SaveCommand(_storage),
                new AppModules.Main.Commands.SaveAsCommand(_storage),
                new AppModules.Main.Commands.PlayCommand(_storage, _adapterService),
                new AppModules.Main.Commands.StopCommand(_adapterService),
                //new AppModules.Main.Commands.TraceExportToExcelCommand(traceMessages),
                new AppModules.Main.Commands.OptionsCommand(),
            };

            _workspaceMainToolbarCommands = new ToolStripMenuItem[]
            {
                new AppModules.Main.Commands.BackCommand(this),
            };

            #endregion

            #region Diag ContextMenu

            MainForm.MainView.AppDiag.Menu.Items.AddRange(
                new ToolStripItem[]
                {
                    new AppModules.AppDiag.Commands.ClearCommand(_mainForm.MainView.AppDiag),
                    new AppModules.AppDiag.Commands.SaveCommand(_mainForm.MainView.AppDiag),
                    new AppModules.AppDiag.Commands.OffCommand(_mainForm.MainView.AppDiag)
                });

            #endregion

     

            #region  Log Context Menu

            _mainForm.MainView.LogView.LogGrid.Menu.Items.AddRange(
                new ToolStripItem[]
                {
                    new AppModules.Log.Commands.DeleteMessageCommand(_mainForm.MainView.LogView),
                }
            );

            #endregion

            #region The Tree Nodes
            MainForm.MainView.TreeView.Nodes.Add(
                new AppModules.Adapters.TreeNodes.ProjectTopTreeNode(
                    new TreeNode[]
                    {
                        (_startTreeNode = new AppModules.Adapters.TreeNodes.AdapterTreeNode(_mainForm.MainView.TreeView,
                            new TreeNode[]
                            {
                                new AppModules.Statistics.Adapter.TreeNodes.AdapterStatisticsTreeNode(_adapterStatistics), 
                                new AppModules.Statistics.Message.TreeNodes.MessagesTreeNode(_messageStatistics,_adapterService, _parameters),
                                new AppModules.Filter.TreeNodes.FiltersTreeNode(_filters),
                                new TopTreeNode(MainForm.MainView.TraceAndPagesView.Pages), 
                            })),

                        new AppModules.Log.TreeNodes.LogTopTreeNode(this,
                            _logFiles,
                            _mainForm.MainView.LogView.DescriptionView),
                    })

            );
            #endregion

            #region Main TreeView Items
            MainForm.MainView.TreeView.ContextMenuStrip = new ContextMenuStrip();
            MainForm.MainView.TreeView.ContextMenuStrip.Items.AddRange(
                new ToolStripItem[]
                {
                    
                    new AppModules.Main.Commands.ExpandAllSectionCommand(),
                    new AppModules.Main.Commands.ExpandMainSectionCommand(), 
                    new AppModules.Main.Commands.CollapseAllSectionCommand(),

                    new AppModules.Adapters.Commands.EnabledCommand(), 

                    new AppModules.Filter.Commands.EnabledNodeCommand(),
                    new AppModules.Filter.Commands.DefaultNodeCommand(_filters), 

                    new AppModules.Statistics.Adapter.Commands.DefaultNodeCommand(_adapterStatistics),
                    new AppModules.Statistics.Adapter.Commands.EnabledNodeCommand(), 
                    new AppModules.Statistics.Message.Commands.EnabledNodeCommand(), 
                    new AppModules.Statistics.Message.Commands.ClearNodeCommand(_messageStatistics),
                    new AppModules.Statistics.Message.Commands.DefaultNodeCommand(_messageStatistics, MainForm.MainView.StatisticsView),
                   
                    new NewToolNodeCommand(MainForm.MainView.TreeView, _storage.Tools),
                    new DeleteToolNodeCommand(MainForm.MainView.TreeView, _storage.Tools), 
                    new RenameToolNodeCommand(MainForm.MainView.TreeView,_storage.Tools,MainForm.MainView.TraceAndPagesView.Pages), 

                    new AppModules.Log.Commands.DeleteFileCommand(),
                    new AppModules.Log.Commands.EditDescriptionCommand(),
                    new AppModules.Log.Commands.ReloadCommand(_logFiles, _storage),
                    new AppModules.Log.Commands.RenameFileCommand(_logFiles),
                    new AppModules.Log.Commands.ExportCommand(_storage, _mainForm.MainView.LogView.LogGrid),
                    new AppModules.Log.Commands.EnabledCommand(),
                }
            );
           #endregion

            #region  Tree Menu Items
            MainForm.MainView.TreeToolStrip.Items.AddRange(
                new ToolStripItem[]
                {

                    new AppModules.Adapters.Commands.EnabledCommand(), 

                    /**/
                    //TODO: new AddMessageFilterCommand(filters),
                    //new AddMessageToFilterCommand(_filters),
                    new AppModules.Filter.Commands.EnabledNodeCommand(),

                    new AppModules.Statistics.Adapter.Commands.DefaultNodeCommand(_adapterStatistics),
                    new AppModules.Statistics.Adapter.Commands.EnabledNodeCommand(), 
                    new AppModules.Statistics.Message.Commands.EnabledNodeCommand(), 
                    
                    /*Log*/
                    new AppModules.Log.Commands.DeleteFileCommand(),
                    new AppModules.Log.Commands.EditDescriptionCommand(),
                    new AppModules.Log.Commands.ReloadCommand(_logFiles, _storage),
                    new AppModules.Log.Commands.RenameFileCommand(_logFiles),
                    new AppModules.Log.Commands.ExportCommand(_storage, _mainForm.MainView.LogView.LogGrid),
                    new AppModules.Log.Commands.EnabledCommand(),

                }
            );
            #endregion

            #region Image List
            var images = new ImageList();
            images.Images.Add("stats_0", Resources.stats_0);
            images.Images.Add("Mail_16x16", Resources.Mail_16x16);
            images.Images.Add("arrow_down", Resources.arrow_down);
            images.Images.Add("arrow_up", Resources.arrow_up);
            images.Images.Add("Sync16", Resources.Sync16);
            images.Images.Add("l_clock", Resources.l_clock);
            images.Images.Add("counter16", Resources.counter16);
            images.Images.Add("Statistics16", Resources.Statistics16);
            images.Images.Add("Refresh_16x16", Resources.Refresh_16x16);
            images.Images.Add("mails16", Resources.mails16);
            images.Images.Add("mails24", Resources.mails24);
            images.Images.Add("Warning16", Resources.Warning16);
            images.Images.Add("Sandglass16", Resources.Sandglass16);
            images.Images.Add("trash16", Resources.trash16);
            images.Images.Add("id16", Resources.id16);
            images.Images.Add("mail16", Resources.mail16);
            images.Images.Add("stats_min", Resources.stats_min);
            images.Images.Add("stats_max", Resources.stats_max);
            images.Images.Add("data16", Resources.data16);
            images.Images.Add("watch16", Resources.watch16);
            images.Images.Add("Filter16", Resources.Filter16);
            images.Images.Add("switchon16", Resources.switchon16);
            images.Images.Add("switchoff16", Resources.switchoff16);
            images.Images.Add("database16", Resources.database16);
            images.Images.Add("log16", Resources.log16);
            images.Images.Add("rename16", Resources.rename16);
            images.Images.Add("article16", Resources.article16x16);
            images.Images.Add("dictionary48", Resources.dictionary48);
            images.Images.Add("connector16", Resources.connector16);
            images.Images.Add("FilterClear16", Resources.FilterClear16);
            images.Images.Add("FilterFilled16", Resources.FilterFilled16);
            images.Images.Add("table24", Resources.table24);
            images.Images.Add("canbus24", Resources.canbus24);
            images.Images.Add("devicenet", Resources.Lum_DeviceNet);
            images.Images.Add("fullscreen24", Resources.fullscreen24);
            images.Images.Add("hummer24", Resources.hummer24);
            #endregion

            MainForm.MainView.TreeView.ImageList = images;

        }



        #endregion // Constructor

        #region Project események
        /// <summary>
        /// Project ProgressBar
        /// </summary>
        private void ProjectService_ProgressChange(object sender, ProgressChangedEventArgs e)
        {
            _mainForm.ProgressBarUpdate(e);
        }

        /// <summary>
        /// Project mentése befjeződött.
        /// </summary>
        private void ProjectService_SaveCompleted(object sender, EventArgs e)
        {
            MainForm.Text = _storage.ToString();
            if (System.IO.File.Exists(_storage.FullPath))
            {
                _workspaceService.WorkspaceItems.AddProject(_storage.FullPath);
                _workspaceService.LastOpenedProjectPath = _storage.FullPath;
            }
            EventAggregator.Instance.Publish(new StorageAppEvent(_storage, FileChangingType.SaveComplete));
            MainForm.CursorDefault();
        }

        /// <summary>
        /// Project betöltése befejződött.
        /// </summary>
        private void ProjectService_LoadCompleted(object sender, EventArgs e)
        {
            /*Ablak neve*/
            MainForm.Text = _storage.ToString();

            /*Az utoljára használt project ez amit most betöltött*/
            if (System.IO.File.Exists(_storage.FullPath))
            {
                _workspaceService.WorkspaceItems.AddProject(_storage.FullPath);
                _workspaceService.LastOpenedProjectPath = _storage.FullPath;
            }

            /*Converterek formátumának beállítása*/
            Converters.ArbitrationIdConverter.Format = _storage.Parameters.ArbitrationIdFormat;
            Converters.DataFrameConverter.Format = _storage.Parameters.DataFormat;

            /*Megnyitást követően rá áll az Adapter Nódra a TreeView-ban.*/
            MainForm.MainView.TreeView.Nodes[0].ExpandAll();
            MainForm.MainView.TreeView.SelectedNode = _startTreeNode;

            _adapterStatistics.Reset();
            _messageStatistics.Clear();

            /*Mindenkinek jelzem hogy a project betöltésre és haszálatra kész.*/
            EventAggregator.Instance.Publish(new StorageAppEvent(_storage, FileChangingType.LoadComplete));
            MainForm.CursorDefault();
        }

        /// <summary>
        /// Project mentése folyamatban.
        /// </summary>
        private void ProjectService_Saving(object sender, EventArgs e)
        {
            MainForm.CursorWait();          
            EventAggregator.Instance.Publish(new StorageAppEvent(_storage, FileChangingType.Saving));
        }

        /// <summary>
        /// Project betöltése folyamatban.
        /// </summary>
        private void ProjectService_Loading(object sender, EventArgs e)
        {
            MainForm.CursorWait();
            EventAggregator.Instance.Publish(new StorageAppEvent(_storage, FileChangingType.Loading));
        }

        /// <summary>
        /// Project változott és ez rléteszenzé a változásokat, ami alapján a céltudosabban lehetne viselkedni.
        /// </summary>
        private void ProjectService_ContentChanged(object sender, StorageChanegdEventArgs e)
        {
            Debug.WriteLine(GetType().Namespace + "." + GetType().Name + "." + MethodBase.GetCurrentMethod().Name + "():" + e.ToString());

            if (e.DataObjects == DataObjects.ParameterProperty)
            {
                /*A megjelítéshez tartozó konverterek itt frissülnek*/
                if(e.PropertyDescriptor.Name == PropertyPlus.GetPropertyName(()=>_storage.Parameters.ArbitrationIdFormat))
                    Converters.ArbitrationIdConverter.Format = _storage.Parameters.ArbitrationIdFormat;

                if (e.PropertyDescriptor.Name == PropertyPlus.GetPropertyName(() => _storage.Parameters.DataFormat))
                    Converters.DataFrameConverter.Format = _storage.Parameters.DataFormat;

            }
            /*Project fájl neve...*/
            MainForm.Text = _storage.ToString();
            EventAggregator.Instance.Publish(new StorageAppEvent(_storage, FileChangingType.ContentChanged, e));
        }
        #endregion

        #region Log Fájl események
        /// <summary>
        /// Log kÖnyvtár betöltése kész.
        /// </summary>
        private void LogFiles_CollectionLoadingComplete(object sender, EventArgs e)
        {
            EventAggregator.Instance.Publish<LogFileCollectionAppEvent>(new LogFileCollectionAppEvent(_logFiles, FileChangingType.LoadComplete));

            /*TODO: Ennek az egyesített TreeView-ban lesz a helye egy EventAggretaorban*/
            /*TODO: Az Enabled használta nem szép a Cursor Click tíltása jobb lenne.*/
            _mainForm.MainView.TreeView.Enabled = true;
            Cursor.Current = Cursors.Default;
        }

        /// <summary>
        /// Log kÖnyvtár betöltése folyamatban.
        /// </summary>
        private void LogFiles_CollectionLoading(object sender, EventArgs e)
        {
            EventAggregator.Instance.Publish(new LogFileCollectionAppEvent(_logFiles, FileChangingType.Loading));
            /*TODO: Ennek az egyesített TreeView-ban lesz a helye egy EventAggretaorban*/
            _mainForm.MainView.TreeView.Enabled = false;
            Cursor.Current = Cursors.WaitCursor;
        }

        /// <summary>
        /// Log ProgressBar
        /// </summary>
        private void LogFiles_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            _mainForm.ProgressBarUpdate(e);
        }
        #endregion

        #region Settings események
        /// <summary>
        /// Settings tulajodnság változtt.
        /// </summary>
        void Settings_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Debug.WriteLine(GetType().Namespace + "." + GetType().Name + "." + MethodBase.GetCurrentMethod().Name + "(): " + e.PropertyName + ", NewValue: " + Settings.Default[e.PropertyName]);

            if (e.PropertyName == PropertyPlus.GetPropertyName(() => Settings.Default.ShowWorkingDirectoryInTitleBar))
            {
                /*Project fájl neve...*/
                _mainForm.Text = _storage.ToString();
            }

            if (e.PropertyName == PropertyPlus.GetPropertyName(() => Settings.Default.GuiRefreshRateMs))
            {
                TimerService.Instance.Interval = Settings.Default.GuiRefreshRateMs;
            }
        }

        /// <summary>
        /// Beállítás változik, itt még a Settings.Default property még nem változott meg, de e.NewValue már tudja az új értékét.
        /// </summary>
        void Settings_SettingChanging(object sender, SettingChangingEventArgs e)
        {
            Debug.WriteLine(GetType().Namespace + "." + GetType().Name + "." + MethodBase.GetCurrentMethod().Name + "()");
        }
        #endregion

        /// <summary>
        /// AppTrace sorokoat gyüjti. 
        /// </summary>
        private void AppDiag_ContentAdded(object sender, AppDiagnosticsEventArgs e)
        {
            _mainForm.MainView.AppDiag.WriteLine(e.Content);
        }

        /// <summary>
        /// App Progrogress állapotát frissíti
        /// </summary>
        private void AppDiagnosticsService_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
             _mainForm.ProgressBarUpdate(e);
        }

        #region TreeView Events

        /// <summary>
        /// Valamelyik TreeView-ben változtott a kijelőlés
        /// </summary>
        private void TreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            EventAggregator.Instance.Publish(new TreeViewSelectionChangedAppEvent(e.Node));

            if (e.Node is AppModules.Log.TreeNodes.LogFileNameTreeNode)
                ShowLogInMainView();

            if (e.Node is AppModules.Adapters.TreeNodes.AdapterTreeNode)
                ShowTraceInMainView();

            if (e.Node is AppModules.Statistics.Message.TreeNodes.MessagesTreeNode)
                ShowStatistics();

            if (e.Node is AppModules.Filter.TreeNodes.FiltersTreeNode ||
                e.Node is AppModules.Filter.TreeNodes.FilterNameTreeNode)
                ShowFilter();

            if (MainForm.MainView.TraceAndPagesView.Pages.ClickOnNodeHandler(e.Node))
            {
                ShowTraceInMainView();
            }
        }

        /// <summary>
        ///Szekció bezárása utáni esemény, kell hogy frssiüljön szekció context menü elem.
        /// </summary>
        private void TreeView_AfterCollapse(object sender, TreeViewEventArgs e)
        {
            EventAggregator.Instance.Publish(new TreeViewSelectionChangedAppEvent(e.Node));
        }

        /// <summary>
        /// Szekció kibontása uánti esemény, kell hogy frssiüljön szekció context menü elem.
        /// </summary>
        private void TreeView_AfterExpand(object sender, TreeViewEventArgs e)
        {
            EventAggregator.Instance.Publish(new TreeViewSelectionChangedAppEvent(e.Node));
        }


        void TreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var logFileNode = e.Node as AppModules.Log.TreeNodes.LogFileNameTreeNode;
                if (logFileNode != null)
                {
                    var treeView = sender as TreeView;
                    if (treeView != null)
                        if(treeView.SelectedNode != logFileNode)
                            logFileNode.Load();
                }
             
            }
        }
        #endregion
        
        /// <summary>
        /// Workspacről fájl töltt be, ez ugyan az mint az OpenFileDialog-al nyitotta volna meg.
        /// Itt csak be kell tölteni a fájlt és megjeleníteni a TraceView-t
        /// </summary>
        private void WorkspaceService_OpenProjectChanged(object sender, EventArgs e)
        {
#if TRACE
            AppDiagService.WriteLine(this.GetType().Namespace + "." + this.GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name + "()" + " : " + sender.ToString());
#endif
            if (_storage.IsChanged)
            { /*Betöltött project változott*/
                var user = new AppModules.Main.View.SaveMessageBox().Show(_storage.FileName + AppConstants.FileExtension);
                if (user == UserAction.Yes)
                {
                    /*Régi project változott => mensd és mutasd a felületet és tölsd be az újat*/
                    new AppModules.Main.Commands.SaveCommand(_storage).PerformClick();
                    ShowTraceInMainView();
                    var workspaceItems = sender as AppModules.Workspace.Model.IWorkspaceCollection;
                    if(workspaceItems!=null)
                        _storage.Load(workspaceItems.OpenedItem.ItemFilePath);
                }
                if (user == UserAction.No)
                {
                    /*Régi project változott => NE mensd és mutasd a felületet és tölsd be az újat*/
                    _storage.DropChanged();
                    ShowTraceInMainView();
                    var workspaceItems = sender as AppModules.Workspace.Model.IWorkspaceCollection;
                    if (workspaceItems != null)
                        _storage.Load(workspaceItems.OpenedItem.ItemFilePath);
                }
                else if (user == UserAction.Cancel)
                {
                    /*Ne csinálj semmit*/
                }
            }
            else
            {
                try
                {
                    /*A régi projectben nincs változás betölhető az új project*/
                    ShowTraceInMainView();
                    var workspaceItems = sender as AppModules.Workspace.Model.IWorkspaceCollection;
                    if (workspaceItems != null)
                        _storage.Load(workspaceItems.OpenedItem.ItemFilePath);
                    /*Sikerült betölteni*/
                }
                catch
                {
                    /*Nem sikerült az utolsó projectet betölteni, ezért most egy Untitled project Indul*/
                    _storage.New(_adapterService.GetDefaultDeviceName, _adapterService.GetDefaultBaudrate);
                    ShowTraceInMainView();
                    /*Tovább dobja a hibaüzenete*/
                    throw;
                }
            }
        }

        #region Adapter Események
        /// <summary>
        /// Az adapter leállt vagy leállitották.
        /// </summary>
        private void Adapter_Stopped(object sender, EventArgs e)
        {
#if TRACE
            AppDiagService.WriteLine(this.GetType().Namespace + "." + this.GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name + "()" + " : " + sender.ToString());
#endif
            EventAggregator.Instance.Publish<StopAppEvent>(new StopAppEvent(_adapterService));
            TimerService.Instance.Stop();          
        }
        /// <summary>
        /// Az adapter elindult
        /// </summary>
        private void AdapterService_Started(object sender, EventArgs e)
        {
#if TRACE
            AppDiagService.WriteLine(this.GetType().Namespace + "." + this.GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name + "()" + " : " + sender.ToString());
#endif
            EventAggregator.Instance.Publish<PlayAppEvent>(new PlayAppEvent(_adapterService));
            TimerService.Instance.Start();
        }
        #endregion

        /// <summary>
        /// Event aggregator müködött.
        /// </summary>
        private void EventAggregator_PublishEvent(object sender, EventArgs e)
        {         
            Debug.WriteLine(this.GetType().Namespace + "." + this.GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name); 
        }

        #region MainForm események
        /// <summary>
        /// Az alkalmazás fokuszában lenyomtak egy billentyüt.
        /// </summary>
        void MainForm_KeyUp(object sender, KeyEventArgs e)
        {

#if TRACE
            if (Settings.Default.KeyTracingEnable)
                AppDiagService.WriteLine(this.GetType().Namespace + "." + this.GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + e.Modifiers.ToString() + " " + e.KeyCode.ToString());
#endif
            /*
            TODO: Ezt meg csinálni
            foreach (ToolStripMenuItem cmd in _traceSenderMainToolbarCommands)
                if (cmd.ShortcutKeys == e.KeyData)
                    cmd.PerformClick();
            _messageSender.KeyPressed(e.KeyCode.ToString());
            */
        }

        /// <summary>
        /// MainFrom megjelnt 
        /// Innentől él s SyncContext!
        /// User felé a default állapot, ami nem kerül soha mentésre...
        /// Betölti a GUI paramtéereket.
        /// </summary>
        void MainForm_Shown(object sender, EventArgs e)
        {
#if TRACE
            AppDiagService.WriteLine( this.GetType().Namespace + "." + this.GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name + "()");
#endif
            
            SyncContext = SynchronizationContext.Current;
            _mainForm.LayoutRestore();   
            /*Ötölti be a projectet*/
            Start(Environment.GetCommandLineArgs());
            /*Kezdő Node Legyen az Adapter node*/
            EventAggregator.Instance.Publish<TreeViewSelectionChangedAppEvent>(new TreeViewSelectionChangedAppEvent(_startTreeNode));
        }

        /// <summary>
        /// MainFrom bezárult 
        /// Itt ment mindent amit kell.
        /// </summary>
        void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
#if TRACE
            AppDiagService.WriteLine( this.GetType().Namespace + "." + this.GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name + "()");
#endif
            _workspaceService.Save();
            _adapterService.Dispose();
            _storage.Tools.Dispose();
            _mainForm.LayoutSave();
            Settings.Default.Save();
            EventAggregator.Instance.Dispose();
            TimerService.Instance.Dispose();
        }

        /// <summary>
        /// Erőrorrások felszabadítása az alakalmazás bezárása után
        /// </summary>
        void MainForm_Disposed(object sender, EventArgs e)
        {
#if TRACE
            AppDiagService.WriteLine( this.GetType().Namespace + "." + this.GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name + "()");
#endif       
        }

        /// <summary>
        /// Az alaklamzás bezárását kérte a felhasználó, ez még megszakítható
        /// </summary>
        void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
#if TRACE
            AppDiagService.WriteLine( this.GetType().Namespace + "." + this.GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name + "()");
#endif
            if (_storage.IsChanged)
            {
                var user = new AppModules.Main.View.SaveMessageBox().Show(_storage.FileName + AppConstants.FileExtension);
                if (user == UserAction.Yes)
                {
                    e.Cancel = true;
                    new AppModules.Main.Commands.SaveCommand(_storage).PerformClick();
                    _mainForm.Close();
                }
                else if (user == UserAction.Cancel)
                {
                    e.Cancel = true;
                }
            }
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        public void OpenArg(string[] args)
        {
            Debug.WriteLine(this.GetType().Namespace + "." + this.GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + string.Join<string>("\r\n", args));

            if (args.Count() > 1 && System.IO.File.Exists(args[1]) && !args[1].Contains("UnitTest.nunit"))
            {
                _storage.Load(args[1]);
                ShowTraceInMainView();
            }
        }

        /// <summary>
        /// Itt dől el hogy induláskor mit lát a felszhnáló
        /// 1. Utolsó projectjét visszatölti
        /// 2. Argumentumban jött útvonal és ezt jeleníti meg
        /// 3. Új Project.
        /// </summary>
        /// <param name="args">A fájl - hoz tartozó argumentumok.</param>
        public void Start(string[] args)
        {
#if DEBUG
            AppDiagService.WriteLine(this.GetType().Namespace + "." + this.GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + string.Join("\r\n -", args));
#endif
         if (args.Count() > 1 && System.IO.File.Exists(args[1]) && !args[1].Contains("UnitTest.nunit"))
            {
                /*fájlra klikkelt ezt betölti és megjeleníti*/
                try
                {
                    _storage.Load(args[1]);
                    ShowTraceInMainView();
                }
                catch
                {
                    /*Nem sikerült az utolsó projectet betölteni, ezért most egy Untitled project Indul*/
                    _storage.New(_adapterService.GetDefaultDeviceName, _adapterService.GetDefaultBaudrate);
                    ShowTraceInMainView();
                    /*Tovább dobja a hibaüzenete*/
                }
            }
            else
            {
                if (Settings.Default.LoadProjectOnAppllicationStart)
                {
                    try
                    {
                        if (System.IO.File.Exists(_workspaceService.LastOpenedProjectPath))
                        {
                            /*Sikerült az utolsó projectet betölteni*/
                            _storage.Load(_workspaceService.LastOpenedProjectPath);
                            ShowTraceInMainView();
                        }
                        else
                        {
                            /*Nem volt elöző porject, új projectet töltünk be.*/
                            _storage.New(_adapterService.GetDefaultDeviceName, _adapterService.GetDefaultBaudrate);
                            ShowTraceInMainView();
                        }
                    }
                    catch
                    {
                        /*Nem sikerült az utolsó projectet betölteni, ezért most egy Default project Indul*/
                        _storage.New(_adapterService.GetDefaultDeviceName, _adapterService.GetDefaultBaudrate);
                        ShowTraceInMainView();
                        /*Tovább dobja a hibaüzenete*/
                        throw;
                    }
                }
                else
                {
                    _storage.New(_adapterService.GetDefaultDeviceName, _adapterService.GetDefaultBaudrate);
                    ShowTraceInMainView();
                }
            }
            _mainForm.Status = "Started";
        }

        #region Show

        public void ShowLogInMainView()
        {
            MainForm.MainView.ShowLogView(_traceSenderMainToolbarCommands);
        }

        public void ShowTraceInMainView()
        {
            MainForm.MainView.ShowTraceView(_traceSenderMainToolbarCommands);
        }

        public void ShowWorksapce()
        {
            MainForm.MainView.ShowWorkspaceView(_workspaceMainToolbarCommands);
        }

        public void ShowStatistics()
        {
            MainForm.MainView.ShowStatisticsView(_traceSenderMainToolbarCommands);
        }

        public void ShowFilter()
        {
            MainForm.MainView.ShowFilterView(_traceSenderMainToolbarCommands);
        }

        #endregion
    }
}