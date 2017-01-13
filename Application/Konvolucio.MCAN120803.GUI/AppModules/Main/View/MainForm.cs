
namespace Konvolucio.MCAN120803.GUI.AppModules.Main.View
{
    using System;
    using System.ComponentModel;
    using System.Windows.Forms;
    
    using Properties;
    using WinForms.Framework;

    public interface IMainForm: IUiLayoutRestoring
    {
        event FormClosedEventHandler FormClosed;
        event FormClosingEventHandler FormClosing;
        event EventHandler Disposed;
       
        event EventHandler Shown;
        event KeyEventHandler KeyUp;
        event HelpEventHandler HelpRequested; /*????*/

        IMainView MainView { get; }
        string Text { get; set; }
        string Status { get; set; }

        void ProgressBarUpdate(ProgressChangedEventArgs arg);
        void Close();

        void CursorWait();
        void CursorDefault();
    }

    public partial class MainForm : Form, IMainForm
    {

        public IMainView MainView { get { return mainView; } }


        
        public string Status
        {
            get { return toolStripStatusLabel1.Text; }
            set 
            {
                readyTimer.Stop();
                readyTimer.Start();
                toolStripStatusLabel1.Text = value;
                toolStripProgressBar1.Visible = false;
            }
        }
      
        public new string Text
        {
            get { return base.Text; }
            set
            {
                value += " " + Application.ProductVersion;
                if (InvokeRequired)
                    this.Invoke((MethodInvoker)delegate{ base.Text = value; });
                else
                    base.Text = value;
            }
        }
        
        readonly MainView mainView;
        readonly Timer readyTimer;

        /// <summary>
        /// Konstruktor
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            readyTimer = new Timer();
            readyTimer.Interval = 2000;
            readyTimer.Tick += new EventHandler(readyTimer_Tick);
            mainView = new MainView() {Dock = DockStyle.Fill };
        }

        void readyTimer_Tick(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Ready";
        }

        /// <summary>
        /// Az ablak bal alsó sarkába Ready-re állítja a szöveget
        /// </summary>
        /// <param name="arg"></param>
        public void ProgressBarUpdate(ProgressChangedEventArgs arg)
        {
            Action act = () =>
                {
                    readyTimer.Stop();
                    readyTimer.Start();

                    if (arg != null)
                    {
                        if (arg.UserState != null)
                        {
                            toolStripStatusLabel1.Text = arg.UserState.ToString();
                            System.Windows.Forms.Application.DoEvents();
                        }
                        if (arg.ProgressPercentage > 0 && arg.ProgressPercentage != 100)
                        {
                            toolStripProgressBar1.Visible = true;
                            toolStripProgressBar1.Value = arg.ProgressPercentage;
                        }
                        else
                        {
                            toolStripProgressBar1.Visible = false;
                            toolStripProgressBar1.Value = 0;
                        }
                    }
                    else
                    {
                        toolStripStatusLabel1.Text = "null";
                    }
                };
            if (InvokeRequired)
                this.Invoke((MethodInvoker)delegate {act();});
            else
                act();
        }


        /// <summary>
        /// Defaultban ezt történik
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            panel1.Controls.Add(mainView);
        }

        /// <summary>
        /// Mensd a ennek a vezérlő állapotát
        /// </summary>
        public void LayoutSave()
        {
            Settings.Default.MainFormLocation = Location;
            Settings.Default.MainFormWindowState = WindowState;
            Settings.Default.MainFormSize = Size;
            MainView.LayoutSave();
        }
        /// <summary>
        /// Állisd vissza ennek a vezélrlőnek az állapotát
        /// </summary>
        public void LayoutRestore()
        {
            Location = Settings.Default.MainFormLocation;
            WindowState = Settings.Default.MainFormWindowState;
            Size = Settings.Default.MainFormSize;
            MainView.LayoutRestore();
        }

        /// <summary>
        /// 
        /// </summary>
        public void CursorWait()
        {
            Cursor.Current = Cursors.WaitCursor;
        }

        /// <summary>
        /// 
        /// </summary>
        public void CursorDefault()
        {
            Cursor.Current = Cursors.Default;
        }
    }
}
