// -----------------------------------------------------------------------
// <copyright file="Exportable.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Konvolucio.MCAN120803.GUI.AppModules.Export.Model
{
    using System;
    using System.Collections.ObjectModel;

    /// <summary>
    /// A táblának egy cellája.
    /// Automtikusan készül.
    /// </summary>
    public class ExportCellItem
    {
        public string Value { get; set; }

        public override string ToString()
        {
            if (string.IsNullOrEmpty(Value) || string.IsNullOrWhiteSpace(Value))
                return "Empty";
            else
                return Value;
        }
    }

    /// <summary>
    /// A sorok listája alkot egy sort.
    /// </summary>
    public class ExportCellCollection : Collection<ExportCellItem>
    {
        public override string ToString()
        {
            return string.Join<ExportCellItem>(",", this);
        }
    }

    /// <inheritdoc />
    public class ExportColumnItem : IExportColumnItem
    {
        public string Name { get; set; }
        public string HeaderText { get; set; }
        public int DisplayIndex { get; set; }
        public bool Visible { get; set; }
        public int Width { get; set; }

        public override string ToString()
        {
            return Name + " - " + HeaderText;
        }
    }

    /// <inheritdoc />
    public class ExportRowItem : IExportRowItem
    {
        public ExportCellCollection Cells { get; set; }

        public ExportRowItem()
        {
                
        }

        public override string ToString()
        {
            var str = string.Empty;
            if (Cells != null)
                foreach (var column in Cells)
                    str += column.Value + ", ";
            str = str.Trim(' ', ',');
            return str;
        }
    }

    /// <inheritdoc />
    public class ExportColumnCollection : Collection<IExportColumnItem>, IExportColumnCollection
    {
        internal ExportRowCollection Rows;

        internal ExportColumnCollection()
        {

        }

        public new void RemoveAt(int index)
        {

            base.RemoveAt(index);
            for (int i = 0; i < Rows.Count; i++)
                Rows[i].Cells.RemoveAt(index);
        }
    }

    /// <inheritdoc />
    public class ExportRowCollection : Collection<IExportRowItem>, IExportRowCollection
    {
        internal ExportColumnCollection Columns;

        internal ExportRowCollection()
        {

        }

        /// <summary>
        /// Egy sor hozzádasa
        /// - itt az oszlopok alapján cellák készülnek 
        /// - a cellák listája adja a sort.
        /// </summary>
        /// <param name="item"></param>
        public new void Add(IExportRowItem item)
        {
            if(Columns.Count == 0)
                throw new ArgumentException("nincs oszlop..");

            var cells = new ExportCellCollection();
            for (int i = 0; i < Columns.Count; i++)
                cells.Add(new ExportCellItem());
            item.Cells = cells;
            base.Add(item);
        }
    }

    /// <inheritdoc />
    public class ExportTable : IExportTable
    {

        public IExportRowCollection Rows
        {
            get { return _rows; }
        }
        private readonly ExportRowCollection _rows;

        public IExportColumnCollection Columns
        {
            get { return _columns; }
        }        
        private readonly ExportColumnCollection _columns;

        public bool SourceIsSupportColumnWidth { get; set; }

        public string this[int columnIndex, int rowIndex]
        {
            get { return _rows[rowIndex].Cells[columnIndex].Value; }
        }

        public ExportTable()
        {
            _columns = new ExportColumnCollection();
            _rows = new ExportRowCollection();
            _columns.Rows = _rows;
            _rows.Columns = _columns;
        }
    }
}
