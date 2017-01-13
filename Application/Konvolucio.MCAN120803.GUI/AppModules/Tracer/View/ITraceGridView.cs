// -----------------------------------------------------------------------
// <copyright file="ITraceGridView.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Konvolucio.MCAN120803.GUI.AppModules.Tracer.View
{
    using System;
    using System.Windows.Forms;
    
    using Model;
    using Common;
    using WinForms.Framework;

    /// <summary> CAN Analizator Trace ablak</summary>
    public interface ITraceGridView
    {
        event EventHandler FullScreenChanged;

        /// <summary>
        /// Trace lista.
        /// </summary>
        MessageTraceCollection Source { get; set; }

        /// <summary>
        /// Egyedi arbitrációs mező oszlopai.
        /// </summary>
        CustomArbIdColumnCollection CustomArbIdColumns { set; }

        /// <summary>
        /// AutoScorll
        /// </summary>
        bool AutoScrollEnabled { get; set; }

        /// <summary>
        /// Context Menu
        /// </summary>
        //ContextMenuStrip Menu { get; }

        /// <summary>
        /// Grid Layout mentés és visszaállítása.
        /// </summary>
        ColumnLayoutCollection GridLayout { get; set; }

        /// <summary>
        /// Periodikus frissítés időalapja ms egységben.
        /// </summary>
        int RefreshRate { get; set; }

        /// <summary>
        /// Üres tábla esetén a háttérszöveg.
        /// </summary>
        string BackgroundText { get; set; }

        /// <summary>
        /// Üzenet időblyegének formátuma.
        /// </summary>
        string TimestampFormat { get; set; }

        /// <summary>
        /// Periodikus frssítés indítása.
        /// </summary>
        void Start();

        /// <summary>
        /// Periodikus frissítés leéllítása.
        /// Ami még nem került kirajzolásra azt kirajzolja.
        /// </summary>
        void Stop();

        /// <summary>
        /// Layout felveszi az alaphelyzetét.
        /// </summary>
        void DefaultLayout();

        /// <summary>
        /// Minden oszolop autmatikus széles.
        /// </summary>
        void AutoSizeAll();

        /// <summary>
        /// Teljesképernyős?.
        /// </summary>
        bool IsFullscreen { get; set; }

        /// <summary>
        /// Context mnühöz
        /// </summary>
        ContextMenuStrip ContextMenuStrip { get; }

        /// <summary>
        /// Hívd és frssíti a képet... 
        /// </summary>
        void UpdateScreenState();

    }
}
