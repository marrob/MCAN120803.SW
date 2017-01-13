

namespace Konvolucio.MCAN120803.GUI.AppModules.Workspace.Service
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using System.IO;

    using Model;

    public partial class WorkspaceService
    {   
        /****************************************************************/
        public interface IStorageWorkaspace
        {
            string LastUsedFilePath { get; set; }
            StorageWorkspaceCollection WorkspaceItems { get; }
            void SaveToFile(string path);
            void LoadFromFile(string path);
        }
        /****************************************************************/
        public class StorageWorkaspace : IStorageWorkaspace
        {
            public string LastUsedFilePath { get; set; }
            public StorageWorkspaceCollection WorkspaceItems { get; set; }

            /****************************************************************/
           public static StorageWorkspaceCollection Test_001()
           {
               var items = new StorageWorkspaceCollection();

               items.Add(new StorageWorkspaceItem(@"C:\Users\Margit Róbert\Documents\Konvolucio\MCAN120803\Egyes\Egyes.canx"));
               items.Add(new StorageWorkspaceItem(@"C:\Users\Margit Róbert\Documents\Konvolucio\MCAN120803\Hármas\Hármas.canx"));
               items.Add(new StorageWorkspaceItem(@"C:\Users\Margit Róbert\Documents\Konvolucio\MCAN120803\Kettes\Kettes.canx"));
               return items;
           }

            /****************************************************************/
            public StorageWorkaspace()
            {
                LastUsedFilePath = string.Empty;
                WorkspaceItems = new StorageWorkspaceCollection();
            }
            [XmlIgnore]
            public Type[] SupportedTypes
            {
                get
                {
                    return new Type[]
                    {
                        typeof(StorageWorkspaceItem),
                        typeof(StorageWorkspaceCollection)
                    };
                }
            }
            /****************************************************************/
            public void LoadFromFile(string path)
            {
                var xmlFormat = new XmlSerializer(typeof(StorageWorkaspace), null, SupportedTypes, new XmlRootAttribute(XmlRootElement), XmlNamespace);
                StorageWorkaspace instance;
                using (Stream fStream = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    instance = (StorageWorkaspace)xmlFormat.Deserialize(fStream);
                }
                LastUsedFilePath = instance.LastUsedFilePath;
                WorkspaceItems = instance.WorkspaceItems;
            }             
            /****************************************************************/
             public void SaveToFile(string path)
             {
                 var xmlFormat = new XmlSerializer(typeof(StorageWorkaspace), null, SupportedTypes, new XmlRootAttribute(XmlRootElement), XmlNamespace);
                using (Stream fStream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    xmlFormat.Serialize(fStream, this);
                }
             }
        }
    }
}
