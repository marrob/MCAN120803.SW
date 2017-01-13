
namespace Konvolucio.MCAN120803.GUI.AppModules.Options
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    using Properties;   /*ApplicationSettings*/
    using View;
    using Services;     /*Culture*/

    public partial class OptionsForm : Form
    {

       private readonly GeneralView _genaralPanel;
       private readonly ProjectlCustomArbIdColumnsView _projectCustomArbIdColumnsPanel;

       private readonly ProjectView _projectPanel;
       private readonly ProjectRepresentationView _projectRepresentationPanel;
       private readonly ProjectFunctionsView _projectFunctionsPanel;

       private readonly DeveloperView _developerPanel;
       private readonly AboutView _aboutPanel;

       private readonly UserControl[] _options;

        /// <summary>
        /// Konstructor
        /// </summary>
        public OptionsForm()
        {
            InitializeComponent();

            _options = new UserControl[] 
            {
                _genaralPanel = new GeneralView() { Dock = DockStyle.Fill },
          
                _projectPanel = new ProjectView() { Dock = DockStyle.Fill },
                _projectRepresentationPanel = new ProjectRepresentationView() { Dock = DockStyle.Fill},
                _projectFunctionsPanel = new ProjectFunctionsView() { Dock = DockStyle.Fill},
                _projectCustomArbIdColumnsPanel = new ProjectlCustomArbIdColumnsView() {Dock = DockStyle.Fill},
                
                _developerPanel = new DeveloperView() { Dock = DockStyle.Fill, /*BackColor = Color.Yellow*/ },
                _aboutPanel = new AboutView() { Dock = DockStyle.Fill },
            }; 
        }

        /// <summary>
        /// Betöltés után
        /// </summary>
        private void OptionsForm_Load(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            panel1.Controls.Add(_genaralPanel);
        }

        /// <summary>
        /// Megelenés után frissíti az értékeket ha kell valakinek
        /// </summary>
        private void OptionsForm_Shown(object sender, EventArgs e)
        {
            foreach (var userControl in _options)
            {
                var item = (IOptionPage) userControl;
                item.UpdateValues();
            }
        }

        /// <summary>
        /// Mentés
        /// Megnézi kell e valaki miatt restelni, majd ment és ezt követőne rákérdez a reszetre.
        /// </summary>
        private void buttonOk_Click(object sender, EventArgs e)
        {
            
            var defaultRequied = false;
            var restartRequied = false;

            foreach (var userControl in _options)
            {
                var item = (IOptionPage) userControl;
                defaultRequied |= item.RequiedDefault;
            }

            foreach (var userControl in _options)
            {
                var item = (IOptionPage) userControl;
                restartRequied |= item.RequiedRestart;
            }

            if (defaultRequied)
            {
                foreach (var userControl in _options)
                {
                    var item = (IOptionPage) userControl;
                    item.Defualt();
                }
            }
            else
            {
                foreach (var userControl in _options)
                {
                    var item = (IOptionPage) userControl;
                    item.Save();
                }
            }

            Settings.Default.Save();

            if (restartRequied)
            {
                if (MessageBox.Show(
                                CultureService.Instance.GetString(CultureText.msgBox_RestartRequiredDoYouWantNow),
                                Application.ProductName, MessageBoxButtons.YesNo,
                                MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.Yes)
                {
                    Application.Restart();
                }
            }

            Close();
        }

        /// <summary>
        /// Kliép mentés nélkül
        /// </summary>
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Kiválasztja a panelt Node.Name alpján
        /// </summary>
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            switch (e.Node.Name as string)
            {

                case "GeneralNode": 
                    {
                        if (!panel1.Controls.Contains(_genaralPanel))
                        {
                            panel1.Controls.Clear();
                            panel1.Controls.Add(_genaralPanel);
                        }
                        break;
                    }

                case "ProjectNode":
                    {
                        if (!panel1.Controls.Contains(_projectPanel))
                        {
                            panel1.Controls.Clear();
                            panel1.Controls.Add(_projectPanel);
                        }
                        break;
                    }

                case "ProjectRepresentationNode":
                    {
                        if (!panel1.Controls.Contains(_projectRepresentationPanel))
                        {
                            panel1.Controls.Clear();
                            panel1.Controls.Add(_projectRepresentationPanel);
                        }
                        break;
                    }

                case "ProjectFunctionsNode":
                    {
                        if (!panel1.Controls.Contains(_projectFunctionsPanel))
                        {
                            panel1.Controls.Clear();
                            panel1.Controls.Add(_projectFunctionsPanel);
                        }
                        break;
                    }

                case "ProjectCustomArbIdColumnsNode":
                    {
                        if (!panel1.Controls.Contains(_projectCustomArbIdColumnsPanel))
                        {
                            panel1.Controls.Clear();
                            panel1.Controls.Add(_projectCustomArbIdColumnsPanel);
                        }
                        break;
                    }

                case "DeveloperNode":
                    {
                        if (!panel1.Controls.Contains(_developerPanel))
                        {
                            panel1.Controls.Clear();
                            panel1.Controls.Add(_developerPanel);
                        }
                        break;
                    }
                case "AboutNode":
                    {
                        if (!panel1.Controls.Contains(_aboutPanel))
                        {
                            panel1.Controls.Clear();
                            panel1.Controls.Add(_aboutPanel);
                        }
                        break;
                    }
            }
        }
        /// <summary>
        /// How to prevent a form object from disposing on close?
        /// http://stackoverflow.com/questions/6060471/how-to-prevent-a-form-object-from-disposing-on-close
        /// </summary>
        private void OptionsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true; // this cancels the close event.
        }
    }
}
