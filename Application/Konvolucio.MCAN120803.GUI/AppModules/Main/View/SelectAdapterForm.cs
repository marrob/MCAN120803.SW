
namespace Konvolucio.MCAN120803.GUI.AppModules.Main.View
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;
    using Adapters;
    using Properties;

    public interface IAdapterSelectForm
    {
        string SelectedAdapter { get; set; }
        DialogResult ShowDialog();
    }

    public partial class SelectAdapterForm : Form, IAdapterSelectForm
    {
        public string SelectedAdapter { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public SelectAdapterForm()
        {
            InitializeComponent();
            var images = new ImageList();
            images.Images.Add("adapter", Resources.adapter128);
            images.Images.Add("virtual_adapter", Resources.virtual_adapter128);
            images.ImageSize = new Size(64, 64);
            listView1.LargeImageList = images;
            listView1.View = View.Tile;
            AdaptersListRefresh();
        }

        /// <summary>
        /// 
        /// </summary>
        private void AdaptersListRefresh()
        {
            var adapters = AdapterService.GetAdapters();
            listView1.Items.Clear();
            foreach (var name in adapters)
            {
                ListViewItem item = new ListViewItem();

                if (name.Contains("Virtual"))
                {
                    item.ImageKey = @"virtual_adapter";
                    item.Text = name;
                    listView1.Items.Add(item);
                }
                else
                {
                    item.ImageKey = @"adapter";
                    item.Text = @"Serial Number: " + name;
                    listView1.Items.Add(item);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 
        /// </summary>
        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            AdaptersListRefresh();
        }

        /// <summary>
        /// 
        /// </summary>
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count != 0)
            {
                SelectedAdapter = AdapterService.GetAdapters()[listView1.SelectedItems[0].Index];
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void AdapterSelectorDialog_Shown(object sender, EventArgs e)
        {
            if (listView1.Items.Count > 0)
                listView1.Items[0].Selected = true;
        }
    }
}
