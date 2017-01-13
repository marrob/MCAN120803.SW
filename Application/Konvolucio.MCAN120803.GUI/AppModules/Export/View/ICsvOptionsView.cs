namespace Konvolucio.MCAN120803.GUI.AppModules.Export.View
{
    using System.Windows.Forms;

    public interface ICsvOptionsView
    {
        bool ColumnNameInFirstRow { get; }
        string Escape { get; }
        string Delimiter { get; }
        string NewLine { get; }
        DockStyle Dock { get; set; }
    }
}