
namespace Konvolucio.MCAN120803.GUI.AppModules.Workspace.Service
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.ComponentModel;
    using Model;

    public interface IWorkspaceService
    {
        event ProgressChangedEventHandler ProgressChange;
        IWorkspaceCollection WorkspaceItems { get; }
        string LastOpenedProjectPath { get; set; }

        void Save();
        void Load(string path);
    }

    public partial class WorkspaceService : IWorkspaceService
    {
        public const string XmlNamespace = @"http://www.konvolucio.hu/mcanx/2016/workspace/content";
        public const string XmlRootElement = "Workspace";

        public event ProgressChangedEventHandler ProgressChange;

        public string LastOpenedProjectPath
        {
            get { return storage.LastUsedFilePath; }
            set { storage.LastUsedFilePath = value; }
        }

        //public static IWorkspaceService Instance { get { return instance; } }
        //private readonly static IWorkspaceService instance = new WorkspaceService();

        public IWorkspaceCollection WorkspaceItems { get { return workspaceItems; } }
        private readonly IWorkspaceCollection workspaceItems;

        private readonly IStorageWorkaspace storage;

        string filePath;

        public WorkspaceService()
        {
            workspaceItems = new WorkspaceCollection();
            storage = new StorageWorkaspace();
            filePath = string.Empty;
        }

        public void Load(string path)
        {
            filePath = path;
            if (!System.IO.File.Exists(path))
            {
                Save();
            }
            else
            {
                storage.LoadFromFile(path);
                foreach (var item in storage.WorkspaceItems)
                {
                    var viewItem = new WorkspaceItem(workspaceItems, item.ProjectPath);
                    (workspaceItems as WorkspaceCollection).Add(viewItem);
                }
            }
        }

        public void Save()
        {
            storage.WorkspaceItems.Clear();
            foreach (var item in workspaceItems as WorkspaceCollection)
                storage.WorkspaceItems.Add(new StorageWorkspaceItem(item.ItemFilePath));
            storage.SaveToFile(filePath);
        }

        protected void OnProgressChanged(ProgressChangedEventArgs e)
        {
            if (ProgressChange != null)
                ProgressChange(this, e);
        }
    }
}
