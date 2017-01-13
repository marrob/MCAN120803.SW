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
    internal sealed class AutoSizeAllCommand : ToolStripMenuItem
    {
               
        private readonly ITraceGridView _gridView;

        public AutoSizeAllCommand(ITraceGridView gridView)
        {
            Image = Resources.SizeHorizontal32;
            Text = CultureService.Instance.GetString(CultureText.menuItem_AutoColumnWidth_Text);
            //ShortcutKeys = Keys.Alt | Keys.C;
            _gridView = gridView;

        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            _gridView.AutoSizeAll();
        }
    }
}
