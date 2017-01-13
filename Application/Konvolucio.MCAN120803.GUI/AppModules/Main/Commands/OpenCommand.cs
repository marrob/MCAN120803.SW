
namespace Konvolucio.MCAN120803.GUI.AppModules.Main.Commands
{
    using System;
    using System.Windows.Forms; /*ToolStripMenuItem*/
    
    using Properties;
    using WinForms.Framework;
    using Workspace.Service;
    using DataStorage;
    using View;
    using DataStorage;

    internal sealed class OpenCommand : ToolStripMenuItem
    {
        private readonly Storage _storage;
        private readonly IWorkspaceService _worksapce;
        readonly OpenFileView _openView = null;

        public OpenCommand(Storage storage, IWorkspaceService workspace)
        {
            _storage = storage;
            _worksapce = workspace;
            Image = Resources.Folder_48x48;
            DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            ToolTipText = @"Ctrl + O";
            ShortcutKeys = Keys.Control | Keys.O;
            EventAggregator.Instance.Subscribe<StopAppEvent>(e => Enabled = true);
            EventAggregator.Instance.Subscribe<PlayAppEvent>(e => Enabled = false);
            EventAggregator.Instance.Subscribe<StorageAppEvent>(e =>
            {
                if (e.ChangingType == FileChangingType.Loading || e.ChangingType == FileChangingType.Saving)
                    Enabled = false;

                if (e.ChangingType == FileChangingType.LoadComplete || e.ChangingType == FileChangingType.SaveComplete)
                    Enabled = true;
            });

           _openView = new OpenFileView();
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);

            if (_storage.IsChanged)
            {
                var userAction = new SaveMessageBox().Show(_storage.FileName + AppConstants.FileExtension);
                if (userAction == UserAction.Yes)
                {
                    /*A régi változásit menttete, nyitja az újat...*/
                    new SaveCommand(_storage).PerformClick();

                    if (_openView.Show() == UserAction.OK)
                        _worksapce.WorkspaceItems.OpenProject(_openView.Path);

                }
                else if (userAction == UserAction.No)
                {
                    /*A régi változásit eldobta, nyitja az újat...*/
                    _storage.DropChanged();
                    if (_openView.Show() == UserAction.OK)
                    {
                        _worksapce.WorkspaceItems.OpenProject(_openView.Path);
                    }
                }
            }
            else
            {   /*Csask simán nyitja a workspacen kersztül*/
                if (_openView.Show() == UserAction.OK)
                {
                    _worksapce.WorkspaceItems.OpenProject(_openView.Path);
                }
            }
        }
    }
}
