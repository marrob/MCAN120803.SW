// -----------------------------------------------------------------------
// <copyright file="FilterNameTreeNode.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Konvolucio.MCAN120803.GUI.AppModules.Filter.TreeNodes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms;
    using Model;
    using Services;
    using WinForms.Framework;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    internal sealed class FilterNameTreeNode : TreeNode
    {
        private readonly MessageFilterItem _item;
        public FilterNameTreeNode(MessageFilterItem item)
        {
            Tag = item;
            _item = item;

            if (_item.AcceptanceCount.HasValue)
                Text = _item.Name + @": " + _item.AcceptanceCount;
            else
                Text = _item.Name + @": " + AppConstants.ValueNotAvailable2;

            if(item.Enabled)
                SelectedImageKey = ImageKey = @"FilterFilled16";
            else
                SelectedImageKey = ImageKey = @"FilterClear16";

            item.PropertyChanged += (o, e) =>
            {
                if (e.PropertyName == PropertyPlus.GetPropertyName(() => item.Enabled))
                {
                    if (item.Enabled)
                        SelectedImageKey = ImageKey = @"FilterFilled16";
                    else
                        SelectedImageKey = ImageKey = @"FilterClear16";
                }
                else if (e.PropertyName == PropertyPlus.GetPropertyName(() => item.Name))
                {
                    if (_item.AcceptanceCount.HasValue)
                        Text = item.Name + @": " + item.AcceptanceCount;
                    else
                        Text = item.Name + @": " + AppConstants.ValueNotAvailable2;
                }

            };

            TimerService.Instance.Tick += new EventHandler(Timer_Tick);
            item.DefaultStateComplete += (o, e) => Timer_Tick(null, EventArgs.Empty);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (_item.AcceptanceCount.HasValue)
                Text = _item.Name + @": " + _item.AcceptanceCount;
            else
                Text = _item.Name + @": " + AppConstants.ValueNotAvailable2;
        }
    }
}
