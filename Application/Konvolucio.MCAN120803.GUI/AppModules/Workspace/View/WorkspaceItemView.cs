

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


    public interface IWorkspaceItemView
    {
        WorkspaceItem Item { get; }
    }

    public partial class WorkspaceItemView : UserControl, IWorkspaceItemView
    {
        public WorkspaceItem Item { get; private set; }
        public bool Selected 
        {
            get { return selected; }
            set 
            {
                if (selected != value)
                {
                    selected = value;
                    if (selected)
                    {
                        textBoxPath.BackColor = Color.Orange;
                        textBoxComment.BackColor = Color.Orange;
                    }
                    else
                    {
                        textBoxPath.BackColor = SystemColors.Control;
                        textBoxComment.BackColor = SystemColors.Control;
                    }
                }
            }
        }
        bool selected;

        /****************************************************************/
        public WorkspaceItemView()
        {
            InitializeComponent();
        }
        /****************************************************************/
        public WorkspaceItemView(WorkspaceItem value)
        {
            InitializeComponent();
            Item = value;
            Update(value);
        }
        /****************************************************************/
        void Update(WorkspaceItem value)
        {
            toolStripLabel1.Text = value.FileName;
            textBoxPath.Text = value.ItemFilePath;
            textBoxComment.Text = value.Comment;
        }
        /****************************************************************/
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Item.CloseItem();
        }
        /****************************************************************/
        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
            if(Item.IsValid)
                Item.OpenItem();
        }
    }
}
