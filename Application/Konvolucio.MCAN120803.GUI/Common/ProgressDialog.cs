using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Konvolucio.MCAN120803.GUI
{
    public interface IProgressDialog
    {
        event EventHandler UserCanceled;
        string Comment { get; set; }
        bool AllowCancel { get; set; }

        void Show();
        void Start();
        void End();
    }

    /// <summary>
    /// 
    /// </summary>
    public partial class ProgressDialog : Form, IProgressDialog
    {
        public event EventHandler UserCanceled;

        public string Comment
        {
            get { return label1.Text; }
            set { label1.Text = value; }
        }

        public bool AllowCancel { get; set; }

        public ProgressDialog()
        {
            InitializeComponent();
        }


        public void Start()
        {
            progressBar1.Style = ProgressBarStyle.Marquee;
        }

        public void End()
        {
            Timer windowHoldTimer = new Timer();
            windowHoldTimer.Interval = 1000;
            windowHoldTimer.Start();
            windowHoldTimer.Tick += (s, e) =>
                {
                    windowHoldTimer.Stop();
                    this.Close();
                };
        }

        private void ProgressDialog_Shown(object sender, EventArgs e)
        {
            buttonCancel.Visible = AllowCancel;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            if (UserCanceled != null)
                UserCanceled(this, EventArgs.Empty);
        }
    }
}
