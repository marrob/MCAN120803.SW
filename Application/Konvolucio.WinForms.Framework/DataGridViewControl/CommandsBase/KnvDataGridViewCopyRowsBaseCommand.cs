// -----------------------------------------------------------------------
// <copyright file="CopyCommand.cs" company="">
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

    /// <summary>
    /// 
    /// <code>
    /// internal sealed class CopyRowCommand : KnvDataGridViewCopyRowsBaseCommand
    /// {
    ///     public CopyRowCommand(DataGridView dataGridView, IBindingList collection) : base(dataGridView, collection)
    ///     {
    ///         Image = Resources.Copy_24x24;
    ///         Text = @"Copy";
    ///         ShortcutKeys = Keys.Alt | Keys.D;
    ///     }
    /// }
    /// </code>
    /// </summary>
    public class KnvDataGridViewCopyRowsBaseCommand : ToolStripMenuItem
    {
        private readonly IBindingList _collection;
        private readonly DataGridView _dataGridView;

        protected KnvDataGridViewCopyRowsBaseCommand(DataGridView dataGridView, IBindingList collection)
        {
            // ReSharper disable once DoNotCallOverridableMethodsInConstructor
            Text = CultureService.GetString(CultureText.menuItem_Copy_Text);
            // ReSharper disable once DoNotCallOverridableMethodsInConstructor
            Image = Resources.Copy_24x24;
            ShortcutKeys = Keys.Control | Keys.C;

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

                var selectedRows = _dataGridView.SelectedRows;

                //Az "items"-be mentem a kiejlÖlt sorokat, ezt a töbmöt index alapján növekvő sorrendbe rendezem.
                var items = new DataGridViewRow[selectedRows.Count];
           
                //másolat minden elemről.
                selectedRows.CopyTo(items, 0);
                
                //Index alapján növekvő sorrendbe rendezés, így a kijelölés irányától független lesz a másolás.
                items = items.OrderBy(n => n.Index).ToArray();

                //Ez a vágólap objectuma.
                var data = new DataObject();
                
                //A vágólapra egy obejktum tömb kerül. 
                var objects = new object[items.Length];

                //Kötött elemek másolása az objektum tömbe. 
                //Itt történik az elemek másolása is "Clone".
                for (var i = 0; i < items.Length; i++)
                    objects[i] = ((ICloneable) items[i].DataBoundItem).Clone();

                //Kollekció elemeinek típusának megszerzése, ez alapján meg lehet mondani, hogy szerializálható-e.
                var type = _collection.GetType().GetProperty("Item").PropertyType;

                if (!type.IsSerializable)
                    throw new NotSupportedException(@"A típus nem szerializálható.");

                //az objektum tömb a vágólapra 
                data.SetData(typeof (object[]), objects);

                Clipboard.SetDataObject(data);
                
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }
    }
}
