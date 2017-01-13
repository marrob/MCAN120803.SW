
namespace Konvolucio.MCAN120803.GUI.AppModules.Main.View
{
    using System.Windows.Forms;
    using Services;             /*Culture Services*/
    using WinForms.Framework;   /*UserAction*/


    internal class SaveMessageBox
    {
        public UserAction Show(string fileName)
        {
             /*"Do you want to save " + fileName + "?",*/
            var msg = string.Format(CultureService.Instance.GetString(CultureText.msgBox_DoYouWantToSave_Text), fileName);
            var dlg = MessageBox.Show(msg,
                                Application.ProductName,
                                MessageBoxButtons.YesNoCancel,
                                MessageBoxIcon.Question,
                                MessageBoxDefaultButton.Button1);
            switch (dlg)
            {
                case DialogResult.Yes: return UserAction.Yes;
                case DialogResult.No: return UserAction.No;
                case DialogResult.Cancel: return UserAction.Cancel;
                default: return UserAction.Cancel;
            }
        }
    }
}
