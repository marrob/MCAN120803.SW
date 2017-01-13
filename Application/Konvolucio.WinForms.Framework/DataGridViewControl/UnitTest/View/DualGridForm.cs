
namespace Konvolucio.WinForms.Framework.DataGridViewControl.UnitTest.View
{
    using System.Windows.Forms;

    public partial class DualGridForm : Form, IDualGridForm1
    {
        public DualGridForm()
        {
            InitializeComponent();
        }

        public IUserControlView LeftGrid 
        {
            get { return userControl12; }
        }

        public IUserControlView RightGird
        {
            get { return userControl11; }
        }
    }

    public interface IDualGridForm1
    {
        IUserControlView LeftGrid { get; }
        IUserControlView RightGird { get; }

       

        DialogResult ShowDialog();
    }

}
