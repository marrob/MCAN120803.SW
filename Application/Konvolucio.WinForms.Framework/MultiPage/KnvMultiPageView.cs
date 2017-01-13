
namespace Konvolucio.WinForms.Framework
{
    using System;
    using System.ComponentModel;
    using System.Windows.Forms;

    public partial class KnvMultiPageView : UserControl
    {

        [Category("KNV")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public MultiPageCollection Pages
        {
            get { return _pages; }
        }
        private readonly MultiPageCollection _pages;

        [Category("KNV")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public MultiPageItem CurrentPage
        {
            get { return _pages.CurrentPage; }
            set
            {
                if (_pages != null)
                    _pages.CurrentPage = value;
            }
        }

        [Category("KNV")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Control CurrentControl
        {
            get
            {
                if (panel1.Controls.Count > 0)
                    return panel1.Controls[0];
                return null;
            }
        }

        [Category("KNV")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        internal KnvMultiPageStripView StripView
        {
            get { return this.mulitPageStripView1; }
        }

        [Category("KNV")]
        public string BackgroundText
        {
            get { return label1.Text; }
            set
            {
                label1.Text = value;
            }
        }

        public KnvMultiPageView()
        {
            InitializeComponent();
            _pages = new MultiPageCollection();
            SourceChanged(_pages);
            this.Disposed += new EventHandler(KnvMultiPageView_Disposed);
        }

        private void KnvMultiPageView_Disposed(object sender, EventArgs e)
        {
           Pages.Dispose();
        }

        private void SourceChanged(MultiPageCollection value)
        {
            mulitPageStripView1.Pages = Pages;
            if (_pages != null)
            {         
                RemovePages();
                _pages.CurrentPageChanged += new EventHandler(Pages_CurrentPageChanged);
                _pages.ListChanged += new ListChangedEventHandler(Page_ListChanged);
            }
        }

        private void Pages_CurrentPageChanged(object sender, EventArgs e)
        {
            var multiPageCollection = sender as MultiPageCollection;
            if (multiPageCollection != null)
            {
                if (multiPageCollection.CurrentPage.PageControl != CurrentControl)
                {
                    if (!panel1.Controls.Contains(multiPageCollection.CurrentPage.PageControl))
                    {
                        panel1.Controls.Clear();
                        panel1.Controls.Add(multiPageCollection.CurrentPage.PageControl);
                    }
                }
            }
        }

        private void Page_ListChanged(object sender, ListChangedEventArgs e)
        {
            switch (e.ListChangedType)
            {
                case ListChangedType.Reset:
                {
                    RemovePages();
                    break;
                }
            }
        }

        private void RemovePages()
        {
            panel1.Controls.Clear();
        }
    }
}
