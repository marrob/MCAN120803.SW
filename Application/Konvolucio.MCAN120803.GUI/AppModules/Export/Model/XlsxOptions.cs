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
    class XlsxOptions
    {
        public bool ColumnNameInFirstRow { get; set; }
        public bool ApplyTheFormattingParameters { get; set; }


        public XlsxOptions()
        {
            ColumnNameInFirstRow = true;
            ApplyTheFormattingParameters = true;
        }
    }
}
