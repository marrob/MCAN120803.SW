
namespace Konvolucio.Database.API
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class DataGridViewBackgorundText : DataGridView
    {
        string backgroundText;
        [Category("BackgorundText")]
        [DefaultValueAttribute("BackgorundText")]
        public string BackgroundText
        {
            get { return backgroundText; }
            set
            {
                backgroundText = value;
                Refresh();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var dgv = this;
            float width = dgv.Bounds.Width;
            float height = dgv.Bounds.Height;

            int backgroundTextSize = 25;

            base.OnPaint(e);

            if ((dgv.Rows == null || dgv.Rows.Count == 0))
            {
                Color clear = dgv.BackgroundColor;
                if (backgroundTextSize == 0) backgroundTextSize = 10;
                Font f = new Font("Seqoe", 20, FontStyle.Bold);
                Brush b = new SolidBrush(Color.Orange);
                SizeF strSize = e.Graphics.MeasureString(backgroundText, f);
                e.Graphics.DrawString(backgroundText, f, b, (width / 2) - (strSize.Width / 2), (height / 2) - strSize.Height / 2);
            }

         
        }
    }
}
