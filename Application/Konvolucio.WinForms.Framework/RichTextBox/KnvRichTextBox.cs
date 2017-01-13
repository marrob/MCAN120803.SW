
namespace Konvolucio.WinForms.Framework
{
    using System;
    using System.Text;
    using System.Windows.Forms;
    using System.Drawing;
    using System.ComponentModel;
    using System.Runtime.InteropServices;

    public class KnvRichTextBox: RichTextBox
    {

        #region Constructor
        public KnvRichTextBox()
        {
            BorderStyle = System.Windows.Forms.BorderStyle.None;
        }
        #endregion

        #region Backgrount Text
        [Category("KNV")]
        public string BackgroundText
        {
            get { return _backgroundText; }
            set
            {
                _backgroundText = value;
                Refresh();
            }
        }
        string _backgroundText = "Backgorund Text";

        /// <summary>
        /// Amikor át méretez újra kell rajzolni a TextBackground-ot
        /// </summary>
        /// <param name="e"></param>
        protected override void OnResize(EventArgs e)
        {
            this.Refresh();
            base.OnResize(e);
        }

        /// <summary>
        /// TextBackground változik újra kell rajzolni sszöveg
        /// </summary>
        /// <param name="e"></param>
        protected override void OnTextChanged(EventArgs e)
        {
            this.Refresh();
            base.OnTextChanged(e);
        }

        /// <summary>
        /// Rajzolja a TextBackground-ot
        /// </summary>
        /// <param name="e"></param>
        private void PaintTextBackground(PaintEventArgs e)
        {
            var rch = this;

            float width = rch.Bounds.Width;
            float height = rch.Bounds.Height;

            string backgroundText = BackgroundText;
            int backgroundTextSize = 25;

            if (string.IsNullOrEmpty(rch.Text))
            {
                Color clear = Color.Red;
                if (backgroundTextSize == 0) backgroundTextSize = 10;
                Font f = new Font("Seqoe", 20, FontStyle.Bold);
                Brush b = new SolidBrush(Color.Orange);
                SizeF strSize = e.Graphics.MeasureString(backgroundText, f);
                e.Graphics.DrawString(backgroundText, f, b, (width / 2) - (strSize.Width / 2), (height / 2) - strSize.Height / 2);
            }
        }
        #endregion

        #region Border Line
        /// <summary>
        /// Amikor szkrolloz, akkor a BorderLine-t újra kell rajzolni mert rajta marad a lapon
        /// </summary>
        /// <param name="e"></param>
        protected override void OnVScroll(EventArgs e)
        {
            this.Refresh();
            base.OnVScroll(e);
        }

        /// <summary>
        /// Amikor szkrolloz, akkor a BorderLine-t újra kell rajzolni mert rajta marad a lapon 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnHScroll(EventArgs e)
        {
            this.Refresh();
            base.OnHScroll(e);
        }

        /// <summary>
        /// Border Line-t rajzolja
        /// </summary>
        /// <param name="e"></param>
        private void PaintBorderLine(PaintEventArgs e)
        {
            if (BorderStyle == System.Windows.Forms.BorderStyle.None)
            {
                const int BORDER_SIZE = 1;
                ControlPaint.DrawBorder(e.Graphics, ClientRectangle,
                              Color.Black, BORDER_SIZE, ButtonBorderStyle.Solid,
                              Color.Black, BORDER_SIZE, ButtonBorderStyle.Solid,
                              Color.Black, BORDER_SIZE, ButtonBorderStyle.Solid,
                              Color.Black, BORDER_SIZE, ButtonBorderStyle.Solid);
            }
        }
        #endregion 

        #region Common
        [Category("KNV")]
        [DefaultValueAttribute(false)]
        [DisplayName("InhibitPaint")]
        [Description("Kikapcsolja a Border rajzolását")]
        public bool InhibitPaint
        {
            set { _inhibitPaint = value; }
        }
        private bool _inhibitPaint = false;

        /// <summary>
        /// Kényszeríti az OnPaint esemény meghívásást.
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            const int WM_PAINT = 15;

            base.WndProc(ref m);

            if (m.Msg == WM_PAINT && !_inhibitPaint)
            {
                // raise the paint event
                using (Graphics graphic = base.CreateGraphics())
                {
                    OnPaint(new PaintEventArgs(graphic, base.ClientRectangle));
                }
            }
        }

        /// <summary>
        /// Rajzoló esmény
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            PaintBorderLine(e);
            PaintTextBackground(e);
        }
        #endregion 
    }
}
