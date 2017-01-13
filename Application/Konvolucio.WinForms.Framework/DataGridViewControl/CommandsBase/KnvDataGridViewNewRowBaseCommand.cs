// -----------------------------------------------------------------------
// <copyright file="AddMessageFilter.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Konvolucio.WinForms.Framework
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Linq;
    using System.Reflection;
    using System.Windows.Forms;
    using Properties;

    /// <summary>
    /// A BindingList.AddNew metódusát hívja meg.
    /// - A BingingList elemének kell, hogy legyen publikus Default Konstrukora.
    /// - BindingList.AllowNew = true kell hogy legyen.
    /// - Szálbiztos.
    /// <code>
    ///    public NewRowCommand(DataGridView dataGridView, IBindingList collection) : base(dataGridView, collection)
    ///    {
    ///        Image = Resources.New_24x24;
    ///        Text = @"New";
    ///        ShortcutKeys = Keys.Alt | Keys.N;
    ///    }
    /// </code>
    /// </summary>
    public class KnvDataGridViewNewRowBaseCommand: ToolStripMenuItem 
    {
        private readonly IBindingList _collection;
        private readonly DataGridView _dataGridView;

        protected KnvDataGridViewNewRowBaseCommand(DataGridView dataGridView, IBindingList collection)
        {
            // ReSharper disable once DoNotCallOverridableMethodsInConstructor
            Image = Resources.New_24x24;
            // ReSharper disable once DoNotCallOverridableMethodsInConstructor
            Text = CultureService.GetString(CultureText.menuItem_New_Text);

            if (dataGridView.ContextMenuStrip == null)
                throw new ArgumentNullException(@"dataGridView", @"DataGridView-nak mar kellne, hogy legyen ContextMenuStrip-je!");
            _collection = collection;
            _dataGridView = dataGridView;
            dataGridView.ContextMenuStrip.Opening += new CancelEventHandler(ContextMenuStrip_Opening);
 
        }

        private void ContextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            Enabled =  _collection.AllowNew;
        }

        protected override void OnClick(EventArgs e)
        {
            if(Enabled) Command();            
            base.OnClick(e);
        }

        protected virtual void Command()
        {
            lock (_collection.SyncRoot)
            {
                try
                {
                    var selectedCell = _dataGridView.SelectedCells;
                    var index = selectedCell.Count > 0 ? selectedCell[0].RowIndex : 0;

                    Cursor.Current = Cursors.WaitCursor;
                    var type = _collection.GetType().GetProperty("Item").PropertyType;
                    var newItem = MakeNewItem(type);
                    _dataGridView.ClearSelection(); 
                    if (newItem != null)
                    {
                        if (index != 0)
                            _collection.Insert(index + 1, newItem);
                        else
                            _collection.Add(newItem);

                        var newRow = _dataGridView.Rows.Cast<DataGridViewRow>().FirstOrDefault(n => n.DataBoundItem == newItem);
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
        /// Alaphelyzetben a Paraméter nélküli konstruktort hívja.
        /// Ez alapján készül az új elem.
        /// Ha Null-al térsz vissza, akkor megszaithatod az új elem felvételét. 
        ///
        ///<example>
        ///public override object MakeNewItem(Type type)
        ///{
        ///    PersonItem newItem = null;
        ///    if (_personForm.ShowDialog() == DialogResult.OK)
        ///    {
        ///        newItem = new PersonItem
        ///        {
        ///            FirstName = _personForm.FirstName,
        ///            LastName = _personForm.LastName,
        ///            Age = _personForm.Age
        ///        };
        ///        _collection.Add(newItem);
        ///    }
        ///    return newItem;
        ///}
        /// </example> 
        /// 
        /// 
        /// </summary>
        protected virtual object MakeNewItem(Type type)
        {
            return Activator.CreateInstance(type);
        }
    }
}
