// -----------------------------------------------------------------------
// <copyright file="Presenter.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Konvolucio.MCAN120803.GUI.AppModules.Tools
{
    using System.ComponentModel;
    using System.Windows.Forms;
    using CanTx.Commands;
    using CanTx.Model;
    using CanTx.View;
    using Common;
    using DataStorage;
    using Tools.Model;
    using WinForms.Framework;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class Presenter
    {
        private readonly MultiPageCollection _pagesView;
        private readonly Storage _storage;
        private readonly CustomArbIdColumnCollection _customArbIdColumns;

        public Presenter(Storage storage, MultiPageCollection pagesView, ToolTableCollection toolTables, CustomArbIdColumnCollection customArbIdColumns)
        {
            var toolTables1 = toolTables;
            _pagesView = pagesView;
            _storage = storage;
            _customArbIdColumns = customArbIdColumns;

            toolTables1.ListChanged += new ListChangedEventHandler(ToolTables_ListChanged);
            toolTables1.ListChanging += new ListChangingEventHandler<ToolTableItem>(ToolTables_ListChanging);
           
            #region Project események
            EventAggregator.Instance.Subscribe<StorageAppEvent>(e1 =>
            {
                switch (e1.ChangingType)
                {
                    case FileChangingType.Loading:
                    {
                        e1.Storage.TableLayouts.Clear();
                        _pagesView.Clear();
                        break;
                    }
                    case FileChangingType.LoadComplete:
                    {
                        var i = 0;
                        foreach (var pages in pagesView)
                        {
                            var senderGrid = pages.PageControl as SenderGridView;
                            if (senderGrid != null && e1.Storage.TableLayouts.Count > i)
                                 senderGrid.GridLayout = e1.Storage.TableLayouts[i++];

                            if(senderGrid != null)
                                 senderGrid.CustomArbIdColumns = _customArbIdColumns;
                        }
                        break;
                    }
                    case FileChangingType.Saving:
                    {
                        e1.Storage.TableLayouts.Clear();
                        foreach (var pages in pagesView)
                        {
                            var senderGrid = pages.PageControl as SenderGridView;
                            if (senderGrid != null)
                                e1.Storage.TableLayouts.Add(senderGrid.GridLayout);
                        }

                        break;
                    }

                    case FileChangingType.ContentChanged:
                    {
                        break;
                    }
                }
            });
            #endregion 
        
        }

        void ToolTables_ListChanging(object sender, ListChangingEventArgs<ToolTableItem> e)
        {
            if (e.ListChangedType == ListChangingType.ItemRemoving)
            {
                _pagesView.Remove(e.Item.Name);
            }
        }

        private void ToolTables_ListChanged(object sender, ListChangedEventArgs e)
        {

            if (e.ListChangedType == ListChangedType.ItemAdded)
            {
                var bindingList = sender as IBindingList;
                if (bindingList != null)
                {
                    var tableItem = bindingList[e.NewIndex] as ToolTableItem;
                    switch (tableItem.ToolType)
                    {
                        case ToolTypes.CAN:
                        {
                            var newControl = new SenderGridView();
                            if (tableItem.TableObject == null)
                                tableItem.TableObject = new CanTxMessageCollection();
                                newControl.Menu.Items.AddRange(new ToolStripItem[]
                                {
                                    new NewRowCommand(newControl.BaseDataGridView, (CanTxMessageCollection)tableItem.TableObject ),
                                    new CopyRowsCommand(newControl.BaseDataGridView, (CanTxMessageCollection)tableItem.TableObject),
                                    new CutRowsCommand(newControl.BaseDataGridView, (CanTxMessageCollection)tableItem.TableObject),
                                    new PasteRowsCommand(newControl.BaseDataGridView, (CanTxMessageCollection)tableItem.TableObject),
                                    new DeleteCommand(newControl.BaseDataGridView, (CanTxMessageCollection)tableItem.TableObject),
                                    new ExportCommand(_storage, newControl),
                                });
                            newControl.Source = (CanTxMessageCollection) tableItem.TableObject;
                            newControl.CustomArbIdColumns = _customArbIdColumns;

                            var page = new MultiPageItem(tableItem.Name, newControl,"canbus24");
                            page.Tag = (CanTxMessageCollection)tableItem.TableObject;
                            _pagesView.Add(page);

                            break;
                        }
                    }
                }
            }
            else if (e.ListChangedType == ListChangedType.ItemChanged)
            {
               
            }
        }
    }
}
