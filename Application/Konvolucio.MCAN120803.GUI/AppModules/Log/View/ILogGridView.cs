// -----------------------------------------------------------------------
// <copyright file="ILogGridView.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Konvolucio.MCAN120803.GUI.AppModules.Log.View
{

    using System.Windows.Forms;

    using WinForms.Framework;   /*datgridViewBackgorunText*/
    using Model;
    using Common;
    using Export.Model;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface ILogGridView :  IExportableTableObject
    {
        /// <summary>
        /// Tiltás.
        /// </summary>
        bool Enabled { get; set; }

        /// <summary>
        /// Context Menu
        /// </summary>
        ContextMenuStrip Menu { get; }

        /// <summary>
        /// Log üzenetek listája.
        /// </summary>
        ILogFileMessageCollection Source { get; set; }

        /// <summary>
        /// Egyedi arbitrációs mező oszlopai.
        /// </summary>
        CustomArbIdColumnCollection CustomArbIdColumns { set; }

        /// <summary>
        /// Üzenet időblyegének formátuma.
        /// </summary>
        string TimestampFormat { get; set; }

        /// <summary>
        /// Automtaikus oszolp szélesség
        /// </summary>
        void ColumnsAutoSizeAll();

        /// <summary>
        /// Grid Layout mentés és visszaállítása.
        /// </summary>
        ColumnLayoutCollection GridLayout { get; set; }

        /// <summary>
        /// Újra rajzolja a grid-et.
        /// </summary>
        void Refresh();

        /// <summary>
        /// Layout felveszi az alaphelyzetét.
        /// </summary>
        void DefaultLayout();

    }
}
