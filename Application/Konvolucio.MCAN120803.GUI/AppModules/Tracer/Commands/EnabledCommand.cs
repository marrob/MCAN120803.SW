// -----------------------------------------------------------------------
// <copyright file="DeleteLogMessageCommand.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Konvolucio.MCAN120803.GUI.AppModules.Tracer.Commands
{
    using System;
    using System.Windows.Forms;
    using Common;
    using Properties;
    using Services;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    internal sealed class EnabledCommand : ToolStripMenuItem
    {
        private readonly ProjectParameters _parameters;

        public EnabledCommand(ProjectParameters parameters)
        {
            Text = string.Empty;
            _parameters = parameters;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (_parameters.TraceEnabled)
            {
                Text = CultureService.Instance.GetString(CultureText.menuItem_Disable_Text);
                Image = Resources.switchon16;
            }
            else
            {
                Text = CultureService.Instance.GetString(CultureText.menuItem_Enable_Text);
                Image = Resources.switchoff16;
            }
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);

            if (Enabled)
            {
                _parameters.TraceEnabled = !_parameters.TraceEnabled;
            }
        }
    }
}
