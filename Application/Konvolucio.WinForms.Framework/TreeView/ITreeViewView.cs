
namespace Konvolucio.WinForms.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Linq;
    using System.Windows.Forms;
    [Obsolete]
    public interface xITreeViewView
    {
        event TreeViewEventHandler SelectedChanged;
        event TreeViewEventHandler AfterExpand;
        event TreeViewEventHandler AfterCollapse;
        event TreeNodeMouseClickEventHandler NodeMouseClick;
       

        ImageList ImageList { get; set; }
        ContextMenuStrip Menu { get; }
        TreeNodeCollection Nodes { get; }
        TreeNode SelectedNode { get; set; }
        bool Enabled { get; set; }
    }
    [Obsolete]
    public partial class xTreeViewView : UserControl,xITreeViewView
    {



        public event TreeViewEventHandler SelectedChanged
        {
            add { treeView1.AfterSelect += value; }
            remove { treeView1.AfterSelect -= value; }
        }

        public event TreeViewEventHandler AfterExpand
        {
            add { treeView1.AfterExpand += value; }
            remove { treeView1.AfterExpand -= value; }
        }

              
        public event TreeViewEventHandler AfterCollapse
        {
            add { treeView1.AfterCollapse += value; }
            remove { treeView1.AfterCollapse -= value; }
        }


        public event TreeNodeMouseClickEventHandler NodeMouseClick
        {
            add { treeView1.NodeMouseClick += value; }
            remove { treeView1.NodeMouseClick -= value; }
        }

        public ImageList ImageList
        {
            get { return treeView1.ImageList; }
            set { treeView1.ImageList = value; }
        }

        public ContextMenuStrip Menu { get { return _menu; } }
        readonly ContextMenuStrip _menu;

        public TreeNodeCollection Nodes 
        {
            get { return treeView1.Nodes; }
        }

        public TreeNode SelectedNode 
        {
            get { return treeView1.SelectedNode; }
            set { treeView1.SelectedNode = value; }
        }

        public xTreeViewView()
        {
            InitializeComponent();
            treeView1.ShowNodeToolTips = true;
            _menu = new ContextMenuStrip();

            
        }
        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                _menu.Show(treeView1, new Point(e.X, e.Y));
            }
        }

        void treeView1_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
