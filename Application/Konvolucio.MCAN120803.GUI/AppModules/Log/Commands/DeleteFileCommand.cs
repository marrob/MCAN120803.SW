
namespace Konvolucio.MCAN120803.GUI.AppModules.Log.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms; /*ToolStripMenuItem*/

    using Properties;
    using WinForms.Framework;
    using Services;
    using Model;
    using TreeNodes;

    internal sealed class DeleteFileCommand : ToolStripMenuItem
    {
        ILogFileCollection _logs;
        ILogFileItem _log;

        public DeleteFileCommand()
        {
            Image = Resources.Delete_16x16;
            Text = CultureService.Instance.GetString(CultureText.menuItem_Delete_Text);
            
           
            EventAggregator.Instance.Subscribe<TreeViewSelectionChangedAppEvent>(e =>
            {
                if (e.SelectedNode is LogFileNameTreeNode)
                {
                    _logs = (e.SelectedNode as LogFileNameTreeNode).Logs;
                    _log = (e.SelectedNode as LogFileNameTreeNode).Log;
                    Visible = true;
                }
                else
                {
                    Visible = false;
                }
            });
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            _logs.Remove(_log);
        }
    }
}
