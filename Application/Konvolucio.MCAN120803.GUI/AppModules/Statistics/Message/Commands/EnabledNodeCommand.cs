// -----------------------------------------------------------------------
// <copyright file="DeleteLogMessageCommand.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Konvolucio.MCAN120803.GUI.AppModules.Statistics.Message.Commands
{
    using System;
    using System.Windows.Forms; /*ToolStripMenuItem*/
    
    using Properties;
    using Services;             /*CultureService*/
    using TreeNodes;
    using DataStorage;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    internal sealed class EnabledNodeCommand : ToolStripMenuItem
    {
        Storage _storage;

        public EnabledNodeCommand()
        {
            _storage = null;
            Text = string.Empty;

            EventAggregator.Instance.Subscribe<TreeViewSelectionChangedAppEvent>(e =>
            {
                Visible = (e.SelectedNode is MessagesTreeNode);
            });

            EventAggregator.Instance.Subscribe<StorageAppEvent>(e =>
            {
                if (e.ChangingType == FileChangingType.ContentChanged)
                {
                    _storage = e.Storage;

                    if (_storage.Parameters.MessageStatisticsEnabled)
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
                _storage.Parameters.MessageStatisticsEnabled = !_storage.Parameters.MessageStatisticsEnabled;

                if (_storage.Parameters.MessageStatisticsEnabled)
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
