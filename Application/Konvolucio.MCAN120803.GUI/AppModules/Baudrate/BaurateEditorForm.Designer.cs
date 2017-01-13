namespace Konvolucio.MCAN120803.GUI.AppModules.Baudrate
{
    partial class BaurateEditorForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BaurateEditorForm));
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.customBaudrateView1 = new View.BaudrateEditorView();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            resources.ApplyResources(this.buttonOK, "buttonOK");
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // customBaudrateView1
            // 
            this.customBaudrateView1.Brp = 1;
            this.customBaudrateView1.CustomBaudrateValue = "";
            this.customBaudrateView1.ExpectedBaudrate = double.NaN;
            resources.ApplyResources(this.customBaudrateView1, "customBaudrateView1");
            this.customBaudrateView1.Name = "customBaudrateView1";
            this.customBaudrateView1.Sjw = 1;
            this.customBaudrateView1.Tseg1 = 1;
            this.customBaudrateView1.Tseg2 = 1;
            this.customBaudrateView1.ParameterChanged += new System.EventHandler(this.customBaudrateView1_ParameterChanged);
            this.customBaudrateView1.ExpectedBaudRateChanged += new System.EventHandler(this.customBaudrateView1_ExpectedBaudRateChanged);
            this.customBaudrateView1.CustomBaudRateChanged += new System.EventHandler(this.customBaudrateView1_CustomBaudrateChanged);
            // 
            // BaurateEditorForm
            // 
            this.AcceptButton = this.buttonOK;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.customBaudrateView1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BaurateEditorForm";
            this.ShowInTaskbar = false;
            this.TopMost = true;
            this.ResumeLayout(false);

        }

        #endregion

        private View.BaudrateEditorView customBaudrateView1;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;

    }
}