
namespace Konvolucio.MCAN120803.GUI.AppModules.Main.View
{
    using System;
    using System.ComponentModel;
    using System.Windows.Forms;

    using Properties;
    using WinForms.Framework;
    using Log.View;
    using AppDiag.View;
    using Workspace.View;
    using Statistics.Message.View;
    using Filter.View;

    public interface IMainView: IUiLayoutRestoring
    {
        ITraceSenderView TraceAndPagesView { get; }
        ILogView LogView { get; }
        IWorkspaceView WorkspaceView { get; }
        KnvTreeView TreeView { get; }
        ToolStrip TreeToolStrip { get; }
        IAppDiagView AppDiag { get; }
        FiltersGridView DataGridView { get; }
        DockStyle Dock { get; set; }
        IStatisticsGridView StatisticsView { get; }

        void ShowTraceView(ToolStripMenuItem[] menuItems);
        void ShowLogView(ToolStripMenuItem[] menuItems);
        void ShowWorkspaceView(ToolStripMenuItem[] menuItems);
        void ShowStatisticsView(ToolStripMenuItem[] menuItems);
        void ShowFilterView(ToolStripMenuItem[] menuItems);

    }

    public partial class MainView : UserControl, IMainView
    {
        public ITraceSenderView TraceAndPagesView => _traceAndPagesView;
        public ILogView LogView => _logView;
        public IWorkspaceView WorkspaceView => _workspaceView;
        public IAppDiagView AppDiag => appTraceView1;
        public KnvTreeView TreeView => treeView1;
        public ToolStrip TreeToolStrip => toolStrip2;
        public IStatisticsGridView StatisticsView => _statisticsGridView;
        public FiltersGridView DataGridView => _dataGridView;
        public ToolStrip MainToolStrip => toolStrip1;

        private readonly TraceSenderView _traceAndPagesView;
        private readonly LogView _logView;
        private readonly WorkspaceView _workspaceView;
        private readonly StatisticsGridView _statisticsGridView;
        private readonly FiltersGridView _dataGridView;


        public MainView()
        {
            InitializeComponent();
            _traceAndPagesView = new TraceSenderView(){ Dock = DockStyle.Fill };
            _logView = new LogView() { Dock = DockStyle.Fill };
            _workspaceView = new WorkspaceView() { Dock = DockStyle.Fill };
            _statisticsGridView = new StatisticsGridView() {Dock = DockStyle.Fill,};
            _dataGridView = new FiltersGridView() {Dock = DockStyle.Fill,};

        }

        private void MainView_Load(object sender, EventArgs e)
        {
            splitContainerMainView.Panel2Collapsed = !Settings.Default.IsDeveloperMode;
            Settings.Default.PropertyChanged += new PropertyChangedEventHandler(Default_PropertyChanged);
        }

        void Default_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName ==  PropertyPlus.GetPropertyName( () => Settings.Default.IsDeveloperMode ))
            { 
                splitContainerMainView.Panel2Collapsed = !Settings.Default.IsDeveloperMode;
            }
        }

        public void ShowTraceView(ToolStripMenuItem[] menuItems)
        {
            if (!splitContainerMainView.Panel1.Controls.Contains(splitContainerMainTree))
            {
                splitContainerMainView.Panel1.Controls.Clear();
                splitContainerMainView.Panel1.Controls.Add(splitContainerMainTree);
            }

            splitContainerMainTree.Panel2.Controls.Clear();
            splitContainerMainTree.Panel2.Controls.Add(_traceAndPagesView);

            MainToolStrip.Items.Clear();
            MainToolStrip.Items.AddRange(menuItems);

        }

        public void ShowLogView(ToolStripMenuItem[] menuItems)
        {
            if (!splitContainerMainView.Panel1.Controls.Contains(splitContainerMainTree))
            { /*Workspace-en volt előtte*/
                splitContainerMainView.Panel1.Controls.Clear();
                splitContainerMainView.Panel1.Controls.Add(splitContainerMainTree);
            }

            if (!splitContainerMainTree.Panel2.Controls.Contains(_logView))
            { /*Nem villog*/
                splitContainerMainTree.Panel2.Controls.Clear();
                splitContainerMainTree.Panel2.Controls.Add(_logView);
            }
            MainToolStrip.Items.Clear();
            MainToolStrip.Items.AddRange(menuItems);
        }

        public void ShowWorkspaceView(ToolStripMenuItem[] menuItems)
        {
            splitContainerMainView.Panel1.Controls.Clear();
            splitContainerMainView.Panel1.Controls.Add(_workspaceView);

            MainToolStrip.Items.Clear();
            MainToolStrip.Items.AddRange(menuItems);
        }

        public void ShowStatisticsView(ToolStripMenuItem[] menuItems)
        {
            if (!splitContainerMainView.Panel1.Controls.Contains(splitContainerMainTree))
            { /*Workspace-en volt előtte*/
                splitContainerMainView.Panel1.Controls.Clear();
                splitContainerMainView.Panel1.Controls.Add(splitContainerMainTree);
            }

            if (!splitContainerMainTree.Panel2.Controls.Contains(_statisticsGridView))
            { /*Nem villog*/
                splitContainerMainTree.Panel2.Controls.Clear();
                splitContainerMainTree.Panel2.Controls.Add(_statisticsGridView);
            }
            MainToolStrip.Items.Clear();
            MainToolStrip.Items.AddRange(menuItems);
        }

        public void ShowFilterView(ToolStripMenuItem[] menuItems)
        {
            if (!splitContainerMainView.Panel1.Controls.Contains(splitContainerMainTree))
            { /*Workspace-en volt előtte*/
                splitContainerMainView.Panel1.Controls.Clear();
                splitContainerMainView.Panel1.Controls.Add(splitContainerMainTree);
            }

            if (!splitContainerMainTree.Panel2.Controls.Contains(_dataGridView))
            { /*Nem villog*/
                splitContainerMainTree.Panel2.Controls.Clear();
                splitContainerMainTree.Panel2.Controls.Add(_dataGridView);
            }
            MainToolStrip.Items.Clear();
            MainToolStrip.Items.AddRange(menuItems);
        }

        public void LayoutSave()
        {
            Settings.Default.splitContainerMainView_SplitterDistance = splitContainerMainView.SplitterPrecent;
            Settings.Default.splitContainerMainTree_SplitterDistance = splitContainerMainTree.SplitterPrecent;
            TraceAndPagesView.LayoutSave();
            AppDiag.LayoutSave();
            LogView.LayoutSave();
        }

        public void LayoutRestore()
        {
            splitContainerMainView.SplitterPrecent = Settings.Default.splitContainerMainView_SplitterDistance;
            splitContainerMainTree.SplitterPrecent = Settings.Default.splitContainerMainTree_SplitterDistance;
            TraceAndPagesView.LayoutRestore();
            AppDiag.LayoutRestore();
            LogView.LayoutRestore();
        }

        private void splitContainerMainView_SplitterMoved(object sender, SplitterEventArgs e)
        {
#if DEBUG
            AppDiagService.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + splitContainerMainView.SplitterDistance + ", Percent: " + splitContainerMainView.SplitterPrecent.ToString());
#endif  
        }

        private void splitContainerMainTree_SplitterMoved(object sender, SplitterEventArgs e)
        {
#if DEBUG
            AppDiagService.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + splitContainerMainTree.SplitterDistance + ", Percent: " + splitContainerMainTree.SplitterPrecent.ToString());
#endif
        }

    }
}
