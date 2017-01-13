// -----------------------------------------------------------------------
// <copyright file="AddMessageFilter.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Konvolucio.MCAN120803.GUI.AppModules.Filter.Commands
{
    using System;
    using System.Windows.Forms;
    using Properties;
    using Services;
    using TreeNodes;
    using DataStorage;

    /// <summary>
    /// Parancs ami a Filter állapotát modosítja és jelzi az állapotát.
    /// Frissül:
    /// 1. Alaphelyzet ha a változott az az érték
    /// 2. Akkor látható ha megfelelő node-ra kattintott.
    /// 3. Állapot változtott
    /// </summary>
    internal sealed class EnabledNodeCommand : ToolStripMenuItem
    {
        Storage _prj;

        public EnabledNodeCommand()
        {
            EventAggregator.Instance.Subscribe<TreeViewSelectionChangedAppEvent>(e =>
            {
                Visible = (e.SelectedNode is FiltersTreeNode);
            });

            EventAggregator.Instance.Subscribe<StorageAppEvent>(e =>
            {
                _prj = e.Storage;

                if (e.ChangingType == FileChangingType.ContentChanged)
                {
                    if (e.Storage.Parameters.FiltersEnabled)
                    {
                        Text = CultureService.Instance.GetString(CultureText.menuItem_Disable_Text);
                        Image = Resources.switchon16;
                    }
                    else
                    {
                        Text = CultureService.Instance.GetString(CultureText.menuItem_Enable_Text);
                        Image = Resources.switchoff16;
                    }

                }
            });
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);

            if (Enabled)
            {
                _prj.Parameters.FiltersEnabled = !_prj.Parameters.FiltersEnabled;

                if (_prj.Parameters.FiltersEnabled)
                {
                    Text = CultureService.Instance.GetString(CultureText.menuItem_Disable_Text);
                    Image = Resources.switchon16;
                }
                else
                {
                    Text = CultureService.Instance.GetString(CultureText.menuItem_Enable_Text);
                    Image = Resources.switchoff16;
                }
            }
        }
    }
}
