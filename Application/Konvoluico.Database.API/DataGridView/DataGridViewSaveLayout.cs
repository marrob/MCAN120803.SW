
namespace Konvolucio.Database.API
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Forms;
    using System.Configuration;
    using System.ComponentModel;
    using Konvolucio.Database.API.Properties;

    public class DataGridViewSaveLayout : DataGridViewBackgorundText
    {
        [Category("Settings")]
        public string LayoutSettingsName
        {
            get { return this.Name + "_ColumnLayout"; }
        }

        private void SetLayout()
        {
            var settings = Settings.Default;
            if (settings.GetType().GetProperty(this.Name + "_ColumnLayout") != null)
            {
                var tempValue = settings.GetType().GetProperty(this.Name + "_ColumnLayout").GetValue(settings, null);
                if (tempValue != null)
                {
                    var layoutItems = tempValue as ColumnLayoutCollection;
                    var sorted = layoutItems.OrderBy(n => n.DisplayIndex);
                    foreach (ColumnLayoutItem item in sorted)
                    {
                        if (this.Columns.Contains(item.Name))
                        {
                            this.Columns[item.Name].DisplayIndex = item.DisplayIndex;
                            this.Columns[item.Name].Visible = item.Visible;
                            this.Columns[item.Name].Width = item.Width;
                            this.Columns[item.Name].Visible = item.Visible;
                        }
                    }
                }
            }
        }
        
        private void SaveLayout()
        {
            var settings = Settings.Default;
            if (settings.GetType().GetProperty(this.Name + "_ColumnLayout") != null)
            {
                var items = new ColumnLayoutCollection();
                foreach (DataGridViewColumn gridColumn in this.Columns)
                {
                    var columnLayout = new ColumnLayoutItem();
                    columnLayout.Name = gridColumn.Name;
                    columnLayout.DisplayIndex = gridColumn.DisplayIndex;
                    columnLayout.Visible = gridColumn.Visible;
                    columnLayout.Width = gridColumn.Width;
                    items.Add(columnLayout); 
                }
                settings.GetType().GetProperty(this.Name + "_ColumnLayout").SetValue(settings, items, null);
            }
        }       
        
        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            SetLayout();
        }
        
        protected override void Dispose(bool disposing)
        {
            SaveLayout();
            base.Dispose(disposing);
        }
        
        protected override void OnMouseUp(MouseEventArgs e)
        {
            DataGridView.HitTestInfo hitTestInfo;
            hitTestInfo = this.HitTest(e.X, e.Y);

            /*Ha fejlcécre kattint, akkor egy másik kontext menü jelenik meg.*/
            if (hitTestInfo.Type == DataGridViewHitTestType.ColumnHeader)
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    var contextMenu = new ContextMenuStrip();
                    foreach (DataGridViewColumn gridColumn in this.Columns)
                    {
                        if (gridColumn.HeaderText != "Index")
                        {
                            var button = new ToolStripMenuItem();
                            button.Text = gridColumn.HeaderText;
                            button.CheckOnClick = true;
                            button.CheckState = gridColumn.Visible ? CheckState.Checked : CheckState.Unchecked;
                            var column = gridColumn;
                            button.CheckStateChanged += (o, ev) =>
                            {
                                column.Visible = button.CheckState == CheckState.Checked ? true : false;
                            };
                            contextMenu.Items.Add(button);
                        }
                    }
                    contextMenu.Show(this, new System.Drawing.Point(e.X,e.Y));
                }
            }
            base.OnMouseUp(e);
        }
    }

    [Serializable]
    public class ColumnLayoutItem
    {
        public int DisplayIndex { get; set; }
        public int Width { get; set; }
        public bool Visible { get; set; }
        public string Name { get; set; }
        public bool AllowHide { get; set; }

        public ColumnLayoutItem() { }

        public override string ToString()
        {
            return Name + " ";
        }
    }
   
    public class ColumnLayoutCollection: List<ColumnLayoutItem>
    {
    }
}
