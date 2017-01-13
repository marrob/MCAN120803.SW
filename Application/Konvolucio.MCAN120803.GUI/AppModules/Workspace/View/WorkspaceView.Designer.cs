namespace Konvolucio.MCAN120803.GUI.AppModules.Workspace.View
{
    partial class WorkspaceView
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.projectItemView1 = new Konvolucio.MCAN120803.GUI.AppModules.Workspace.View.WorkspaceItemView();
            this.projectItemView5 = new Konvolucio.MCAN120803.GUI.AppModules.Workspace.View.WorkspaceItemView();
            this.projectItemView2 = new Konvolucio.MCAN120803.GUI.AppModules.Workspace.View.WorkspaceItemView();
            this.projectItemView3 = new Konvolucio.MCAN120803.GUI.AppModules.Workspace.View.WorkspaceItemView();
            this.projectItemView4 = new Konvolucio.MCAN120803.GUI.AppModules.Workspace.View.WorkspaceItemView();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.White;
            this.flowLayoutPanel1.Controls.Add(this.projectItemView1);
            this.flowLayoutPanel1.Controls.Add(this.projectItemView5);
            this.flowLayoutPanel1.Controls.Add(this.projectItemView2);
            this.flowLayoutPanel1.Controls.Add(this.projectItemView3);
            this.flowLayoutPanel1.Controls.Add(this.projectItemView4);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(865, 476);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // projectItemView1
            // 
            this.projectItemView1.BackColor = System.Drawing.Color.Orange;
            this.projectItemView1.Location = new System.Drawing.Point(3, 3);
            this.projectItemView1.Name = "projectItemView1";
            this.projectItemView1.Selected = false;
            this.projectItemView1.Size = new System.Drawing.Size(812, 58);
            this.projectItemView1.TabIndex = 0;
            // 
            // projectItemView5
            // 
            this.projectItemView5.AutoScroll = true;
            this.projectItemView5.BackColor = System.Drawing.Color.Orange;
            this.projectItemView5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.projectItemView5.Location = new System.Drawing.Point(3, 67);
            this.projectItemView5.Name = "projectItemView5";
            this.projectItemView5.Selected = false;
            this.projectItemView5.Size = new System.Drawing.Size(812, 0);
            this.projectItemView5.TabIndex = 4;
            // 
            // projectItemView2
            // 
            this.projectItemView2.BackColor = System.Drawing.Color.Orange;
            this.projectItemView2.Location = new System.Drawing.Point(3, 73);
            this.projectItemView2.Name = "projectItemView2";
            this.projectItemView2.Selected = false;
            this.projectItemView2.Size = new System.Drawing.Size(812, 57);
            this.projectItemView2.TabIndex = 1;
            // 
            // projectItemView3
            // 
            this.projectItemView3.BackColor = System.Drawing.Color.Orange;
            this.projectItemView3.Location = new System.Drawing.Point(3, 136);
            this.projectItemView3.Name = "projectItemView3";
            this.projectItemView3.Selected = false;
            this.projectItemView3.Size = new System.Drawing.Size(812, 56);
            this.projectItemView3.TabIndex = 2;
            // 
            // projectItemView4
            // 
            this.projectItemView4.BackColor = System.Drawing.Color.Orange;
            this.projectItemView4.Location = new System.Drawing.Point(3, 198);
            this.projectItemView4.Name = "projectItemView4";
            this.projectItemView4.Selected = false;
            this.projectItemView4.Size = new System.Drawing.Size(812, 59);
            this.projectItemView4.TabIndex = 3;
            // 
            // WorkspaceView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "WorkspaceView";
            this.Size = new System.Drawing.Size(865, 476);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private WorkspaceItemView projectItemView5;
        private WorkspaceItemView projectItemView1;
        private WorkspaceItemView projectItemView2;
        private WorkspaceItemView projectItemView3;
        private WorkspaceItemView projectItemView4;

    }
}
