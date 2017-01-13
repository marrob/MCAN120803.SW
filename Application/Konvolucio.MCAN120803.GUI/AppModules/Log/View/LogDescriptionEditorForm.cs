
namespace Konvolucio.MCAN120803.GUI.AppModules.Log.View
{
    using System;
    using System.Windows.Forms;



    public partial class LogDescriptionEditorForm : Form, ILogDescriptionEditorForm
    {

        public string Content
        {
            get 
            {
                return richTextBoxWithBackgorundLabel1.Text;
            }
            set
            {
                richTextBoxWithBackgorundLabel1.Text = value;
            }
        }

        public LogDescriptionEditorForm()
        {
            InitializeComponent();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }
    }
}
