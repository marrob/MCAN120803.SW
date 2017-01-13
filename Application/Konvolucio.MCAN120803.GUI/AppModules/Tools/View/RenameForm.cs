
namespace Konvolucio.MCAN120803.GUI.AppModules.Tools.View
{
    using System;
    using System.Windows.Forms;

    public partial class RenameForm : Form, IRenameForm
    {
        public string NewName
        {
            get { return textBox1.Text; }
            set { textBox1.Text = value; }
        }

        public RenameForm()
        {
            InitializeComponent();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
    }
}
