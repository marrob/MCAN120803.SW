
namespace Konvolucio.WinForms.Framework.MultiPage.UnitTest.View
{
    using System;
    using System.Windows.Forms;

    partial class NewForm : Form, INewForm
    {
        public string NewName
        {
            get { return textBox1.Text; }
            set { textBox1.Text = value; }
        }

        public NewForm()
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
