
namespace Konvolucio.MCAN120803.GUI.AppModules.Workspace.Commands
{

    using Main.View;
    using WinForms.Framework; 
    using DataStorage;

    class OpenWorkspaceCommand
    {
        private readonly Storage _storage;
        string _path;

        public OpenWorkspaceCommand(Storage storage)
        {
            _storage = storage;
        }

        public void Open(string path)
        {
            _path = path;
            if (_storage.IsChanged)
            {
                var userAction = new SaveMessageBox().Show(_storage.FileName + AppConstants.FileExtension);
                if (userAction == UserAction.Yes)
                {
                    new Main.Commands.SaveCommand(_storage).PerformClick();
                }
            }
            else
            {
                _storage.Load(path);
            }
        }
    }
}
