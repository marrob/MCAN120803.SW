
namespace Konvolucio.WinForms.Framework.DataGridViewControl.UnitTest.View
{
    using System;
    using System.Windows.Forms;

    public partial class NewPersonForm : Form, INewPersonForm
    {
        public string FirstName
        {
            get { return textBox1.Text; }
            set { textBox1.Text = value; }
        }

        public string LastName {
            get { return textBox2.Text; }
            set { textBox2.Text = value; }
        }
        public int Age 
        {
            get { return (int)numericUpDown1.Value; }
            set { numericUpDown1.Value = value; }
        }

        public NewPersonForm()
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
