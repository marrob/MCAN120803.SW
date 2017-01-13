
namespace Konvolucio.WinForms.Framework.DataGridViewControl.UnitTest.View
{
    using System.Windows.Forms;

    public partial class UserControlView : UserControl, IUserControlView
    {
        public DataGridView GridView 
        {
            get { return dataGridView1; }
        }

        public string TextBacgkground 
        {
            get { return dataGridView1.BackgroundText; }
            set { dataGridView1.BackgroundText = value; }
        }

        public UserControlView()
        {
            InitializeComponent();
        }
    
    }


    public interface IUserControlView
    {
        DataGridView GridView { get; }
        string TextBacgkground { get; set; }
    }

}
