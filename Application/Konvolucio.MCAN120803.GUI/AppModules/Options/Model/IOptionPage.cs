// -----------------------------------------------------------------------
// <copyright file="OptionPageBase.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Konvolucio.MCAN120803.GUI.AppModules.Options
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface IOptionPage
    {
        /// <summary>
        /// Jelzi a panle, hogy miatta kell-e reszetelni
        /// </summary>
        bool RequiedRestart { get; }

        /// <summary>
        /// Egy panel jelzi, hogy neki alapértelmezett érték kell.
        /// Ezt úgy kell kezelni, hogy a penelekre ilyenkor nem kell meghívni a Save() metódust,
        /// helyette Defualt-ot kell
        /// </summary>
        bool RequiedDefault { get; }

        /// <summary>
        /// Panel frssítse az értékeket, ha kell...
        /// </summary>
        void UpdateValues();

        /// <summary>
        /// Panel mentse a változásokat
        /// </summary>
        void Save();

        /// <summary>
        /// Állísd be a default értéket
        /// </summary>
        void Defualt();

    }
}
