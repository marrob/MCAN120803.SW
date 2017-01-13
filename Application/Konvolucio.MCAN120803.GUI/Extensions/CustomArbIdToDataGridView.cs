// -----------------------------------------------------------------------
// <copyright file="CustomArbIdToDataGridView.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Konvolucio.MCAN120803.GUI.Extensions
{
    using System.ComponentModel;
    using System.Linq;
    using System.Windows.Forms;
    using Common;
    using WinForms.Framework;


    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class CustomArbIdToDataGridView
    {
        public CustomArbIdColumnCollection CustomArbIdColumns
        {
            set
            {
                _customArbIdColumns = value;
                InitCustomColumns();
            }
        }

        public bool ReadOnly { get; set; }


        private CustomArbIdColumnCollection _customArbIdColumns;

        private readonly DataGridView _dataGridView;

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="dataGridView"></param>
        public CustomArbIdToDataGridView(DataGridView dataGridView )
        {
            _dataGridView = dataGridView;
        }

        /// <summary>
        /// Eseményekre itt íratkozik fel.
        /// </summary>
        private void InitCustomColumns()
        {
            if (_customArbIdColumns == null)
            {
                RemoveAllCustomArbColumns();
            }
            else
            {
                /*Oszlopok beszurása*/
                foreach (var item in _customArbIdColumns)
                {
                    if (_dataGridView.Columns.Cast<DataGridViewColumn>().FirstOrDefault(n => n.Tag == item) == null)
                    {
                        /*Hozzáadja ha még nincs*/
                        var dgvtbc = new DataGridViewTextBoxColumn()
                        {
                            HeaderText = item.Name,
                            Name = item.Name,
                            ValueType = typeof(string),
                            ToolTipText = item.Description,
                            Tag = item,
                            Visible = true,
                            ReadOnly =  ReadOnly,
                            AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                        };
                        _dataGridView.Columns.Add(dgvtbc);
                    }
                }
            }

            if (_customArbIdColumns != null)
            {
                _customArbIdColumns.ListChanged += new ListChangedEventHandler(CustomArbIdColumns_ListChanged);
                _customArbIdColumns.ListChanging += new ListChangingEventHandler<CustomArbIdColumnItem>(CustomArbIdColumns_ListChanging);
            }
        }

        /// <summary>
        /// A lista változni fog...
        /// </summary>
        private void CustomArbIdColumns_ListChanging(object sender, ListChangingEventArgs<CustomArbIdColumnItem> e)
        {
            switch (e.ListChangedType)
            {
                case ListChangingType.ItemRemoving:
                {
                    var toRemoveColumn = _dataGridView.Columns.Cast<DataGridViewColumn>().FirstOrDefault(n => n.Tag == e.Item);
                    if (toRemoveColumn != null)
                        _dataGridView.Columns.Remove(toRemoveColumn);
                    break;
                }
                case ListChangingType.Clearing:
                {
                    RemoveAllCustomArbColumns();
                        break;
                }
            }
        }

        /// <summary>
        /// Oszlopok száma vagy tartlama változott.
        /// </summary>
        private void CustomArbIdColumns_ListChanged(object sender, ListChangedEventArgs e)
        {
            switch (e.ListChangedType)
            {
                case ListChangedType.ItemAdded:
                    {
                        var bindingList = sender as IBindingList;
                        if (bindingList != null)
                        {
                            var item = bindingList[e.NewIndex] as CustomArbIdColumnItem;
                            if (item != null)
                            {
                                if (_dataGridView.Columns.Cast<DataGridViewColumn>().FirstOrDefault(n => n.Tag == item) == null)
                                {
                                    /*Hozzáadja ha még nincs*/
                                    var dgvtbc = new DataGridViewTextBoxColumn()
                                    {
                                        HeaderText = item.Name,
                                        Name = item.Name,
                                        AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                                        ToolTipText = item.Description,
                                        Tag = item,
                                        ReadOnly = ReadOnly,
                                        Visible = true,
                                    };
                                    _dataGridView.Columns.Add(dgvtbc);
                                }
                            }
                        }
                        break;
                    }

                case ListChangedType.ItemChanged:
                    {
                        var bindingList = sender as IBindingList;
                        if (bindingList != null)
                        {
                            var item = bindingList[e.NewIndex] as CustomArbIdColumnItem;
                            if (item != null)
                            {
                                if (e.PropertyDescriptor.DisplayName == "Name")
                                {
                                    var toRenameItem = _dataGridView.Columns.Cast<DataGridViewColumn>().FirstOrDefault(n => n.Tag == item);
                                    if (toRenameItem != null)
                                    {
                                        var newName = e.PropertyDescriptor.GetValue(item);
                                        if (newName != null)
                                        {
                                            toRenameItem.Name = newName.ToString();
                                            toRenameItem.HeaderText = newName.ToString();
                                        }
                                    }
                                }
                            }
                            else
                            {
                                /*Egyéb esetben rajzolja újra, a CellFormatting lefrissíti a változásokat*/
                                /*Amikor a Customizer értékekt frissíti a felhszáló*/
                                _dataGridView.Refresh();
                            }
                        }
                        break;
                    }
            }
        }

        /// <summary>
        /// Az egyedi oszlopokat törli.
        /// </summary>
        public void RemoveAllCustomArbColumns()
        {
            /*Meglévő Custom oszlopok törlése*/
            var toRemoveColumnsx = _dataGridView.Columns.Cast<DataGridViewColumn>().Where(n => n.Tag is CustomArbIdColumnItem).ToArray();/*Tömb lesz.*/
            /*Ha listát tömbé konvertálod, majd a tömbön lépkedve törlöd a lista elemeti, az úgy OK.*/
            foreach (var item in toRemoveColumnsx)
                _dataGridView.Columns.Remove(item);
        }
    }
}
