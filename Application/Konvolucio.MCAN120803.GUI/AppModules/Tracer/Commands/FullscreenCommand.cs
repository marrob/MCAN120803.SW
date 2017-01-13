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
    internal sealed class FullscreenCommand : ToolStripMenuItem
    {
               
        private readonly ITraceGridView _gridView;

        public FullscreenCommand(ITraceGridView gridView)
        {
            Image = Resources.fullscreen16;
            Text = CultureService.Instance.GetString(CultureText.menuItem_Fullscreen);
            ShortcutKeys = Keys.F11;
            ToolTipText = Text + @"(F11)";

            _gridView = gridView;
            _gridView.ContextMenuStrip.Opening += ContextMenuStrip_Opening;

        }

        private void ContextMenuStrip_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Checked = _gridView.IsFullscreen;
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            _gridView.IsFullscreen = !_gridView.IsFullscreen;
            _gridView.UpdateScreenState();
        }
    }
}
