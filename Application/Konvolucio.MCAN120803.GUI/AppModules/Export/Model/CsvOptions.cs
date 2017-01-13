// -----------------------------------------------------------------------
// <copyright file="CsvOptions.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Konvolucio.MCAN120803.GUI.AppModules.Export.Model
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    class CsvOptions
    {
        public bool ColumnNameInFirstRow { get; set; }
        public string Escape { get; set; }
        public string Delimiter { get; set; }
        public string NewLine { get; set; }

        public CsvOptions()
        {
            ColumnNameInFirstRow = true;
            Escape = Esacpes[0];
            Delimiter = Delimiters[0];
            NewLine = NewLines[0];
        }

        public static string[] Esacpes
        {
            get { return new string[] { "\"", "'", ""}; }
        }

        public static string[] Delimiters
        {
            get { return new string[] { ";", ",", @"\t", " " }; }
        }

        public static string[] NewLines
        {
            get { return new string[] { @"\r\n", @"\r", @"\n" }; }
        }
    }
}
