// -----------------------------------------------------------------------
// <copyright file="DeleteLogMessageCommand.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Konvolucio.MCAN120803.GUI.AppModules.Log.Commands
{
    using System;
    using System.Windows.Forms; 
    using Properties;
    using Services;             
    using TreeNodes;
    using DataStorage;

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
                Visible = (e.SelectedNode is LogTopTreeNode);
            });

            EventAggregator.Instance.Subscribe<StorageAppEvent>(e =>
            {
                if (e.ChangingType == FileChangingType.ContentChanged)
                {
                   _prj = e.Storage;

                   if (_prj.Parameters.LogEnabled)
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

            EventAggregator.Instance.Subscribe<PlayAppEvent>(e => Enabled = false);
            EventAggregator.Instance.Subscribe<StopAppEvent>(e => Enabled = true);
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);

            if (Enabled)
            {
                _prj.Parameters.LogEnabled = !_prj.Parameters.LogEnabled;

                if (_prj.Parameters.LogEnabled)
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
