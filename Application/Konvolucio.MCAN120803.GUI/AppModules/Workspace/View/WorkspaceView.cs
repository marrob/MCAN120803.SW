

namespace Konvolucio.MCAN120803.GUI.AppModules.Workspace.View
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms;

    using Model;

    public interface IWorkspaceView
    {
        IWorkspaceCollection DataSource { get; set; }
    }

    public partial class WorkspaceView : UserControl, IWorkspaceView
    {
       [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IWorkspaceCollection DataSource
        {
            get { return dataSource; }
            set 
            {
                if (dataSource != value)
                { 
                    dataSource = value;
                    DataSourceChanged();
                }
            }
        }

        IWorkspaceCollection dataSource;

        public WorkspaceView()
        {
            InitializeComponent();
        }

        private void DataSourceChanged()
        {
            (dataSource as WorkspaceCollection).ProjectCloseing += new EventHandler(WorkspaceView_ItemCloseing);
            (dataSource as WorkspaceCollection).ItemAdded += new EventHandler(WorkspaceView_ItemAdded);

            flowLayoutPanel1.Controls.Clear();
            if (dataSource != null)
            {
                foreach (var item in dataSource as WorkspaceCollection)
                {
                    IWorkspaceItemView control = new WorkspaceItemView(item);
                    flowLayoutPanel1.Controls.Add(control as WorkspaceItemView);
                }
            }
        }

        void WorkspaceView_ItemCloseing(object sender, EventArgs e)
        {
            var doRemoveItem = sender as WorkspaceItem;
            var controls = flowLayoutPanel1.Controls.Cast<IWorkspaceItemView>();
            var doRemoveControl = controls.First(n => n.Item == doRemoveItem);
            flowLayoutPanel1.Controls.Remove(doRemoveControl as WorkspaceItemView);
        }

        void WorkspaceView_ItemAdded(object sender, EventArgs e)
        {
            flowLayoutPanel1.Controls.Add(new WorkspaceItemView((sender as WorkspaceItem)));
        }
    }
}
