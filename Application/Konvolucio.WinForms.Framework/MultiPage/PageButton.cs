// -----------------------------------------------------------------------
// <copyright file="UncheckableToolStripButton.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Konvolucio.WinForms.Framework
{
    using System;
    using System.Windows.Forms;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class PageButton: ToolStripButton
    {
        protected override void OnClick(EventArgs e)
        {
            if (!Checked)
            {
                base.OnClick(e);
            }
        }

        public override string ToString()
        {
            return Text + " " + CheckState.ToString();
        }
    }
}
