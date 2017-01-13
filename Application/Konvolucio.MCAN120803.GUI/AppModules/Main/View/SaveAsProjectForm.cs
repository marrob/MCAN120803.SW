
namespace Konvolucio.MCAN120803.GUI.AppModules.Main.View
{
    using System;
    using System.ComponentModel;
    using System.Configuration;
    using System.IO;
    using System.Windows.Forms;
    using Properties;
    using Services;
    using WinForms.Framework;

/*IProjectService*/



    public interface ISaveProjectForm
    {
        string FullPath { get; }
        string ProudctName { get; set; }
        string PrjProductVersion { get; set; }
        string ProcutCode { get; set; }
        string CustomerName { get; set; }
        string CustomerCode { get; set; }
        DialogResult ShowDialog();
    }
  
    public partial class SaveAsProjectForm : Form, ISaveProjectForm
    {
        public string FullPath { get { return textBoxPath.Text; } }
        public string ProudctName 
        {
            get { return textBoxProductName.Text; }
            set { textBoxProductName.Text = value; }
        }
        public string PrjProductVersion
        {
            get { return textBoxProductVersion.Text; }
            set { textBoxProductVersion.Text = value; }
        }
        public string ProcutCode
        {
            get { return textBoxProductCode.Text; }
            set { textBoxProductCode.Text = value; }
        }
        public string CustomerName 
        { 
            get { return textBoxCustomerName.Text; }
            set { textBoxCustomerName.Text = value; }
        }
        public string CustomerCode 
        {
            get { return textBoxCustomerCode.Text; }
            set { textBoxCustomerCode.Text = value; }
        }

        /*private readonly ErrorProvider _errorProviderProductName;*/
 
        public SaveAsProjectForm()
        {
            InitializeComponent();

            textBoxProductName.Text = AppConstants.ValueNotAvailable2;
            textBoxProductName.MaxLength = AppConstants.MaxProductNameLenght;
            
            /*
            textBoxProductName.Tag = PropertyPlus.GetPropertyName(() => ProductName);
            _errorProviderProductName = new ErrorProvider()
            {
                BlinkRate = 1000,
                BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.AlwaysBlink,
            };
            textBoxProductName.Validated += new EventHandler(textBoxProductName_Validated);
            */

            textBoxProductVersion.Text = AppConstants.ValueNotAvailable2;
            textBoxProductVersion.MaxLength = AppConstants.MaxProductVersionLenght;

            textBoxProductCode.Text = AppConstants.ValueNotAvailable2;
            textBoxProductCode.MaxLength = AppConstants.MaxProductCodeLenght;

            textBoxCustomerName.Text = AppConstants.ValueNotAvailable2;
            textBoxCustomerName.MaxLength = AppConstants.MaxCustomerNameLenght;

            textBoxCustomerCode.Text = AppConstants.ValueNotAvailable2;
            textBoxCustomerCode.MaxLength = AppConstants.MaxCustomerCodeLenght;
   
            buttonOk.Enabled = false;
            checkBoxSubdirectory.Checked = true;
            textBoxLocation.Text = Settings.Default.BrowseLocation;

        
        }
        /*
        void textBoxProductName_Validated(object sender, EventArgs e)
        {
            var errMsg = Validator(porpertyName: (string)(((TextBox)sender).Tag), value: ((TextBox)sender).Text);
            if (errMsg != null)
                _errorProviderProductName.SetError((Control)sender, errMsg);
            else
                _errorProviderProductName.Clear();
        }
         * */


        /*
        private string Validator(string porpertyName, string value)
        {
            string retval = null;

            if(porpertyName == PropertyPlus.GetPropertyName(()=>ProductName))
            {
                if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
                    retval = "Nem lehet üres";
                if (value != null && value == AppConstants.ValueNotAvailable)
                    retval = "Adj meg egy nevet";
                if (value != null && value.Length > AppConstants.MaxProductNameLenght)
                    retval = "Túl hosszú.";
            }
            return retval;
        }
        */

        /// <summary>
        /// Okézhathó, ha van érvényes ProductName
        /// </summary>
        private static bool IsOkEnabled(string prodouctName, string name)
        {
            bool retval;

            if (!string.IsNullOrEmpty(prodouctName))
                prodouctName = prodouctName.Trim();

            if (!string.IsNullOrEmpty(name))
                prodouctName = name.Trim();

            if (!string.IsNullOrEmpty(prodouctName) && (prodouctName != AppConstants.ValueNotAvailable2) &&  !(string.IsNullOrEmpty(name)) &&  name != AppConstants.ValueNotAvailable2)
                retval = true;
            else
                retval = false;

            return retval;
        }

        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            var fbd = new FolderBrowserDialog {SelectedPath = Settings.Default.BrowseLocation};

            if (fbd.ShowDialog() == DialogResult.OK)
            {
                textBoxLocation.Text = fbd.SelectedPath;
                textBoxPath.Text = PathService.Instance.CreateNewProjectPath(fbd.SelectedPath, textBoxProductName.Text, textBoxName.Text, checkBoxSubdirectory.Checked);
            }
        }      

        private void buttonOk_Click(object sender, EventArgs e)
        {
            var fullPath= string.Empty;
            
            try
            {
                fullPath = PathService.Instance.CreateNewProjectPath(textBoxLocation.Text, textBoxProductName.Text, textBoxName.Text, checkBoxSubdirectory.Checked);
                string result = null;

                if((result = ConsistencyCheck.FileName(textBoxName.Text)) != null)
                    throw new ArgumentException(result, @"File Name");

                if (Directory.Exists(Path.GetDirectoryName(fullPath)) == false)
                    Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

                if (Directory.Exists(Path.GetDirectoryName(fullPath)) == false)
                    MessageBox.Show(CultureService.Instance.GetString(CultureText.text_DirectoryCannotBeCreated) + "\n" + Path.GetDirectoryName(fullPath), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                if (File.Exists(fullPath))
                {
                    MessageBox.Show(CultureService.Instance.GetString(CultureText.text_FileAlreadyExists) + "\n" + fullPath, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    DialogResult = DialogResult.OK;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void checkBoxSubdirectory_CheckedChanged(object sender, EventArgs e)
        {
            textBoxPath.Text = PathService.Instance.CreateNewProjectPath(textBoxLocation.Text, textBoxProductName.Text, textBoxName.Text, checkBoxSubdirectory.Checked);
            buttonOk.Enabled = IsOkEnabled(textBoxProductName.Text, textBoxName.Text);
        }

        private void textBoxLocation_TextChanged(object sender, EventArgs e)
        {
            textBoxPath.Text = PathService.Instance.CreateNewProjectPath(textBoxLocation.Text, textBoxProductName.Text, textBoxName.Text, checkBoxSubdirectory.Checked);
            buttonOk.Enabled = IsOkEnabled(textBoxProductName.Text, textBoxName.Text);
        }

        private void textBoxName_TextChanged(object sender, EventArgs e)
        {
            textBoxPath.Text = PathService.Instance.CreateNewProjectPath(textBoxLocation.Text, textBoxProductName.Text, textBoxName.Text, checkBoxSubdirectory.Checked);
            buttonOk.Enabled = IsOkEnabled(textBoxProductName.Text, textBoxName.Text);
        }

        private void textBoxProductName_TextChanged(object sender, EventArgs e)
        {
            textBoxName.Text = textBoxProductName.Text;
            textBoxPath.Text = PathService.Instance.CreateNewProjectPath(textBoxLocation.Text, textBoxProductName.Text, textBoxName.Text, checkBoxSubdirectory.Checked);
            buttonOk.Enabled = IsOkEnabled(textBoxProductName.Text, textBoxName.Text);
        }


    }
}
