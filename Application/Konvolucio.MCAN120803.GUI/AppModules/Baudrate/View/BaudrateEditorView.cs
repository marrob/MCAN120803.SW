

namespace Konvolucio.MCAN120803.GUI.AppModules.Baudrate.View
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms;

    public interface ICustomBaudrateView
    {
        event EventHandler ParameterChanged;
        event EventHandler ExpectedBaudRateChanged;
        event EventHandler CustomBaudRateChanged;
        int Brp { get; set; }
        int Tseg1 { get; set; }
        int Tseg2 { get; set; }
        int Sjw { get; set; }
        double SmaplePoint { set; }
        double Baudrate { set; }
        double ExpectedBaudrate { get; set; }
        double BaudratAbsError { set; }
        double BaudrateRelError { set; }
        List<string> CalculateDetails { set; }
        string CustomBaudrateValue { get; set; }
    }

    public partial class BaudrateEditorView : UserControl, ICustomBaudrateView
    {

        public event EventHandler ParameterChanged
        {
            add 
            {
                numericUpDownBrp.ValueChanged += value;
                numericUpDownTseg1.ValueChanged += value;
                numericUpDownTseg2.ValueChanged += value;
                numericUpDownSjw.ValueChanged += value;
            }
            remove
            {
                numericUpDownBrp.ValueChanged -= value;
                numericUpDownTseg1.ValueChanged -= value;
                numericUpDownTseg2.ValueChanged -= value;
                numericUpDownSjw.ValueChanged -= value;
            }
        }
        public event EventHandler ExpectedBaudRateChanged
        {
            add
            { 
                textBoxExpectedBaudRate.TextChanged += value;
            }
            remove
            {
                textBoxExpectedBaudRate.TextChanged -= value;
            }
        }

        public event EventHandler CustomBaudRateChanged
        {
            add { textBoxCustomBaudrateValue.TextChanged += value; }
            remove { textBoxCustomBaudrateValue.TextChanged -= value; }
        }

        public BaudrateEditorView()
        {
            InitializeComponent();
        }

        public int Brp
        {
            get
            {
               return (int)numericUpDownBrp.Value;
            }
            set
            {
                numericUpDownBrp.Value = value;
            }
        }

        public int Tseg1
        {
            get
            {
                return (int)numericUpDownTseg1.Value;
            }
            set
            {
                numericUpDownTseg1.Value = value;
            }
        }

        public int Tseg2
        {
            get
            {
                return (int)numericUpDownTseg2.Value;
            }
            set
            {
                numericUpDownTseg2.Value = value;
            }
        }

        public int Sjw
        {
            get
            {
                return (int)numericUpDownSjw.Value;
            }
            set
            {
                numericUpDownSjw.Value = value;
            }
        }

        public double SmaplePoint
        {
            set
            {
                textBoxSamplePoint.Text = value.ToString("N2")+ " %";
            }
        }

        public double Baudrate
        {
            set
            {
                textBoxBaudRate.Text = value.ToString("N2") + " Baud";
            }
        }

        public List<string> CalculateDetails
        {
            set
            {
                listBoxCalcDetails.Text = string.Empty;
                listBoxCalcDetails.Text = string.Join("\r\n", value.ToArray());
            }
        }

        public double ExpectedBaudrate
        {
            get
            {
                double retval = 0;

                if (double.TryParse(textBoxExpectedBaudRate.Text, out retval))
                {
                    return retval;
                }
                else
                {
                    return double.NaN;
                }
            }
            set
            {
                textBoxExpectedBaudRate.Text = value.ToString();
            }
        }

        public double BaudratAbsError
        {
            set 
            {
                if (value != double.NaN)
                    textBoxBaudRateAbsError.Text = value.ToString("N4");
                else
                    textBoxBaudRate.Text = double.NaN.ToString();
            }
        }

        public double BaudrateRelError
        {
            set
            {
                if (value != double.NaN)
                    textBoxBaudRateRelError.Text = value.ToString("N4") + " %";
                else
                    textBoxBaudRateRelError.Text = double.NaN.ToString();
            }
        }

        public string CustomBaudrateValue
        {
            get { return textBoxCustomBaudrateValue.Text; }
            set { textBoxCustomBaudrateValue.Text = value; }
        }

    }
}
