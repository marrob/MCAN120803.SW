namespace Konvolucio.MCAN120803.GUI.AppModules.Log.View
{
    using System;
    using System.Windows.Forms;
    using Services;



    public partial class LogDescriptionView : UserControl, ILogDescriptionView
    {

        public string Content
        {
            get
            {
                return richTextBox1.Text;
            }
            set
            {
                richTextBox1.Text = value; 
            }
        }

        public string LogName
        {
            get
            {
                return toolStripLabel2.Text;
            }
            set
            {
                toolStripLabel2.Text = value; 
            }
        }

        public LogDescriptionView()
        {
            InitializeComponent();
        }

        private void LogDescriptionView_Load(object sender, EventArgs e)
        {
            UpdateLocalization();
        }

        private void UpdateLocalization()
        {
            toolStripLabelName.Text = CultureService.Instance.GetString(CultureText.text_DESCRIPTION);
            richTextBox1.BackgroundText = CultureService.Instance.GetString(CultureText.text_DESCRIPTION);
        }
    }
}
