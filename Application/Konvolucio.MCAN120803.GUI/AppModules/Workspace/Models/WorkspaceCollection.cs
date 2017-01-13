
namespace Konvolucio.MCAN120803.GUI.AppModules.Workspace.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.ComponentModel;

    public interface IWorkspaceCollection /* : IListChanging<WorkspaceItem> */
    {
        event EventHandler ProjectCloseing;
        event EventHandler ItemAdded;
        event EventHandler OpenProjectChanged;
        IWorkspaceItem OpenedItem { get; }
        void OpenProject(string path);
        void CloseProject(string path);
        void AddProject(string path);
    }

    public class WorkspaceCollection : BindingList<WorkspaceItem>, IList<WorkspaceItem>, IWorkspaceCollection
    {
        public event EventHandler OpenProjectChanged;
        public event EventHandler ProjectCloseing;
        public event EventHandler ItemAdded;

        public IWorkspaceItem OpenedItem { get { return openedItem; } }
        IWorkspaceItem openedItem;

        //public event ListChangingEventHandler<WorkspaceItem> ListChanging;

        public WorkspaceCollection()
        {
            openedItem = null;
        }

        public void OpenProject(string path)
        {
            var item = this.FirstOrDefault(n => n.ItemFilePath == path);
            if (item == null)
            {
                var newItem = new WorkspaceItem(this, path);
                this.Add(newItem);
                openedItem = newItem;
                OnItemAdded(newItem);
            }
            else
            {
                openedItem = item;
            }
            OnOpenedItemChanged();
        }

        public void CloseProject(string path)
        {
            var item = this.FirstOrDefault(n => n.ItemFilePath == path);
            if (item != null)
            {
                OnItemCloseing(item);
                this.Remove(item);
            }
        }

        protected virtual void OnOpenedItemChanged()
        {
            if (OpenProjectChanged != null)
                OpenProjectChanged(this, EventArgs.Empty);
        }

        protected virtual void OnItemCloseing(object item)
        {
            if (ProjectCloseing != null)
                ProjectCloseing(item, EventArgs.Empty);
        }

        protected virtual void OnItemAdded(object item)
        {
            if (ItemAdded != null)
                ItemAdded(item, EventArgs.Empty);
        }

        public void AddProject(string path)
        {
            var item = this.FirstOrDefault(n => n.ItemFilePath == path);
            if (item == null)
            {
                var newItem = new WorkspaceItem(this, path);
                this.Add(newItem);
                OnItemAdded(newItem);
            }
        }
    }
}
