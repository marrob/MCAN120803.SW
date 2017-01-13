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
    using Services; /*Culture*/

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class LogTransmittedTreeNode : TreeNode
    {
        public LogTransmittedTreeNode(ILogFileItem file)
        {
            Text = CultureService.Instance.GetString(CultureText.node_Transmitted_Text) + ": "; 
            Text += file.Statistics.TransmittedMessageCount.ToString();
            SelectedImageKey = ImageKey = "arrow_up";
            file.Statistics.PropertyChanged += (o, e) =>
                {
                    if (e.PropertyName == "TransmittedMessageCount")
                    {
                        Text = CultureService.Instance.GetString(CultureText.node_Transmitted_Text) + ": ";
                        Text += file.Statistics.TransmittedMessageCount.ToString();
                    }
                };
        }
    }
}
