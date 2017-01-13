// -----------------------------------------------------------------------
// <copyright file="AddMessageFilter.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Konvolucio.MCAN120803.GUI.AppModules.Filter.Commands
{
    using System;
    using System.Windows.Forms; /*ToolStripMenuItem*/
    using Model;
    using Properties;
    using Services;
    using TreeNodes;

    internal sealed class DefaultNodeCommand : ToolStripMenuItem
    {
        private readonly MessageFilterCollection _collection;
        private readonly object _lockObj;

        public DefaultNodeCommand(MessageFilterCollection collection)
        {
            Image = Resources.Synchronize_16x16;
            Text = CultureService.Instance.GetString(CultureText.menuItem_Default_Text);
            _collection = collection;
            _lockObj = new object();

            EventAggregator.Instance.Subscribe<TreeViewSelectionChangedAppEvent>(e =>
            {
                Visible = (e.SelectedNode is FiltersTreeNode);
            });
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);

            if (Enabled)
            {
                lock (_lockObj)
                {
                    try
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        _collection.Default();
                    }
                    finally
                    {
                        Cursor.Current = Cursors.Default;
                    }
                }
            }
        }
    }
}
