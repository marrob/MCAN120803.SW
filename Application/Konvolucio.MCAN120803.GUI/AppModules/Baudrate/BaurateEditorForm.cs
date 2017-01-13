
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Konvolucio.MCAN120803.GUI.Services;

namespace Konvolucio.MCAN120803.GUI.AppModules.Baudrate
{
    using View;
    using Model;

    public interface IBaurateEditorForm
    {
        DialogResult ShowDialog();
        /// <summary>
        /// Itt adhtsz neki kezőértéket is.
        /// </summary>
        string CustomBaudRateValue { get; set; }

        /// <summary>
        /// Alapértlemezett értékek beállítása
        /// </summary>
        void Default();
    }

    public partial class BaurateEditorForm : Form, IBaurateEditorForm
    {
        public string CustomBaudRateValue 
        { 
            get
            {
                return view.CustomBaudrateValue;
            }

            set
            {
                view.CustomBaudrateValue = value;
            }
        }

        readonly BaudrateCalculator cbrc;
        ICustomBaudrateView view { get { return customBaudrateView1; } }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public BaurateEditorForm()
        {
            InitializeComponent();
            cbrc = new BaudrateCalculator(CultureService.Instance.TextResource);
        }

        /// <summary>
        /// Változott a Brp, Tseg1, Tseg2, Sjw
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void customBaudrateView1_ParameterChanged(object sender, EventArgs e)
        {
            cbrc.Calculate(view.Brp, view.Tseg1, view.Tseg2, view.Sjw);

            view.SmaplePoint = cbrc.SamplePoint;
            view.Baudrate = cbrc.Baudrate;
            view.CalculateDetails = cbrc.GetCalculateDetails;

            view.BaudratAbsError = cbrc.GetBaudrateAbsoluteError(view.ExpectedBaudrate);
            view.BaudrateRelError = cbrc.GetBaudrateRealtiveError(view.ExpectedBaudrate);

            view.CustomBaudrateValue = cbrc.GetCustomBaudRateValue().ToString();
        }
        /// <summary>
        /// Változott a várt BaudRate
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void customBaudrateView1_ExpectedBaudRateChanged(object sender, EventArgs e)
        {
             view.BaudratAbsError = cbrc.GetBaudrateAbsoluteError(view.ExpectedBaudrate);
             view.BaudrateRelError = cbrc.GetBaudrateRealtiveError(view.ExpectedBaudrate);
        }
        /// <summary>
        /// Változott a a CustomBaudrat Value
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void customBaudrateView1_CustomBaudrateChanged(object sender, EventArgs e)
        {
            /*parméterket megjelnítjem, ha ere is jön változás akkor elrontja...*/
            view.ParameterChanged -= customBaudrateView1_ParameterChanged;
            try
            {
                cbrc.Calculate(view.CustomBaudrateValue);
                view.Brp = cbrc.Segments.Brp;
                view.Tseg1 = cbrc.Segments.Tseg1;
                view.Tseg2 = cbrc.Segments.Tseg2;
                view.Sjw = cbrc.Segments.Sjw;

                view.SmaplePoint = cbrc.SamplePoint;
                view.Baudrate = cbrc.Baudrate;
                view.CalculateDetails = cbrc.GetCalculateDetails;

                view.BaudratAbsError = cbrc.GetBaudrateAbsoluteError(view.ExpectedBaudrate);
                view.BaudrateRelError = cbrc.GetBaudrateRealtiveError(view.ExpectedBaudrate);

                buttonOK.Enabled = true;
            }
            catch (Exception)
            {
                buttonOK.Enabled = false;
            }
            finally
            {
                view.ParameterChanged += customBaudrateView1_ParameterChanged;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Default()
        {
            view.CustomBaudrateValue = "B003F807";
            view.ExpectedBaudrate = 250000;
        }
    }
}
