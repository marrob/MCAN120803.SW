// -----------------------------------------------------------------------
// <copyright file="CopyCommand.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Konvolucio.WinForms.Framework
{
    using System;
    using System.ComponentModel;
    using System.Windows.Forms;
    using Properties;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class KnvDataGridViewCutRowsBaseCommand : ToolStripMenuItem
    {
        private readonly IBindingList _collection;
        private readonly DataGridView _dataGridView;

        protected KnvDataGridViewCutRowsBaseCommand(DataGridView dataGridView, IBindingList collection)
        {
            // ReSharper disable once DoNotCallOverridableMethodsInConstructor
            Text = CultureService.GetString(CultureText.menuItem_Cut_Text);
            // ReSharper disable once DoNotCallOverridableMethodsInConstructor
            Image = Resources.Cut_24x24;
            ShortcutKeys = Keys.Control | Keys.X;

            if (dataGridView.ContextMenuStrip == null)
                throw new ArgumentNullException(@"dataGridView", @"DataGridView-nak mar kellne, hogy legyen ContextMenuStrip-je!");
            _collection = collection;
            _dataGridView = dataGridView;
            dataGridView.ContextMenuStrip.Opening += new CancelEventHandler(ContextMenuStrip_Opening);
        }

        private void ContextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            Enabled = _dataGridView.SelectedRows.Count > 0;
        }

        protected override void OnClick(EventArgs e)
        {
            if (Enabled) Command();
            base.OnClick(e);
        }

        protected virtual void Command()
        {
            try
            {
                
                Cursor.Current = Cursors.WaitCursor;
                
                Clipboard.Clear();

                var items = _dataGridView.SelectedRows;

                var data = new DataObject();

                var objects = new object[items.Count];

                for (var i = 0; i < items.Count; i++)
                    objects[i] = ((ICloneable) items[i].DataBoundItem).Clone();

                var selectedRows = new DataGridViewRow[items.Count];
                _dataGridView.SelectedRows.CopyTo(selectedRows, 0);

                foreach (DataGridViewRow row in selectedRows)
                    _dataGridView.Rows.Remove(row);

                var type = _collection.GetType().GetProperty("Item").PropertyType;

                if(!type.IsSerializable)
                    throw new NotSupportedException(@"A típus nem szerializálható.");

                data.SetData(typeof(object[]), objects);

                Clipboard.SetDataObject(data);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }
    }
}
