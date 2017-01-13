// -----------------------------------------------------------------------
// <copyright file="ColumnHeaderContextMenu.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Konvolucio.WinForms.Framework
{
    using System.Linq;
    using System.Windows.Forms;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ColumnHeaderContextMenu
    {
        #region Column Header ContextMenuStrip

        private readonly KnvDataGridView _dataGridView;

        public ColumnHeaderContextMenu(KnvDataGridView dataGridView )
        {
            _dataGridView = dataGridView;
            _dataGridView.MouseUp += new MouseEventHandler(DataGridView_MouseUp);
        }


        /// <summary>
        /// 
        /// </summary>
        private ToolStripMenuItem HideColumn(int columnIndex)
        {
            var menuItem = new ToolStripMenuItem
            {
                Text = CultureService.GetString(CultureText.menuItem_HideColumn_Text),
                Enabled = !_dataGridView.Columns[columnIndex].Frozen
            };

            menuItem.Click += (o, e) =>
            {
                var newColumnLayout = new ColumnLayoutCollection();
                foreach (var layout in _dataGridView.ColumnLayout)
                    newColumnLayout.Add(layout);

                var toHideColumn = newColumnLayout.FirstOrDefault(n => n.Name == _dataGridView.ColumnLayout[columnIndex].Name);
                if (toHideColumn != null)
                    toHideColumn.Visible = false;

                _dataGridView.ColumnLayout = newColumnLayout;
            };

            return menuItem;
        }

        /// <summary>
        /// 
        /// </summary>
        private ToolStripMenuItem AutoSizeColumn(int columnIndex)
        {
            var menuItem = new ToolStripMenuItem
            {
                Text = CultureService.GetString(CultureText.menuItem_AutoSizeColumn_Text),
            };

            var lastColumns = _dataGridView.Columns.GetLastColumn(DataGridViewElementStates.Visible, DataGridViewElementStates.None);
            if (lastColumns != null)
            { /*Az utolsó oszlopra nem nyomhat autó size-t, mert akkor elvesziti az utolsó oszolp a Fill-t...*/
                if (lastColumns.Index != columnIndex)
                    menuItem.Enabled = true;
                else
                    menuItem.Enabled = false;
            }

            menuItem.Click += (o, e) =>
            {
                _dataGridView.AutoResizeColumn(columnIndex);
            };

            return menuItem;
        }

        /// <summary>
        /// 
        /// </summary>
        private ToolStripMenuItem ShowAll()
        {
            var menuItem = new ToolStripMenuItem
            {
                Text = CultureService.GetString(CultureText.menuItem_ShowAll_Text),
            };
            menuItem.Click += (o, e) =>
            {
              _dataGridView.ShowAllColums();
            };

            return menuItem;
        }

        /// <summary>
        /// 
        /// </summary>
        private ToolStripMenuItem AutoSizeAll()
        {
            var menuItem = new ToolStripMenuItem
            {
                Text = CultureService.GetString(CultureText.menuItem_AutoSizeAll_Text),
            };
            menuItem.Click += (o, e) =>
            {
                Cursor.Current = Cursors.WaitCursor;
                _dataGridView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
                Cursor.Current = Cursors.Default;
            };

            return menuItem;
        }

        /// <summary>
        /// TODO:  Itt probléma van akkor, ha a Custom ArbId Columns táblázat fejlécérea kattint... Valszeg azért mert itt nincs Index mező.
        /// </summary>
        private ToolStripItem[] ShowHideItems()
        {
           var items = new ToolStripItem[_dataGridView.Columns.Count - 1];
 
            int i = 0;
            foreach (DataGridViewColumn gridColumn in _dataGridView.Columns)
            {

//                if(gridColumn.Frozen != )

                if (gridColumn.HeaderText != @"Index")
                {
                    var button = new ToolStripMenuItem
                    {
                        Text = gridColumn.HeaderText,
                        CheckOnClick = true,
                        CheckState = gridColumn.Visible ? CheckState.Checked : CheckState.Unchecked
                    };
                    var column = gridColumn;
                    button.CheckStateChanged += (o, ev) =>
                    {
                        column.Visible = button.CheckState == CheckState.Checked ? true : false;
                    };
                    items[i++] = button;
                }
            }
            return items;
        }

        /// <summary>
        /// Context menu a DataGridView oszlopainak fejlécéhez.
        /// FIGYELEM Csak azokat oszlopokoat lehet elmenteni amiknek van nevük
        /// </summary>
        /// <param name="columnIndex">Erre a fejléc indexre kattintott.</param>
        /// <returns>Context menu, ezt jelenísd meg.</returns>
        private ContextMenuStrip CreateColumnHeaderContextMenuStrip(int columnIndex)
        {
            var contextMenu = new ContextMenuStrip();
            contextMenu.Items.Add(HideColumn(columnIndex));
            contextMenu.Items.Add(AutoSizeColumn(columnIndex));
            contextMenu.Items.Add("-");
            contextMenu.Items.Add(ShowAll());
            contextMenu.Items.Add(AutoSizeAll());
            contextMenu.Items.Add("-");
            contextMenu.Items.AddRange(ShowHideItems());
            return contextMenu;
        }

        private void DataGridView_MouseUp(object sender, MouseEventArgs e)
        {
            var hitTestInfo = _dataGridView.HitTest(e.X, e.Y);

            /*Ha fejlcécre kattint, akkor egy másik kontext menü jelenik meg.*/
            if (hitTestInfo.Type == DataGridViewHitTestType.ColumnHeader)
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Right)
                    CreateColumnHeaderContextMenuStrip(hitTestInfo.ColumnIndex).Show(_dataGridView, new System.Drawing.Point(e.X, e.Y));
            }
        }
        #endregion 
    }
}
