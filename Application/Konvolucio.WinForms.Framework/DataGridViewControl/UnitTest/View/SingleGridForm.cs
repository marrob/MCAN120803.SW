
namespace Konvolucio.WinForms.Framework.DataGridViewControl.UnitTest.View
{
    using System.Windows.Forms;

    public partial class SingleGridForm : Form, ISingleGridForm
    {
        public SingleGridForm()
        {
            InitializeComponent();

            dataGridView1.KnvColumnHeaderContextMenu();
        }

        public KnvDataGridView GridView
        {
            get { return this.dataGridView1; }
        }
    }

    public interface ISingleGridForm
    {
        KnvDataGridView GridView { get; }     
        DialogResult ShowDialog();
    }

}
