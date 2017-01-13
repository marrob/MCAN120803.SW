
namespace Konvolucio.MCAN120803.GUI.AppModules.Workspace.Model
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Xml;

    public interface IWorkspaceItem
    {
        string ItemFilePath { get; }
        string WorkingDirectory { get; }
        bool IsValid { get; }
        string Comment { get; }
        IWorkspaceCollection WorkspaceItems { get; }

        void OpenItem();
        void CloseItem();
    }

    public class WorkspaceItem : IWorkspaceItem
    {
        public string ItemFilePath { get { return itemFilePath; } }
        private readonly string itemFilePath;
   
        public string WorkingDirectory
        {
            get
            {
                return System.IO.Path.GetDirectoryName(ItemFilePath);
            }
        }
   
        public string FileName
        {
            get
            {
                return (System.IO.Path.GetFileNameWithoutExtension(ItemFilePath));
            }
        }
     
        public bool IsValid
        {
            get { return System.IO.File.Exists(ItemFilePath); }
        }
    
        public string Comment
        {
            get
            {
                return ReadComment(ItemFilePath);
            }
        }
    
        public IWorkspaceCollection WorkspaceItems { get { return workspaceItems; } }
        private readonly IWorkspaceCollection workspaceItems;

        public WorkspaceItem(IWorkspaceCollection items, string path) 
        {
            itemFilePath = path;
            workspaceItems = items;
        }

        public void OpenItem()
        {
            WorkspaceItems.OpenProject(this.ItemFilePath);
        }

        public void CloseItem()
        {
            WorkspaceItems.CloseProject(this.itemFilePath);
        }

        string ReadComment(string path)
        {
            string retval = string.Empty;
            FileStream fs = null;
            try
            {
                fs = new FileStream(path, FileMode.Open, FileAccess.Read);
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.Load(fs);
                if (xmldoc.GetElementsByTagName("Comment").Count != 0)
                    retval = xmldoc.GetElementsByTagName("Comment")[0].ChildNodes.Item(0).InnerText;
            }
            catch{ }
            finally
            {
                if (fs != null && fs.CanRead)
                {
                    try { fs.Close(); }  catch { }
                }
            }
            return retval;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
