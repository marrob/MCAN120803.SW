// -----------------------------------------------------------------------
// <copyright file="PasteCommand.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Konvolucio.WinForms.Framework
{
    using System;
    using System.ComponentModel;
    using System.Linq;
    using System.Windows.Forms;
    using Properties;

    public class KnvDataGridViewPasteRowsBaseCommand : ToolStripMenuItem
    {
        private readonly IDataObject _retviredDataObject;

        private readonly IBindingList _collection;
        private readonly DataGridView _dataGridView;

        protected KnvDataGridViewPasteRowsBaseCommand(DataGridView dataGridView, IBindingList collection)
        {
            // ReSharper disable once DoNotCallOverridableMethodsInConstructor
            Text = CultureService.GetString(CultureText.menuItem_Paste_Text);
            // ReSharper disable once DoNotCallOverridableMethodsInConstructor
            Image = Resources.Paste_24x24;
            ShortcutKeys = Keys.Control | Keys.V;

            if (dataGridView.ContextMenuStrip == null)
                throw new ArgumentNullException(@"dataGridView", @"DataGridView-nak mar kellne, hogy legyen ContextMenuStrip-je!");
            _collection = collection;
            _dataGridView = dataGridView;
            _retviredDataObject = Clipboard.GetDataObject();
            dataGridView.ContextMenuStrip.Opening += new CancelEventHandler(ContextMenuStrip_Opening);
        }
        private void ContextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            //_retviredDataObject = Clipboard.GetDataObject();
            Enabled = (_retviredDataObject != null) && _retviredDataObject.GetDataPresent(typeof(object[])) ;
        }

        protected override void OnClick(EventArgs e)
        {
            if (Enabled) Command();
            base.OnClick(e);
        }

        protected virtual void Command()
        {
            lock (_collection.SyncRoot)
            {
                try
                {
                    Cursor.Current = Cursors.WaitCursor;
                    
                    //Ide kerülnek a kiejlölt sorok
                    var selectedRows = new DataGridViewRow[_dataGridView.SelectedRows.Count];
                    
                    //Kijelölt sorok mentése a selectedRows-ba
                    _dataGridView.SelectedRows.CopyTo(selectedRows, 0);

                    //Ha van kijelölt sor, akkor az első első sor indexe kell.
                    var index = !selectedRows.Any() ? 0 : selectedRows[0].Index;

                    //Eltrölöl minde kijelölés, mivel a beilesztett sor(ok) lesznek kiejlölve.
                    _dataGridView.ClearSelection(); 

                    //vágólapon lévő object[] megszerzése.
                    var items = _retviredDataObject.GetData(typeof(object[]));

                    if (items != null)
                        foreach (var item in (Array) items)
                        {
                            //Az új sor az akutális index alá fog kerülni.
                            AddItemToCollection(++index, item);
                            var newRow = _dataGridView.Rows.Cast<DataGridViewRow>().FirstOrDefault(n => n.DataBoundItem == item);
                            if(newRow!= null)
                                newRow.Selected = true;
                        }
                }
                finally
                {
                    Cursor.Current = Cursors.Default;
                }
            }
        }

        /// <summary>
        /// Az elem mielőtt hozzáadásra kerülene a kollekcióhoz, még módosíthatsz rajta. Pl egyedi legyen a Name-mező.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="item"></param>
        protected virtual void AddItemToCollection(int index, object item)
        {
            if (_collection.Count > index)
                _collection.Insert(index, item);
            else
                _collection.Add(item);
        }
    }
}
