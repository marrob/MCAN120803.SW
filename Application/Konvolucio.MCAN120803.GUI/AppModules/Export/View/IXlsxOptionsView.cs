namespace Konvolucio.MCAN120803.GUI.AppModules.Export.View
{
    using System.Windows.Forms;

    public interface IXlsxOptionsView
    {
        bool ColumnNameInFirstRow { get; }
        bool ApplyTheFormattingParameters { get; }

        DockStyle Dock { get; set; }
    }
}