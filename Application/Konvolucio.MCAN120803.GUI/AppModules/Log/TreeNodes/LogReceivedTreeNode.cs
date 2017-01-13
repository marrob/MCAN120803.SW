// -----------------------------------------------------------------------
// <copyright file="LogReceivedTreeViewItem.cs" company="">
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
    using View;
    using Services; /*Culture*/
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class LogReceivedTreeNode : TreeNode
    {
        public LogReceivedTreeNode(ILogFileItem logFile)
        {
            Text = CultureService.Instance.GetString(CultureText.node_Received_Text) + ": ";
            Text  += logFile.Statistics.ReceivedMessageCount.ToString();
            SelectedImageKey = ImageKey = "arrow_down";
            logFile.Statistics.PropertyChanged += (o, e) =>
                {
                    Text = CultureService.Instance.GetString(CultureText.node_Received_Text) + ": ";
                    Text += logFile.Statistics.ReceivedMessageCount.ToString();
                };

        }
    }
}
