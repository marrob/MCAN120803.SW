// -----------------------------------------------------------------------
// <copyright file="Tools.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Konvolucio.WinForms.Framework
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;




    /// <summary>
    /// Az a DatagGridView ami megvalósítja, azon a Sorok áthelyezhetőek 
    /// Drag & Drop módszerrel.
    /// </summary>
    internal class RowReOrdering
    {

        private Rectangle _dragBoxFromMouseDown;
        private int _rowIndexFromMouseDown;
        private readonly DataGridView _dataGridView;

        /// <summary>
        /// Konstructor
        /// </summary>
        public RowReOrdering(DataGridView dataGridView)
        {

            if (dataGridView.VirtualMode)
                throw new ArgumentOutOfRangeException(@"VirtualMode", true, @"Virtual módot nem támogatom...");


            _dataGridView = dataGridView;
            _dataGridView.MouseMove += DataGridView_MouseMove;
            _dataGridView.MouseDown += DataGridView_MouseDown;
            _dataGridView.DragOver += DdataGridView_DragOver;
            _dataGridView.DragDrop += DataGridView_DragDrop;
        }

        /// <summary>
        /// Az egér bal gombját megnyomta és mozogatja az egeret.
        /// </summary>
        private void DataGridView_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                /* If the mouse moves outside the rectangle, start the drag. */
                if (_dragBoxFromMouseDown != Rectangle.Empty &&
                    !_dragBoxFromMouseDown.Contains(e.X, e.Y))
                {
                    /* Proceed with the drag and drop, passing in the list item. */
                    DragDropEffects dropEffect = _dataGridView.DoDragDrop(
                        _dataGridView.Rows[_rowIndexFromMouseDown], DragDropEffects.Move);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void DataGridView_MouseDown(object sender, MouseEventArgs e)
        {
            /* Get the index of the item the mouse is below. */
            var hitTestInfo = _dataGridView.HitTest(e.X, e.Y);

            _rowIndexFromMouseDown = hitTestInfo.RowIndex;

            if (_rowIndexFromMouseDown != -1)
            {
                /* Remember the point where the mouse down occurred. */
                /* The DragSize indicates the size that the mouse can move */
                /* before a drag event should be started. */
                Size dragSize = SystemInformation.DragSize;
                /* Create a rectangle using the DragSize, with the mouse position being */
                /* at the center of the rectangle. */
                _dragBoxFromMouseDown = new Rectangle(new Point(e.X - (dragSize.Width/2),
                        e.Y - (dragSize.Height/2)),
                    dragSize);
            }
            else
            {
                /*Reset the rectangle if the mouse is not over an item in the ListBox.*/
                _dragBoxFromMouseDown = Rectangle.Empty;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void DdataGridView_DragOver(object sender, DragEventArgs e)
        {
            bool isValid = false;

            /*converted to client coordinates.*/
            Point clientPoint = _dataGridView.PointToClient(new Point(e.X, e.Y));
            var hitTestInfo = _dataGridView.HitTest(clientPoint.X, clientPoint.Y);

            /* Sor mozgatása a sor fejlécévnek segitségével */
            if (hitTestInfo.Type == DataGridViewHitTestType.RowHeader)
            {
                e.Effect = DragDropEffects.Move;
                isValid = true;
            }

            /*Törli az Effectet, ha nem volt érvényes...*/
            if (!isValid)
            {
                e.Effect = DragDropEffects.None;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void DataGridView_DragDrop(object sender, DragEventArgs e)
        {
            /* get the currently hovered row that the items will be dragged to */
            /* A datagridhez adja meg az teljes képernyőre vonatkozó koordinátát. */
            /* The mouse locations are relative to the screen, so they must be */
            /* converted to client coordinates. */
            Point clientPoint = _dataGridView.PointToClient(new Point(e.X, e.Y));
            var hitTestInfo = _dataGridView.HitTest(clientPoint.X, clientPoint.Y);

            /* Sor áthelyezése */
            /* http://social.msdn.microsoft.com/Forums/windows/en-US/9c38740d-be4a-4d50-99ef-1ac4c8c48d92/how-to-re-order-multiple-rows-of-datagrid-in-windows-forms-application-ie-c?forum=winforms */
            /* Get the row index of the item the mouse is below. */
            var rowIndexOfItemUnderMouseToDrop = hitTestInfo.RowIndex;

            /* Akkor érvrényes az eldobás, ha sorfejléc fölött van és Move volt az efekt.*/
            if (hitTestInfo.Type == DataGridViewHitTestType.RowHeader && e.Effect == DragDropEffects.Move)
            {
                DataGridViewRow rowToMove = e.Data.GetData(typeof(DataGridViewRow)) as DataGridViewRow;

                if (rowToMove != null)
                {
                    if (_dataGridView.DataSource != null)
                    {
                        if (_dataGridView.DataSource is IRowReOredable)
                        {
                            if (rowToMove.Index != -1)
                            {
                                ((IRowReOredable) _dataGridView.DataSource).ItemMoveTo(rowToMove.DataBoundItem, rowIndexOfItemUnderMouseToDrop);
                            }
                        }
                        else
                        {
                            throw new NotSupportedException("Az elem nem áthelyezhető, mert a kollekció nem támogatja a " + typeof(IRowReOredable).FullName + " interfészt. Valósísd meg!");
                        }
                    }
                }
            }
        }
    }
}
