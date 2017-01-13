

namespace Konvolucio.WinForms.Framework.MultiPage.UnitTest.View
{
    using System.Windows.Forms;

    partial class DemoPage : UserControl 
    {
        public string TextX {
            get { return label2.Text; }
            set { label2.Text = value; }
        }

        public DemoPage()
        {
            InitializeComponent();
        }
    }
}
