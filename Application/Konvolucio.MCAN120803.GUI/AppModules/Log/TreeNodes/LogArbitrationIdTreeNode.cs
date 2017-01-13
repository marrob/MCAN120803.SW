// -----------------------------------------------------------------------
// <copyright file="LogArbitrationIdTreeView.cs" company="">
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
    using Services;
    using Converters;


    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class LogArbitrationIdTreeNode : TreeNode
    {
        public LogArbitrationIdTreeNode(uint arbitrationId)
        {
            /*Arbitration Id: [{0}]*/
            Text = string.Format(CultureService.Instance.GetString(CultureText.node_ArbitrationId_Text), new ArbitrationIdConverter().ConvertTo(arbitrationId, typeof(string)));
            SelectedImageKey = ImageKey = "id16";
        }
    }
}
