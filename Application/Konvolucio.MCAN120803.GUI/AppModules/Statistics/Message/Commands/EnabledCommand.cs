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
    using DataStorage;


    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    internal sealed class EnabledCommand : ToolStripMenuItem
    {
       private readonly Storage _storage;

        public EnabledCommand(Storage storage)
        {
            Text = string.Empty;
            _storage = storage;

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

            EventAggregator.Instance.Subscribe<StorageAppEvent>(e =>
            {
                if (e.ChangingType == FileChangingType.ContentChanged)
                {
                    if (e.Storage.Parameters.MessageStatisticsEnabled)
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
            }
        }
    }
}
