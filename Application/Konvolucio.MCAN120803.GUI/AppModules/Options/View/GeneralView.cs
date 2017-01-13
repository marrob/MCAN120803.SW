

namespace Konvolucio.MCAN120803.GUI.AppModules.Options.View
{
    using System;
    using System.Diagnostics;
    using System.Windows.Forms;

    using Properties;
    using Services;

    public partial class GeneralView : UserControl, IOptionPage
    {
        public bool RequiedDefault
        {
            get { return false; }
        }

        public bool RequiedRestart
        {
            get { return Settings.Default.CurrentCultureName != comboBoxCultureName.Text; }
        }

        public GeneralView()
        {
            InitializeComponent();
            comboBoxCultureName.Items.AddRange(CultureService.Instance.SupportedCulutreNames);
            
        }

        private void buttonBrowse_Text_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog {SelectedPath = Settings.Default.BrowseLocation};

            if (fbd.ShowDialog() == DialogResult.OK)
            {
                textBoxBrowseLocation.Text = fbd.SelectedPath;
            }
        }

        public void Save()
        {
            Debug.WriteLine(this.GetType().Namespace + "." + this.GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name + "()");
            Settings.Default.CurrentCultureName = comboBoxCultureName.SelectedItem.ToString();
            Settings.Default.ShowWorkingDirectoryInTitleBar = checkBoxShowWorkingDirectory.Checked;
            Settings.Default.LoadProjectOnAppllicationStart = checkBoxLoadProjectOnAppStart.Checked;
            Settings.Default.EnableSingleInstance = checkBoxSingleInstanceApp.Checked;
            Settings.Default.BrowseLocation = textBoxBrowseLocation.Text;
        }

        /// <summary>
        /// Alaphelyzet
        /// </summary>
        public void UpdateValues() 
        {
            comboBoxCultureName.SelectedItem = Settings.Default.CurrentCultureName;
            checkBoxShowWorkingDirectory.Checked = Settings.Default.ShowWorkingDirectoryInTitleBar;
            checkBoxLoadProjectOnAppStart.Checked = Settings.Default.LoadProjectOnAppllicationStart;
            checkBoxSingleInstanceApp.Checked = Settings.Default.EnableSingleInstance;
            textBoxBrowseLocation.Text = Settings.Default.BrowseLocation;
        }

        public void Defualt()
        {

        }

    }
}
