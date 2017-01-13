
namespace Konvolucio.MCAN120803.GUI.AppModules.Filter.TreeNodes
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Linq;
    using System.Windows.Forms; /*TreeNode*/

    using WinForms.Framework;
    using Services; /*Culture*/
    using Model;

    internal sealed class FiltersTreeNode : TreeNode
    {
        private readonly MessageFilterCollection _collection;

        public FiltersTreeNode(MessageFilterCollection collection)
        {
            Name = "filtersTreeViewItem1";
            SelectedImageKey = ImageKey = @"Filter16";
            Text = CultureService.Instance.GetString(CultureText.node_Filters_Text);
            _collection = collection;
            collection.ListChanged += new ListChangedEventHandler(Filters_ListChanged);
            collection.ListChanging += new ListChangingEventHandler<MessageFilterItem>(Filters_ListChanging);

            EventAggregator.Instance.Subscribe<StorageAppEvent>(e =>
            {
                if (e.ChangingType == FileChangingType.ContentChanged)
                {
                    if (e.Storage.Parameters.FiltersEnabled)
                    {
                        Text = CultureService.Instance.GetString(CultureText.node_Filters_Text);
                        Text += @" [" + CultureService.Instance.GetString(CultureText.text_ENABLED) + @"]";
                    }
                    else
                    {
                        Text = CultureService.Instance.GetString(CultureText.node_Filters_Text);
                    }
                }
            });
        }

        private void Filters_ListChanging(object sender, ListChangingEventArgs<MessageFilterItem> e)
        {
            if (e.ListChangedType == ListChangingType.ItemRemoving)
            {
                var node = Nodes.Cast<TreeNode>().FirstOrDefault((n) => n.Tag == e.Item);
                if(node!= null)
                    Nodes.Remove(node);
            }
            if (e.ListChangedType == ListChangingType.Clearing)
            {
               Nodes.Clear();
            }
        }

        private void Filters_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (e.ListChangedType == ListChangedType.ItemAdded)
            {
                var item = _collection[e.NewIndex];
                var filterItem = new FilterNameTreeNode(item);
                Nodes.Add(filterItem);
            }
        }
    }
}
