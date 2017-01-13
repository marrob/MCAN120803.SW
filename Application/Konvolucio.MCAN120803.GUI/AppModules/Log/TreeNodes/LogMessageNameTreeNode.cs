// -----------------------------------------------------------------------
// <copyright file="LogArbitrationIdTreeView.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Konvolucio.MCAN120803.GUI.AppModules.Log.TreeNodes
{
    using System.Windows.Forms; /*TreeNode*/
    

    using Model;
    using Services; /*Culture*/
    using Converters;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class LogMessageNameTreeNode : TreeNode
    {
        public LogMessageNameTreeNode(ILogFileItem logFile, uint arbitrationId, TreeNode subNode)
        {
            var messageName = logFile.Messages.GetMessageNameByArbId(arbitrationId);
            var arbid = new ArbitrationIdConverter().ConvertTo(arbitrationId, typeof(string)) as string;
            if (string.IsNullOrEmpty(messageName))
            {
                /*Message: No Name [{0}]*/
                Text = string.Format(CultureService.Instance.GetString(CultureText.node_MessageNoNameArbId_Text), arbid);
            }
            else
            {
                /*Message: {0} [{1}]*/
                Text = string.Format(CultureService.Instance.GetString(CultureText.node_MessageNameArbId_Text), messageName, arbid);
            }

            SelectedImageKey = ImageKey = "Mail_16x16";
            Nodes.Add(subNode);
        }
    }
}
