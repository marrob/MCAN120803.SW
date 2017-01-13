
namespace Konvolucio.MCAN120803.GUI.AppModules.Main.Model
{
    using Services;
    using View;

    public class ShowingParameters : IShowingParameters
    {
        public string Path { get { return _saveAsProjectForm.FullPath; } }
        public string ProudctName 
        {
            get { return _saveAsProjectForm.ProudctName; }
            set { _saveAsProjectForm.ProudctName = value; }
        }
        public string ProductVersion 
        { 
            get { return _saveAsProjectForm.PrjProductVersion; }
            set { _saveAsProjectForm.PrjProductVersion = value; }
        }
        public string ProcutCode 
        {
            get { return _saveAsProjectForm.ProcutCode; }
            set { _saveAsProjectForm.ProcutCode = value; }
        }
        public string CustomerName 
        { 
            get { return _saveAsProjectForm.CustomerName; }
            set { _saveAsProjectForm.CustomerName = value; }
        }
        public string CustomerCode
        {
            get { return _saveAsProjectForm.CustomerCode; }
            set { _saveAsProjectForm.CustomerCode = value; }
        }

        readonly ISaveProjectForm _saveAsProjectForm;

        public ShowingParameters()
        {
            _saveAsProjectForm = new SaveAsProjectForm();   
        }

        public bool Show()
        {
            return _saveAsProjectForm.ShowDialog() == System.Windows.Forms.DialogResult.OK;
        }
    }
}
