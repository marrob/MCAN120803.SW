// -----------------------------------------------------------------------
// <copyright file="IExporter.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Konvolucio.MCAN120803.GUI.AppModules.Export.Model
{
    using System;
    using System.ComponentModel;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface IExporter : IDisposable
    {
        /// <summary>
        /// Exportáló befjezte az exportálást.
        /// </summary>
        event RunWorkerCompletedEventHandler Completed;
        
        /// <summary>
        /// Exportáló státusztát jelzi.
        /// </summary>
        event ProgressChangedEventHandler ProgressChanged;

        /// <summary>
        /// Exportáló neve.
        /// </summary>
        string Name { get; }
        
        /// <summary>
        /// Az exportáló File Dialógus ablakához.
        /// </summary>
        string FileFilter { get; }
        
        /// <summary>
        /// Ide ment az exportáló.
        /// Kiterjesztés is kell!
        /// </summary>
        string Path { get; set; }

        /// <summary>
        /// Ez lesz a fájl kiterjesztése.
        /// </summary>
        string FileExtension { get; }

        /// <summary>
        /// Az exportáló paraméterei.
        /// Exportálónként változik.
        /// </summary>
        object Options { get; set; }

        /// <summary>
        /// Exportálandó tábla.
        /// </summary>
        IExportTable DataSource { get; set; }

        /// <summary>
        /// Utolsó kivétel ami miatt leállt az exportálás.
        /// </summary>
        Exception LastException { get; }

        /// <summary>
        /// Exportálás indítása, aszinkron!
        /// </summary>
        void Start();

        /// <summary>
        /// Exportálás leállítása.
        /// </summary>
        void Stop();
    }
}
