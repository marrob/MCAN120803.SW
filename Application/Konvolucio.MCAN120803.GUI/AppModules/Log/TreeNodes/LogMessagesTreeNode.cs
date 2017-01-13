// -----------------------------------------------------------------------
// <copyright file="LogMessagesTreeView.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Konvolucio.MCAN120803.GUI.AppModules.Log.TreeNodes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms; /*TreeNode*/

    using WinForms.Framework;
    using Model;
    using Services; /*Culture*/
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class LogMessagesTreeNode : TreeNode
    {

        public LogMessagesTreeNode(ILogFileItem logFile)
        {
            Text = CultureService.Instance.GetString(CultureText.node_MessagesStatistics_Text);
            Name = "messsage1";
            SelectedImageKey = ImageKey = "mails16";

            var arbIds = logFile.Statistics.GetArbitationIds();

                   
            for (int i = 0; i < arbIds.Count; i++)
            {
                Nodes.Add( new LogMessageNameTreeNode(logFile, arbIds[i],
                                                             new LogArbitrationIdTreeNode(arbIds[i])));
            }   
        }
    }
}
