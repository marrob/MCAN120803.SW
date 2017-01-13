// -----------------------------------------------------------------------
// <copyright file="TraceNavigatorSelectionChangedEventApp.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Konvolucio.MCAN120803.GUI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms; /*TreeNode*/

    using WinForms.Framework;

    /// <summary>
    /// Minden TreeView ezen eseményen kereszül jelzi, hogy változott a kijelölés!
    /// Az alakalmazásra Globális.
    /// </summary>
    public class TreeViewSelectionChangedAppEvent : IApplicationEvent
    {
        public TreeNode SelectedNode { get; private set; }

        /// <summary>
        /// TODO: Update summary.
        /// </summary>
        public class SelectionSourceType
        {
        }

        public TreeViewSelectionChangedAppEvent(TreeNode selectedNode)
        {
            SelectedNode = selectedNode;
        }
        /// <summary>
        /// Debug-hoz
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (SelectedNode != null)
            {
                if (SelectedNode is TreeNode)
                {
                    return this.GetType().Name + ", " + "NodeType:" + SelectedNode.GetType().Name + " SelectedItem.Text:" + (SelectedNode as TreeNode).Text;
                }
                else
                {
                    return this.GetType().Name + ", " + "NodeType:" + SelectedNode.GetType().Name;
                }
            }
            else
            {
                return this.GetType().Name + ", " + "NodeType: null";
            }
        }
    }
}
