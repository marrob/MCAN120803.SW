

namespace Konvolucio.MCAN120803.GUI.AppModules.AppDiag.View
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms;

    using Properties;
    using WinForms.Framework;
    using Services; /*Culutre*/

    public interface IAppDiagView: IUiLayoutRestoring
    {
        int LineCounter { get; }
        void WriteLine(string line);
        ContextMenuStrip Menu { get; }
        void Clear();
    }

    /// <summary>
    /// 
    /// </summary>
    public partial class AppDiagnosticsView : UserControl, IAppDiagView
    {
        public int LineCounter { get; private set; }
        public ContextMenuStrip Menu { get { return contextMenuStrip1; } }
        readonly ContextMenuStrip _menu;

        /// <summary>
        /// 
        /// </summary>
        public AppDiagnosticsView()
        {
            InitializeComponent();
            _menu = new ContextMenuStrip();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="line"></param>
        public void WriteLine(string line)
        {
            if (richTextBox1.InvokeRequired)
            {
                richTextBox1.Invoke((MethodInvoker)delegate
                {
                    DoWriteLine(line);
                });
            }
            else
            {
                DoWriteLine(line);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="line"></param>
        private void DoWriteLine(string line)
        {
            LineCounter++;
            line = DateTime.Now.ToString(AppConstants.GenericTimestampFormat, System.Globalization.CultureInfo.InvariantCulture) + ";" + line;
            toolStripLabel1.Text = "APP TRACE " + LineCounter.ToString();

            if (line.Contains("ERROR"))
            {
                richTextBox1.AppendText(line, Color.Red, true);
            }
            else if (line.Contains("Layout"))
            {
                richTextBox1.AppendText(line, Color.Aqua, true);
            }
            else
            {
                richTextBox1.AppendText(line, Color.White, true);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            Clear();
        }

        public void Clear()
        {
            LineCounter = 0;
            richTextBox1.Clear();
            toolStripLabel1.Text = "APP TRACE 0";        
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (buttonToolStripAutoScroll.Checked)
            {
                richTextBox1.SelectionStart = richTextBox1.Text.Length;
                richTextBox1.ScrollToCaret();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AppTraceView_Load(object sender, EventArgs e)
        {
            buttonToolStripClear.Text = CultureService.Instance.GetString(CultureText.menuItem_Clear_Text);
            buttonToolStripAutoScroll.Text = CultureService.Instance.GetString(CultureText.menuItem_AutoScrolling_Text);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void richTextBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
                _menu.Show(richTextBox1, e.Location);
        }

        public void LayoutSave()
        {
            Settings.Default.AppTraceScroll = buttonToolStripAutoScroll.Checked;
        }

        public void LayoutRestore()
        {
            buttonToolStripAutoScroll.Checked = Settings.Default.AppTraceScroll;
        }
    }
}
