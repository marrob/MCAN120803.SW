namespace Konvolucio.MCAN120803.GUI.AppModules.Export.View
{
    using System.Windows.Forms;


    public partial class XlsxOptionsView : UserControl, IXlsxOptionsView
    {
        public bool ColumnNameInFirstRow { get { return checkBox1.Checked; } }
        public bool ApplyTheFormattingParameters { get { return checkBoxApplyTheFormattingParameters.Checked; } }

        public XlsxOptionsView()
        {
            InitializeComponent();
        }
    }
}
