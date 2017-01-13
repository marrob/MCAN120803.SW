
namespace Konvolucio.WinForms.Framework
{
    using System;
    using System.Linq;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    /// <summary>
    /// Ez a DataGridView támogatja:
    /// 
    /// - Háttér szöveg rajzolását, ha éppen üres a Gird, 
    ///   ->BackgroundText
    /// - Az oszlopok megjelenésének mentése és visszállítása
    ///   ->ColumnLayout{get;set;}
    /// - Minden oszlophoz tartzik ContextMenu
    ///   -> FIGYELEM! csak az az oszlop HIDE-olható a ContextMemnuből aminek van rendes Name-je.
    /// - Az Utolsó oszlop mindig kitölti a rendelkezésre álló helyet, ehhe engedélyezd a 
    ///   -> LastColumnAlwaysFill tulajdonságot. Minden mező AutoSizeMode-ja legyen: NONE
    ///  
    /// 
    /// </summary>
    //[ToolboxBitmap(@"E:\Install\Icons\Pack_0001\Database Table\DatabaseTable16.bmp")]

    /*[ToolboxBitmap(@"C:\Documents and Settings\Joe\MyPics\myImage.bmp")]*/
    /*[ToolboxBitmap(typeof(DataGridView))]*/
    /*[ToolboxBitmap(typeof(MyControl), "MyControlBitmap")]*/
    /*[ToolboxBitmap(typeof(MyControl), "MyControlBitmap")]*/
    public class KnvDataGridView : DataGridView
    {

        #region Events Overrides
        protected override void OnPaint(PaintEventArgs e)
        {
            PaintHandlerForBackgoroundText(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            //if (!MosueUpHandlerForColumnContextMenu(e))
                base.OnMouseUp(e);
        }

        protected override void OnRowPrePaint(DataGridViewRowPrePaintEventArgs e)
        {
            base.OnRowPrePaint(e);
            
            RowPrePaintHandlerForZebraRows(e);
        }

        protected override void OnDataBindingComplete(DataGridViewBindingCompleteEventArgs e)
        {
            base.OnDataBindingComplete(e);

            LastDisplayedColumnFillHandler();
        }

        protected override void OnColumnStateChanged(DataGridViewColumnStateChangedEventArgs e)
        {
            base.OnColumnStateChanged(e);            
            if (e.StateChanged == DataGridViewElementStates.Visible)
            { 
                LastDisplayedColumnFillHandler();
            }
        }

        protected override void OnColumnAdded(DataGridViewColumnEventArgs e)
        {
            base.OnColumnAdded(e);
            /*TODO emiat az van az hogy az első oszlop a leghosszabb mindig, amikor a dgv készül. átmetneileg kilőve*/
            /*LastDisplayedColumnFillHandler();*/
        }

        protected override void OnColumnRemoved(DataGridViewColumnEventArgs e)
        {
            base.OnColumnRemoved(e);
            LastDisplayedColumnFillHandler();
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            LastDisplayedColumnFillHandler();
        }

        protected override void OnColumnWidthChanged(DataGridViewColumnEventArgs e)
        {
            base.OnColumnWidthChanged(e);
            LastDisplayedColumnFillHandler();
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            EnabledVisualizerHandler(Enabled);
            base.OnEnabledChanged(e);
        }
        #endregion

        #region Enabled Visualizer Handler
        /// <summary>
        /// Enabled állpotát jelzi
        /// </summary>
        private void EnabledVisualizerHandler(bool enabled)
        {
            if (!enabled)
            {
                ColumnHeadersDefaultCellStyle =
                    new DataGridViewCellStyle(new DataGridViewCellStyle()
                    {
                        BackColor = Color.Yellow,

                        Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238))),
                    });


            }
            else
            {
                ColumnHeadersDefaultCellStyle =
                    new DataGridViewCellStyle(new DataGridViewCellStyle()
                    {
                        Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238))),
                        BackColor = SystemColors.Control,
                    });
            }
        }

        #endregion

        #region Background Text
        /// <summary>
        /// Ha nincs tartalom ez a szöveg látszik.
        /// </summary>
        [Category("KNV")]
        [Description("Ha nincs tartalom ez a szöveg látszik.")] 
        public string BackgroundText
        {
            get { return _backgroundText; }
            set
            {
                _backgroundText = value;
                Refresh();
            }
        }
        private string _backgroundText = "Backgorund Text";

        private void PaintHandlerForBackgoroundText(PaintEventArgs e)
        {
            var dgv = this;
            float width = dgv.Bounds.Width;
            float height = dgv.Bounds.Height;

            int backgroundTextSize = 25;

            base.OnPaint(e);

            if (dgv.Rows.Count == 0)
            {
                Color clear = dgv.BackgroundColor;
                if (backgroundTextSize == 0) backgroundTextSize = 10;
                Font f = new Font("Seqoe", 20, FontStyle.Bold);
                Brush b = new SolidBrush(Color.Orange);
                SizeF strSize = e.Graphics.MeasureString(_backgroundText, f);
                e.Graphics.DrawString(_backgroundText, f, b, (width / 2) - (strSize.Width / 2), (height / 2) - strSize.Height / 2);
            }
        }
        #endregion 

        #region Layout
        /// <summary>
        /// Olvasáskor elkészíti a default Layout-ot, amielyt a designerben is látni
        /// Beállításkor pedig visszaállítja...
        /// </summary>
        [Category("KNV")]
        [Description("Olvasáskor elkészíti a default Layout-ot, amielyt a designerben is látni, beállításkor pedig visszaállítja...")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(true)]
        public ColumnLayoutCollection ColumnLayout
        {
            get
            {
                var columLayoutItems = new ColumnLayoutCollection();
                foreach (DataGridViewColumn gridColumn in this.Columns)
                {
                    var columnLayout = new ColumnLayoutItem();

                    if (gridColumn.Frozen)
                    {   /*A befagyasztott oszlpnak csak a neve és szélessége számít.*/
                        columnLayout.Name = gridColumn.Name;
                        columnLayout.Width = gridColumn.Width;
                        columnLayout.Visible = true;
                    }
                    else
                    {
                        columnLayout.Name = gridColumn.Name;
                        columnLayout.DisplayIndex = gridColumn.DisplayIndex;
                        columnLayout.Visible = gridColumn.Visible;
                        columnLayout.Width = gridColumn.Width;
                    }
                    columLayoutItems.Add(columnLayout);
                }
                return columLayoutItems;
            }
            set
            {
                if (value != null)
                {
                    /*Azok az oszlopok amik nincsenek benne a metnésben azok nem látszódnak...*/
                    var layoutItems = value as ColumnLayoutCollection;
                    var sorted = layoutItems.OrderBy(n => n.DisplayIndex);
                    foreach (ColumnLayoutItem item in sorted)
                    {
                        if (Columns.Contains(item.Name))
                        {
                            if (item.DisplayIndex < ColumnCount)/*Az probléma ha DisplayIndex nagyobb mint az oszlopok száma*/
                            {
                                if (Columns[item.Name] != null)
                                {
                                    if (Columns[item.Name].Frozen)
                                    {
                                        /*Be van fagyasztva, az oszlop, csak a szélességét módosíthatja.*/
                                        Columns[item.Name].Width = item.Width;
                                        Columns[item.Name].Visible = true;
                                    }
                                    else
                                    {
                                        Columns[item.Name].DisplayIndex = item.DisplayIndex;
                                        Columns[item.Name].Visible = item.Visible;
                                        Columns[item.Name].Width = item.Width;
                                        Columns[item.Name].Visible = item.Visible;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Layout segítségével minden oszlopot megjelnít.
        /// </summary>
        public void ShowAllColums()
        {
            /*Minden oszlop legyen látható*/
            //var temp = new ColumnLayoutCollection();
            //dataGridView1.ColumnLayout.CopyTo(temp);
            //foreach (var columnLayout in temp)
            //    columnLayout.Visible = true;
            //dataGridView1.ColumnLayout = temp;

            var newColumnLayout = new ColumnLayoutCollection();
            foreach (var layout in ColumnLayout)
            {
                layout.Visible = true;
                newColumnLayout.Add(layout);
            }
            ColumnLayout = newColumnLayout;
        }
        #endregion

        #region Visible Width
        /// <summary>
        /// A láthoható szélesség. A Vertikális ScrollBar szélességével korrigálva.
        /// </summary>
        [Category("KNV")]
        [Description("A láthoható szélesség. A Vertikális ScrollBar szélességével korrigálva.")]
        public int VisibleWidth
        {
            get 
            {
                var retval = 0;
                foreach (DataGridViewColumn gridColumn in this.Columns)
                {
                    if (gridColumn.Visible)
                        retval += gridColumn.Width;
                }
                retval += (this.RowHeadersVisible ? this.RowHeadersWidth : 0);
                /*TODO: +10... mert nem tudom mért.. egyes helyeken megjelnik a Vertikális scrollbár amikor nem kellene.*/
                retval += SystemInformation.VerticalScrollBarWidth + 10;
                return retval;
            }
        }
        #endregion 

#if Disable

        #region Column Header ContextMenuStrip

        /// <summary>
        /// Context menu a DataGridView oszlopainak fejlécéhez.
        /// FIGYELEM Csak azokat oszlopokoat lehet elmenteni amiknek van nevük
        /// </summary>
        /// <param name="columnIndex">Erre a fejléc indexre kattintott.</param>
        /// <returns>Context menu, ezt jelenísd meg.</returns>
        private ContextMenuStrip CreateColumnHeaderContextMenuStrip(int columnIndex)
        {
            var contextMenu = new ContextMenuStrip();

            #region Hide Column

            var hideColumnStripItem = new ToolStripMenuItem
            {
                Text = CultureService.GetString(CultureText.menuItem_HideColumn_Text),
                Enabled = !Columns[columnIndex].Frozen
            };


            hideColumnStripItem.Click += (o, e) =>
            {
                var newColumnLayout = new ColumnLayoutCollection();
                foreach (var layout in ColumnLayout)
                {
                    newColumnLayout.Add(layout);
                }

                var toHideColumn = newColumnLayout.FirstOrDefault(n => n.Name == Columns[columnIndex].Name);
                if (toHideColumn != null)
                    toHideColumn.Visible = false;

                ColumnLayout = newColumnLayout;
                /*TODO: Túl hosszú múvelet.*/
                /*AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);*/
            };
            contextMenu.Items.Add(hideColumnStripItem);
            #endregion

            #region Auto Size Column

            var autoSizeColumnStripItem = new ToolStripMenuItem
            {
                Text = CultureService.GetString(CultureText.menuItem_AutoSizeColumn_Text),
            };

            var lastColumns = Columns.GetLastColumn(DataGridViewElementStates.Visible, DataGridViewElementStates.None);
            if (lastColumns != null)
            { /*Az utolsó oszlopra nem nyomhat autó size-t, mert akkor elvesziti az utolsó oszolp a Fill-t...*/
                if (lastColumns.Index != columnIndex)  
                    autoSizeColumnStripItem.Enabled = true;
                else
                    autoSizeColumnStripItem.Enabled = false;
            }

            autoSizeColumnStripItem.Click += (o, e) =>
            {
               AutoResizeColumn(columnIndex);
            };
            contextMenu.Items.Add(autoSizeColumnStripItem);
            #endregion 

            contextMenu.Items.Add("-");
            
            #region Show All

            var showAllItem = new ToolStripMenuItem
            {
                Text = CultureService.GetString(CultureText.menuItem_ShowAll_Text),
            };
            showAllItem.Click += (o, e) =>
            {
                ShowAllColums();
            };
            contextMenu.Items.Add(showAllItem);
            #endregion 

            #region Auto Size All

            var autoSizeAllItem = new ToolStripMenuItem
            {
                Text = CultureService.GetString(CultureText.menuItem_AutoSizeAll_Text),
            };
            autoSizeAllItem.Click += (o, e) =>
            {
                Cursor.Current = Cursors.WaitCursor;
                AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
                Cursor.Current = Cursors.Default;
            };
            contextMenu.Items.Add(autoSizeAllItem);
            #endregion

            contextMenu.Items.Add("-");

            #region Column Name checkButton
            foreach (DataGridViewColumn gridColumn in this.Columns)
            {
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
                    contextMenu.Items.Add(button);
                }
            }
            #endregion 

            return contextMenu;
        }




        private bool MosueUpHandlerForColumnContextMenu(MouseEventArgs e)
        {
            var hitTestInfo = this.HitTest(e.X, e.Y);

            /*Ha fejlcécre kattint, akkor egy másik kontext menü jelenik meg.*/
            if (hitTestInfo.Type == DataGridViewHitTestType.ColumnHeader)
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    CreateColumnHeaderContextMenuStrip(hitTestInfo.ColumnIndex).Show(this, new System.Drawing.Point(e.X, e.Y));
                    return true;
                }
            }
            return false;
        }
        #endregion 

#endif

        #region Zebra Rows
        /// <summary>
        /// Zebra csikozás engedélyezése.
        /// </summary>
        [Category("KNV")]
        [Description("Zebra csikozás engedélyezése.")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool ZebraRow
        {
            get { return _zebraRows; }
            set
            {
                _zebraRows = value;
                Refresh();
            }
        }
        private bool _zebraRows = true;

        [Category("KNV")]
        public Color FirstZebraColor 
        {
            get { return _firstZebraColor; }
            set { _firstZebraColor = value; }
        }

        private Color _firstZebraColor = Color.Bisque;
       
        [Category("KNV")]
        public Color SecondZebraColor 
        {
            get { return _secondZebraColor; }
            set { _secondZebraColor = value; }
        }
        private Color _secondZebraColor = Color.White;

        private void RowPrePaintHandlerForZebraRows(DataGridViewRowPrePaintEventArgs e)
        {
            if (_zebraRows)
            {
                var rowIndex = e.RowIndex;
                if (rowIndex % 2 == 0)
                    Rows[rowIndex].DefaultCellStyle.BackColor = _firstZebraColor;
                else
                    Rows[rowIndex].DefaultCellStyle.BackColor = _secondZebraColor;
            }
        }
        #endregion

        #region Last Displayed Column Fill
        /// <summary>
        /// Utolsó látható oszlop mindig kitölti a rendelkezésre álló helyet.
        /// </summary>
        [Category("KNV")]
        [DefaultValueAttribute(true)]
        [Description("Utolsó látható oszlop mindig kitölti a rendelkezésre álló helyet. Minden mező AutoSizeMode-ja legyen: NONE")]
        public bool LastColumnAlwaysFill
        {
            get { return _lastColumnAlwaysFill; }
            set
            {
                _lastColumnAlwaysFill = value;
            }
        }
        private bool _lastColumnAlwaysFill = true;

        private void LastDisplayedColumnFillHandler()
        {
            if (_lastColumnAlwaysFill && !DesignMode)
            {
                if (VisibleWidth < this.ClientSize.Width)
                {
                    var lastColumn = this.Columns.GetLastColumn(DataGridViewElementStates.Visible, DataGridViewElementStates.None);
                    if (lastColumn != null)
                        lastColumn.Width += this.ClientSize.Width - VisibleWidth;
                }
            }
        }

        #endregion

        #region Data Error Handler
        protected override void OnDataError(bool displayErrorDialogIfNoHandler, DataGridViewDataErrorEventArgs e)
        {
            /*base.OnDataError(displayErrorDialogIfNoHandler, e);*/
            MessageBox.Show(e.Exception.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
        #endregion

    }
}
