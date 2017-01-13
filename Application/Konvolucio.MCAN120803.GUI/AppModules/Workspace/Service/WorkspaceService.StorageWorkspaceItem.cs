
namespace Konvolucio.MCAN120803.GUI.AppModules.Workspace.Service
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    
    public partial class WorkspaceService
    {

        public class StorageWorkspaceItem
        {
            public string ProjectPath { get; set; }

            public StorageWorkspaceItem()
            {
                ProjectPath = string.Empty;
            }
            
            public StorageWorkspaceItem(string path)
            {
                ProjectPath = path;
            }
        }
    }
}
