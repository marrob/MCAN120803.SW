// -----------------------------------------------------------------------
// <copyright file="DeleteLogMessageCommand.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Konvolucio.MCAN120803.GUI.AppModules.Adapters.Commands
{
    using System;
    using System.Windows.Forms; /*ToolStripMenuItem*/
    using DataStorage;
    using Properties;
    using Services;             /*CultureService*/
    using TreeNodes;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    internal sealed class EnabledCommand : ToolStripMenuItem
    {
        Storage _prj;

        public EnabledCommand()
        {
            _prj = null;
            Text = string.Empty;

            EventAggregator.Instance.Subscribe<TreeViewSelectionChangedAppEvent>(e =>
            {
                Visible = (e.SelectedNode is AdapterTreeNode);
            });

            EventAggregator.Instance.Subscribe<StorageAppEvent>(e =>
            {
                if (e.ChangingType == FileChangingType.ContentChanged)
                {
                    _prj = e.Storage;

                    if (_prj.Parameters.TraceEnabled)
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
            });
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);

            if (Enabled)
            {
                _prj.Parameters.TraceEnabled = !_prj.Parameters.TraceEnabled;

                if (_prj.Parameters.TraceEnabled)
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
