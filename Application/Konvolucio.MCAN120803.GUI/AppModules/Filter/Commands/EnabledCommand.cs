// -----------------------------------------------------------------------
// <copyright file="AddMessageFilter.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Konvolucio.MCAN120803.GUI.AppModules.Filter.Commands
{
    using System;
    using System.Windows.Forms;/*ToolStripMenuItem*/
    using Common;


    using Properties;
    using Services;

    internal sealed class EnabledCommand : ToolStripMenuItem
    {
        private readonly ProjectParameters _parameters;

        public EnabledCommand(ProjectParameters parameters)
        {
            _parameters = parameters;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (_parameters.FiltersEnabled)
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
                _parameters.FiltersEnabled = !_parameters.FiltersEnabled;

                if (_parameters.FiltersEnabled)
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
        }
    }
}
