
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

    /// <summary>
    /// Log fájl átnevezése
    /// </summary>
    internal sealed class RenameFileCommand : ToolStripMenuItem
    {
        /*TODO: AppEvent-en keresztül ezt is meg kaphajta*/
        ILogFileItem _dbFile;

        /*TODO: AppEvent-en keresztül ezt is meg kaphajta*/
        readonly ILogFileCollection _logFiles;

        public RenameFileCommand(ILogFileCollection logFiles)
        {
            Text = CultureService.Instance.GetString(CultureText.menuItem_Rename_Text);
            Image = Resources.rename16;
            _logFiles = logFiles;

            /*Látható és érték átvétel*/
            EventAggregator.Instance.Subscribe<TreeViewSelectionChangedAppEvent>(e =>
            {
                if (e.SelectedNode is LogFileNameTreeNode)
                {
                    _dbFile = (e.SelectedNode as LogFileNameTreeNode).Log;
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

                IRenameForm rf = new RenameForm();
                rf.NewName =  System.IO.Path.GetFileNameWithoutExtension(_dbFile.Path);
                if (rf.ShowDialog() == DialogResult.OK)
                {
                    _dbFile.Rename(rf.NewName);
                }
            }
        }
    }
}
