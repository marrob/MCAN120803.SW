
namespace Konvolucio.MCAN120803.GUI.AppModules.Tools.View
{
    using System;
    using System.Linq;
    using System.Windows.Forms;
    using Common;

    public partial class NewForm : Form, INewForm
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

        public ToolTypes SelectedType
        {
            get { return (ToolTypes)Enum.Parse(typeof(ToolTypes),comboBox1.Text); }
            set { comboBox1.Text = value.ToString(); }
        }

        public string[] Types
        {
            get { return comboBox1.Items.Cast<string>().ToArray<string>(); }
            set { if (value != null) comboBox1.Items.AddRange(value); }
        }
    }
}
