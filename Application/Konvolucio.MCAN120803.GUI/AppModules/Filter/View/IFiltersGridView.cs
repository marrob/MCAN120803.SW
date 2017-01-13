// -----------------------------------------------------------------------
// <copyright file="ISenderGridView.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Konvolucio.MCAN120803.GUI.AppModules.Filter.View
{
    using System;
    using System.Windows.Forms;
    using Common;
    using Export.Model;
    using Model;
    using WinForms.Framework;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface IFiltersGridView : IExportableTableObject
    {
        /// <summary>
        /// Sender Lista. 
        /// </summary>
        MessageFilterCollection Source { get; set; }

        /// <summary>
        /// Felhasználó ezeket az elemeket jelölte ki a táblázatban.
        /// </summary>
        MessageFilterItem[] SelectedItems { get; }

        /// <summary>
        /// Ebben a sorban van a user a kurzorral.
        /// </summary>
        int CurrentRow { get; }

        /// <summary>
        /// Context Menu
        /// </summary>
        ContextMenuStrip Menu { get; }

        /// <summary>
        /// Grid Layout mentés és visszaállítása.
        /// </summary>
        ColumnLayoutCollection GridLayout { get; set; }

        /// <summary>
        /// Üres tábla esetén a háttérszöveg.
        /// </summary>
        string BackgroundText { get; set; }

        /// <summary>
        /// Tiltás.
        /// </summary>
        bool Enabled { get; set; }

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


        DataGridView DataGridViewBase { get; }



    }
}
