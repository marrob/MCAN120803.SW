// -----------------------------------------------------------------------
// <copyright file="IExportForm.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Konvolucio.MCAN120803.GUI.AppModules.Export.View
{
    using System.Windows.Forms;
    using Model;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface IExportForm
    {
        /// <summary>
        /// Az Exportálandó adatforrás.
        /// </summary>
        IExportTable ExportTableSource { get; set; }
        
        /// <summary>
        /// A létrehozandó exportált fájl neve.
        /// Kiterjesztés nélkül.
        /// </summary>
        string FileName { get; set; }
        
        /// <summary>
        ///A létrehozadnó fájl könyvtára. 
        /// </summary>
        string Directory { get; set; }

        /// <summary>
        /// Exportáló ablak megjelnítése.
        /// </summary>
        /// <returns></returns>
        DialogResult ShowDialog();

        /// <summary>
        /// Exportáló ablak címe.
        /// </summary>
        string Title { get; set; }
    }
}
