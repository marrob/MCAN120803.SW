// -----------------------------------------------------------------------
// <copyright file="IExport.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Konvolucio.MCAN120803.GUI.AppModules.Export.Model
{
    using System.Collections.Generic;

    /// <summary>
    /// Tábla Exportálható, ha megvalósítod
    /// </summary>
    public interface IExportableTableObject
    {
        /// <summary>
        /// Ezzel exportálhatod a tábát.
        /// </summary>
        IExportTable ExportTable { get; }
        
    }


    public interface IInfo
    {
        string Name { get; set; }
    }


    /// <summary>
    /// Exportáló tábla
    /// 
    /// Tábla és oszlop létrehozása:
    /// <code>
    /// var table = new ExportTableSource();
    /// table.Columns.Add(new ExportColumnItem()
    /// {
    ///     Name = "columnIndex",
    ///     HeaderText = "Index",
    ///     DisplayIndex = 1,
    ///     Visible = true,
    /// });
    /// </code>
    /// 
    /// Sor hozzáadása:
    /// <code>
    /// table.Rows.Add(new ExportRowItem());
    /// table.Rows[0].Cells[0].Value = "1";
    /// table.Rows[0].Cells[1].Value = "Homer";
    /// table.Rows[0].Cells[2].Value = "Simpsons";
    /// </code>
    /// 
    /// 
    /// </summary>
    public interface IExportTable
    {
        /// <summary>
        /// Az exoportálható tábla sorai
        /// Egy sor-t Cellák allakotják. 
        /// A cellák autmoatikusan létrehozásra kerülnek az Oszlopok listája alapján.
        /// </summary>
        IExportRowCollection Rows { get; }
        /// <summary>
        /// Tábla oszlopainak leírását tartalmazza.
        /// Ez alapján készeül egy sor cellái.
        /// </summary>
        IExportColumnCollection Columns { get; }


        /// <summary>
        /// A táblát létrehozó jelezheti, hogy ő támogatja az oszlopségességeket, 
        /// amelyeket jelzett is az oszlopoknál.
        /// </summary>
        bool SourceIsSupportColumnWidth { get; set; }

        /// <summary>
        /// A tábla egy sorának meg szerzése.
        /// </summary>
        /// <param name="columnIndex">Oszlop index, 0-ás bázisú.</param>
        /// <param name="rowIndex">Sor index, 0-ás bázisú. </param>
        /// <returns></returns>
        string this[int columnIndex, int rowIndex] { get; }

    }

    /// <summary>
    /// A tábla egy oszlopának leírása.
    /// </summary>
    public interface IExportColumnItem
    {
        /// <summary>
        /// Oszlop neve
        /// </summary>
        string Name { get; set; }
        
        /// <summary>
        /// Oszlop fejlécének szövege.
        /// </summary>
        string HeaderText { get; set; }
        
        /// <summary>
        /// Oszlop ebben a pozicióban jelenjen meg. 
        /// FIGYELEM MINDING EBBEN A SORRENDBEN EXPORTÁLJ. Nem a kollekciók tartlamazzák az oszolop sorrendet!
        /// </summary>
        int DisplayIndex { get; set; }

        /// <summary>
        /// Oszlop látható vagy sem.
        /// </summary>
        bool Visible { get; set; }

        /// <summary>
        /// A forrás oszlop szélessége.
        /// </summary>
        int Width { get; set; }
    }

    /// <summary>
    /// A tábla oszlopainak listája.
    /// Ez alapján készül egy sornak a Cellái.
    /// </summary>
    public interface IExportColumnCollection/*:IEnumerable*/
    {
        /// <summary>
        /// Oszlopok száma.
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Oszlop hozzáadása.
        /// <code>
        ///    table.Columns.Add(new ExportColumnItem()
        ///    {
        ///        Name = "columnIndex",
        ///        HeaderText = "Index",
        ///         DisplayIndex = 1,
        ///        Visible = true,
        ///    });
        /// </code>
        /// </summary>
        /// <param name="item"></param>
        void Add(IExportColumnItem item);


        /// <summary>
        /// Tábla oszlopának Indexelt elérése.
        /// </summary>
        /// <param name="columnIndex">Oszlop indexe. 0-ás bázisú.</param>
        /// <returns>Sor</returns>
        IExportColumnItem this[int columnIndex] { get; set; }

        /// <summary>
        /// Lista bejárható IExportColumnItem legyen...
        /// </summary>
        IEnumerator<IExportColumnItem> GetEnumerator();

        /// <summary>
        /// Oszolop trörlése, ami törli egyben minden sor-ban ehhez tartozó cell-át.
        /// </summary>
        /// <param name="index">Oszlop indexe-0 ás bázisú.</param>
        void RemoveAt(int index);

    }

    /// <summary>
    /// A táblának egy sora.
    /// Egy sort a Cellák alkotják.
    /// A Cellák az Oszlopok leírása alapján készülnek.
    /// </summary>
    public interface IExportRowItem
    {
        ExportCellCollection Cells { get; set; }
    }

    /// <summary>
    /// Egy sorban oszlopok vannak.
    /// A sor litába oszlopot nem lehet hozáadni.
    /// </summary>
    public interface IExportRowCollection
    {
        /// <summary>
        /// Oszlopok száma.
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Egy teljes sor hozáadása amik Cella kollekciót tartalmaz.
        /// </summary>
        /// <param name="item"></param>
        void Add(IExportRowItem item);

        /// <summary>
        /// Lista bejárható legyen...
        /// </summary>
        IEnumerator<IExportRowItem> GetEnumerator();

        /// <summary>
        /// Tábla sorának Indexelt elérése.
        /// </summary>
        /// <param name="rowIndex">Sor indexe. 0-ás bázisú. </param>
        /// <returns>Sor</returns>
        IExportRowItem this[int rowIndex] { get; set; }

    }
}
