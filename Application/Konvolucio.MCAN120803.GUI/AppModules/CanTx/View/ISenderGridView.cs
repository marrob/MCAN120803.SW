
namespace Konvolucio.MCAN120803.GUI.AppModules.CanTx.View
{
    using System.Windows.Forms;
    using Model;
    using Common;
    using Export.Model;
    using WinForms.Framework;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface ISenderGridView : IWindowLayoutRestoring, IExportableTableObject
    {

        /// <summary>
        /// Sender Lista. 
        /// </summary>
        CanTxMessageCollection Source { get; set; }

        /// <summary>
        /// Egyedi arbitrációs mező oszlopai.
        /// </summary>
        CustomArbIdColumnCollection CustomArbIdColumns { set; }

        /// <summary>
        /// Felhasználó ezeket az elemeket jelölte ki a táblázatban.
        /// </summary>
        CanTxMessageItem[] SelectedItems { get; }

        /// <summary>
        /// Context Menu
        /// </summary>
        ContextMenuStrip Menu { get; }

        /// <summary>
        /// Grid Layout mentés és visszaállítása.
        /// </summary>
        ColumnLayoutCollection GridLayout { get; set; }

        /// <summary>
        /// Tiltás.
        /// </summary>
        bool AllowClick { get; set; }

        /// <summary>
        /// Csak olvasható vagy sem.
        /// </summary>
        bool ReadOnly { get; set; }

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
