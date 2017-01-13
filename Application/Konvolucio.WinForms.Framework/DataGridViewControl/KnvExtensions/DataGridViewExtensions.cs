
namespace Konvolucio.WinForms.Framework
{
    using System;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Reflection;
    using System.Windows.Forms;
    using System.Windows.Forms.VisualStyles;

    public static class DataGridViewExtensions
    {
        /// <summary>
        /// Double Buffering vezérlése.
        /// </summary>
        /// <param name="dataGridView"></param>
        /// <param name="setting"></param>
        public static void KnvDoubleBuffered(this DataGridView dataGridView, bool setting)
        {
            Type dgvType = dataGridView.GetType();
            PropertyInfo pi = dgvType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(dataGridView, setting, null);
        }

        /// <summary>
        /// Mindig az utoljára hozáadott shorhoz igazítja a Scroll Bar-t
        /// </summary>
        /// <param name="dataGridView"></param>
        public static void KnvNewRowAlwaysShow(this DataGridView dataGridView)
        {
            var newRowAlwaysShow = new NewRowAlwaysShow(dataGridView);
        }

        /// <summary>
        /// A DatagGridView ami megvalósítja, azon a Sorok áthelyezhetőek 
        /// Drag &amp; Drop módszerrel.
        /// </summary>
        /// <param name="dataGridView"></param>
        public static void KnvRowReOrdering(this DataGridView dataGridView)
        {
            var rowReOrdering = new RowReOrdering(dataGridView);
        }

        /// <summary>
        /// Kezeli a DataGridView.ContextMenuStrip Tualjdonsátág.
        /// Megjelniti: A Cellában, Tábla üres helyén, és a sor fejlécén.
        /// </summary>
        public static void KnvShowContextMenuStripOnCellOrRowHeaderOrEmpty(this DataGridView dataGridView)
        {
            if (dataGridView.ContextMenuStrip == null)
                throw new ArgumentNullException(@"dataGridView", @"DataGridView-nak mar kellne, hogy legyen ContextMenuStrip-je!");

            var lastMousLocation = new Point();
         
            //Elmenetm az egér pozicióját, mert ez alapján határozom meg ContextMenu megjelnjen, vagy sem.
            dataGridView.MouseUp += (o, e) =>
            {
                lastMousLocation = e.Location;
            };

            dataGridView.ContextMenuStrip.Opening += (o, e) =>
            {
                var hitTestInfo = dataGridView.HitTest(lastMousLocation.X, lastMousLocation.Y);

                if (hitTestInfo.Type == DataGridViewHitTestType.Cell ||
                    hitTestInfo.Type == DataGridViewHitTestType.None ||
                    hitTestInfo.Type == DataGridViewHitTestType.RowHeader)
                {

                    if ((hitTestInfo.RowIndex != -1) && (hitTestInfo.ColumnIndex != -1))
                    {
                        /*Aa szerkesztés alatt áll a cella*/
                        /*akkor nem jelenhet meg a contex menu, helyette a win menüje jelenik meg..*/
                        if (dataGridView.Rows[hitTestInfo.RowIndex].Cells[hitTestInfo.ColumnIndex].IsInEditMode)
                            e.Cancel = true;
                    }
                }
                else
                {
                    e.Cancel = true;
                }
            };
            dataGridView.ContextMenuStrip.KeyUp += (o, e) =>
            {
                if (e.KeyCode == Keys.Apps)
                {
                    int row = dataGridView.SelectedCells[0].RowIndex;
                    int colum = dataGridView.SelectedCells[0].ColumnIndex;
                    Rectangle rec = dataGridView.GetCellDisplayRectangle(colum, row, true);
                    rec.Y += dataGridView.RowTemplate.Height;
                    dataGridView.ContextMenuStrip.Show(dataGridView, new Point(rec.X, rec.Y));
                }
            };

            //Ez nem megy
            dataGridView.KeyUp += (o, e) =>
            {
                if (e.KeyCode == Keys.Apps)
                {
                    int row = dataGridView.SelectedCells[0].RowIndex;
                    int colum = dataGridView.SelectedCells[0].ColumnIndex;
                    Rectangle rec = dataGridView.GetCellDisplayRectangle(colum, row, true);
                    rec.Y += dataGridView.RowTemplate.Height;
                    dataGridView.ContextMenuStrip.Show(dataGridView, new Point(rec.X, rec.Y));
                }
            };
        }

        /// <summary>
        /// 
        /// </summary>
        public static void KnvColumnHeaderContextMenu(this KnvDataGridView dataGridView)
        {
            new ColumnHeaderContextMenu(dataGridView);
        }

    }
}
