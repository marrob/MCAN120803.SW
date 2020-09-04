// -----------------------------------------------------------------------
// <copyright file="ISenderView.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Konvolucio.MCAN120803.GUI.AppModules.Statistics.Message.View
{
    using System.Windows.Forms;
    using Export.Model;
    using Model;
    using WinForms.Framework;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface IStatisticsGridView : IExportableTableObject
    {

        /// <summary>
        /// Üres tábla esetén a háttérszöveg.
        /// </summary>
        string BackgroundText { get; set; }

        /// <summary>
        /// Üzenet időblyegének formátuma.
        /// </summary>
        string TimestampFormat { get; set; }

        /// <summary>
        /// Sender Lista. 
        /// </summary>
        MessageStatisticsCollection Source { get; set; }

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
        /// Periodikus frissítés időalapja ms egységben.
        /// </summary>
        int RefreshRate { get; set; }

        /// <summary>
        /// Újra rajzolja a grid-et.
        /// </summary>
        void Refresh();

        /// <summary>
        /// Periodikus frssítés indítása.
        /// </summary>
        void Start();

        /// <summary>
        /// Periodikus frissítés leéllítása.
        /// Ami még nem került kirajzolásra azt kirajzolja.
        /// </summary>
        void Stop();

    }
}
