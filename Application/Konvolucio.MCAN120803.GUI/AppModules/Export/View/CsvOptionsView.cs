
namespace Konvolucio.MCAN120803.GUI.AppModules.Export.View
{
    using System.Linq;
    using System.Windows.Forms;
    using Model;

    public partial class CsvOptionsView : UserControl, ICsvOptionsView
    {
        public bool ColumnNameInFirstRow { get { return checkBox1.Checked; } }
        public string Escape { get { return comboBoxEscape.Text; } }
        public string Delimiter { get { return comboBoxDelimiter.Text; } }
        public string NewLine { get { return comboBoxNewLine.Text; } }

        public CsvOptionsView()
        {
            InitializeComponent();

            comboBoxDelimiter.Items.AddRange(CsvOptions.Delimiters.ToArray<object>());
            comboBoxDelimiter.SelectedItem = CsvOptions.Delimiters[0];

            comboBoxNewLine.Items.AddRange(CsvOptions.NewLines.ToArray<object>());
            comboBoxNewLine.SelectedItem = CsvOptions.NewLines[0];

            comboBoxEscape.Items.AddRange(CsvOptions.Esacpes.ToArray<object>());
            comboBoxEscape.SelectedItem = CsvOptions.Esacpes[0];

        }
    }
}
