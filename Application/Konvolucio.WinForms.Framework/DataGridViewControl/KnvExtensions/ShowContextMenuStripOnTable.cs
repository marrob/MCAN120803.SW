// -----------------------------------------------------------------------
// <copyright file="Tools.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Konvolucio.WinForms.Framework
{
    using System.Drawing;
    using System.Windows.Forms;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>

    public class ShowContextMenuStripOnTable
    {

        private readonly DataGridView _dataGridView;

        public ShowContextMenuStripOnTable(DataGridView dataGridView, ContextMenuStrip contextMenuStrip)
        {
            _dataGridView = dataGridView;
            _dataGridView.ContextMenuStrip.MouseUp += DataGridView_MouseUp;
        }

        private void DataGridView_MouseUp(object sender, MouseEventArgs e)
        {
            var hitTestInfo = _dataGridView.HitTest(e.X, e.Y);

            if (hitTestInfo.Type == DataGridViewHitTestType.None)
            {
                /*Ha Griden kívül kattint, akkor megszünteti a kijelölést.*/
                /*Ezzel szokott probléma lenni..*/
                _dataGridView.ClearSelection();
                _dataGridView.CurrentCell = null;
            }

            if (hitTestInfo.Type == DataGridViewHitTestType.Cell ||
                hitTestInfo.Type == DataGridViewHitTestType.None ||
                hitTestInfo.Type == DataGridViewHitTestType.RowHeader)
            {
                if ((hitTestInfo.RowIndex != -1) && (hitTestInfo.ColumnIndex != -1))
                {
                    /*Aa szerkesztés alatt áll a cella*/
                    /*akkor nem jelenhet meg a contex menu, helyette a win menüje jelenik meg..*/
                    if (_dataGridView.Rows[hitTestInfo.RowIndex].Cells[hitTestInfo.ColumnIndex].IsInEditMode)
                        return;
                }

                if (e.Button == MouseButtons.Right)
                {
                    var dataGridView = sender as DataGridView;
                    if(dataGridView!= null)
                        dataGridView.ContextMenuStrip.Show(_dataGridView, new Point(e.X, e.Y));
                }
            }
        }
    }
}
