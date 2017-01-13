// -----------------------------------------------------------------------
// <copyright file="AutoSizeAllCommand.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Konvolucio.MCAN120803.GUI.AppModules.Tracer.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms;
    using Model;
    using Properties;
    using Services;
    using View;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    internal sealed class AutoScrolllCommand : ToolStripMenuItem
    {
               
        private readonly ITraceGridView _gridView;

        public AutoScrolllCommand(ITraceGridView gridView)
        {
            Image = Resources.Scroll48;
            Text = CultureService.Instance.GetString(CultureText.menuItem_AutoScrolling_Text);
            //ShortcutKeys = Keys.Alt | Keys.C;
            _gridView = gridView;
            gridView.ContextMenuStrip.Opening += ContextMenuStrip_Opening;
            
        }

        private void ContextMenuStrip_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Checked = _gridView.AutoScrollEnabled;
        }


        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            _gridView.AutoScrollEnabled = !_gridView.AutoScrollEnabled;
        }
    }
}
