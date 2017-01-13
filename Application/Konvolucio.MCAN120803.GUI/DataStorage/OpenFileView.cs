
namespace Konvolucio.MCAN120803.GUI.DataStorage
{
    using System.Windows.Forms;
    using Properties;
    using WinForms.Framework;

    public interface IOpenFileView
    {
        string Path { get; }
        UserAction Show();
    }

    class OpenFileView : IOpenFileView
    {
        public string Path { get { return openFileDialog.FileName; } }

        readonly OpenFileDialog openFileDialog;

        public OpenFileView ()
	    {
            openFileDialog = new OpenFileDialog();
	    }

        public UserAction Show()
        {
            openFileDialog.Filter = AppConstants.FileFilter;
            openFileDialog.FilterIndex = 1;
            openFileDialog.InitialDirectory = Settings.Default.BrowseLocation;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
                return UserAction.OK;
            else
                return UserAction.Cancel;
        }
    }
}
