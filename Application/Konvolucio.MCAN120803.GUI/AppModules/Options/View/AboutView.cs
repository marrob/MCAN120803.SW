
namespace Konvolucio.MCAN120803.GUI.AppModules.Options.View
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Microsoft.Win32;    /*Required for the registry classes.*/
    using System.IO;        /*Required for the Path class.*/
    using System.Diagnostics;    /*Required for the process classes.*/
    using System.Reflection;
    using WinForms.Framework;

    /// <summary>
    /// 
    /// </summary>
    public partial class AboutView : UserControl, IOptionPage
    {
        public bool RequiedRestart
        {
            get { return false; }
        }

        public bool RequiedDefault
        {
            get { return false; }
        }

        /// <summary>
        /// 
        /// </summary>
        public AboutView()
        {
            InitializeComponent();
        }

        public void Save() { }

        public void UpdateValues() { }

        public void Defualt() { }

        private void buttonSystemInfo_Click(object sender, EventArgs e)
        {
            string strSysInfo = string.Empty;
            if (GetMsinfo32Path(ref strSysInfo))
                StartSysInfo(strSysInfo);
        }

        private void StartSysInfo(string strSysInfo)
        {
            try
            {
                Process.Start(strSysInfo);
            }
            catch (Win32Exception ex)
            {
                MessageBox.Show(this, ex.Message, Application.ProductName,
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strPath"></param>
        /// <returns></returns>
        private bool GetMsinfo32Path(ref string strPath)
        {
            strPath = string.Empty;
            object objTmp = null;
            RegistryKey regKey = Registry.LocalMachine;

            if (regKey != null)
            {
                regKey = regKey.OpenSubKey(
                     "Software\\Microsoft\\Shared Tools\\MSInfo");
                if (regKey != null)
                    objTmp = regKey.GetValue("Path");

                if (objTmp == null)
                {
                    regKey = regKey.OpenSubKey(
                       "Software\\Microsoft\\Shared Tools Location");
                    if (regKey != null)
                    {
                        objTmp = regKey.GetValue("MSInfo");
                        if (objTmp != null)
                            strPath = Path.Combine(
                               objTmp.ToString(), "MSInfo32.exe");
                    }
                }
                else
                    strPath = objTmp.ToString();

                try
                {
                    FileInfo fi = new FileInfo(strPath);
                    return fi.Exists;
                }
                catch (ArgumentException)
                {
                    strPath = string.Empty;
                }
            }

            return false;
        }

        /// <summary>
        /// Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OptionsAboutView_Load(object sender, EventArgs e)
        {
            AppDiagService.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name);

            labelCompanyName.Text = String.Format("{0}", Application.CompanyName); 
            labelProductName.Text = String.Format("Prdouct: {0}", Application.ProductName);
            labelProductVersion.Text = "Version: " + Application.ProductVersion;

            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

            string line = string.Empty;
            foreach (var asm in assemblies)
            {
               AssemblyName asmName = asm.GetName();
               line = asmName.Name + " Version:" + asmName.Version;
               richTextBox1.AppendText(line, Color.LightBlue, true);
            }
        }
    }
}
