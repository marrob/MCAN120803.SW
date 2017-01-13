
namespace Konvolucio.WinForms.Framework
{
    using System;
    using System.ComponentModel;
    using System.Windows.Forms;
    using Properties;

    /// <summary>
    /// DataGridView-ból vagy belőle származtatott obejtumból töröl egy vagy több sort. 
    /// A kijelölt sorok alapján.
    /// Szálbiztos.
    /// Használata a következő:
    /// <code>
    /// internal sealed class DeleteRowCommand : KnvDataGridViewDeleteRowsBaseCommand
    /// {
    ///     public DeleteRowCommand(DataGridView dataGridView, IBindingList collection) : base(dataGridView, collection)
    ///     {
    ///        Image = Resources.Delete_24x24;
    ///        Text = @"Delete";
    ///        ShortcutKeys = Keys.Alt | Keys.D;
    ///     }
    /// }
    /// </code>
    /// </summary>
    public class KnvDataGridViewDeleteRowsBaseCommand : ToolStripMenuItem
    {
        private readonly IBindingList _collection;
        private readonly DataGridView _dataGridView;

        protected KnvDataGridViewDeleteRowsBaseCommand(DataGridView dataGridView, IBindingList collection)
        {
            // ReSharper disable once DoNotCallOverridableMethodsInConstructor
            Text = CultureService.GetString(CultureText.menuItem_Delete_Text);
            // ReSharper disable once DoNotCallOverridableMethodsInConstructor
            Image = Resources.Delete_24x24;
            ShortcutKeys = Keys.Delete;

            if (dataGridView.ContextMenuStrip == null)
                throw new ArgumentNullException(@"dataGridView", @"DataGridView-nak mar kellne, hogy legyen ContextMenuStrip-je!");
            _collection = collection;
            _dataGridView = dataGridView;
            dataGridView.ContextMenuStrip.Opening += new CancelEventHandler(ContextMenuStrip_Opening);
        }

        private void ContextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            Enabled = _dataGridView.SelectedRows.Count > 0 && _collection.AllowRemove;
        }

        protected override void OnClick(EventArgs e)
        {
            if(Enabled) Command();
            base.OnClick(e);
        }
        protected virtual void Command()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                lock (_collection.SyncRoot)
                {
                    foreach (DataGridViewRow item in _dataGridView.SelectedRows)
                        _collection.Remove(item.DataBoundItem);
                }
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }
    }
}
