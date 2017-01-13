// -----------------------------------------------------------------------
// <copyright file="Tools.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Konvolucio.WinForms.Framework
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>

    internal class NewRowAlwaysShow
    {
        private readonly DataGridView _dataGridView;

        public NewRowAlwaysShow(DataGridView dataGridView)
        {

            _dataGridView = dataGridView;
            dataGridView.DataSourceChanged += new System.EventHandler(dataGridView_DataSourceChanged);
        }
        /// <summary>
        /// Légy óvatos, hogy az alakalmazás élete során csak 1x változzon, mert különben beragandak az esmemények.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView_DataSourceChanged(object sender, EventArgs e)
        {
            if (_dataGridView.DataSource != null && _dataGridView.DataSource is IBindingList)
                ((IBindingList)_dataGridView.DataSource).ListChanged += DataSource_ListChanged;
        }

        private void DataSource_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (e.ListChangedType == ListChangedType.ItemAdded)
            {
                if (_dataGridView.RowCount > 1)
                {
                    /*Új elem hozzáadásakor az utolsó sorba lép a Data Gridben.. oda teszi az új elemet...*/
                    _dataGridView.FirstDisplayedScrollingRowIndex = _dataGridView.RowCount - 1;
                    _dataGridView.CurrentCell = _dataGridView[0, _dataGridView.RowCount - 1];
                }
            }
        }
    }
}
