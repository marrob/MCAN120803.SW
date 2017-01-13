
namespace Konvolucio.WinForms.Framework
{
    using System;
    using System.ComponentModel;
    using System.Windows.Forms;
    using System.Drawing;

    public class KnvSplitContainer : SplitContainer
    {
        public KnvSplitContainer()
        {
            BackColor = Color.Orange;
            Panel1.BackColor = System.Drawing.SystemColors.Control;
            Panel2.BackColor = System.Drawing.SystemColors.Control;
        }

        public double SplitterPrecent
        {
            get
            {
                if (Orientation == System.Windows.Forms.Orientation.Vertical)
                    return Math.Round(this.SplitterDistance / (double)this.Width, 2);
                else
                    return Math.Round(this.SplitterDistance / (double)this.Height, 2);
            }
            set
            {
                if (Orientation == System.Windows.Forms.Orientation.Vertical)
                    this.SplitterDistance = (int)((double)(value) * this.Width);
                else
                    this.SplitterDistance = (int)((double)(value) * this.Height);
            }
        }
    }
}
