
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
    using View;

    internal sealed class EditDescriptionCommand : ToolStripMenuItem
    {
        ILogFileItem _logFile;
        public EditDescriptionCommand()
        {
            Image = Resources.article16x16;
            Text = CultureService.Instance.GetString(CultureText.menuItem_EditDescription_Text);

            EventAggregator.Instance.Subscribe<TreeViewSelectionChangedAppEvent>(e =>
            {
                if (e.SelectedNode is LogFileNameTreeNode)
                {
                    _logFile = (e.SelectedNode as LogFileNameTreeNode).Log;
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
            
            if (Enabled)
            {
                ILogDescriptionEditorForm editor = new LogDescriptionEditorForm();
                editor.Content = _logFile.Info.Description;
                if (editor.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    _logFile.Info.Description = editor.Content;
               
            }
        }
    }
}
