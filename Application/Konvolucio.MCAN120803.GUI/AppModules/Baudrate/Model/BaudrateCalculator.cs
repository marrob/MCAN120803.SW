// -----------------------------------------------------------------------
// <copyright file="CustomBaudrateCalculator.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Konvolucio.MCAN120803.GUI.AppModules.Baudrate.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Globalization; /*Culture Info*/

    using System.Resources; /*ResourceManger*/

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class BaudrateCalculator
    {
        public TimeSegemnts Segments { get; private set; }

        #region Calculated
        /// <summary>
        /// Time Quanta 
        /// [Tq](us)
        /// </summary>
        public double TimeQuanta { get; private set; }
        /// <summary>
        /// Total Number Of Time Quanta
        /// [tbit](Tq)
        /// </summary>
        public double TotalNumberOfTimeQuanta { get; private set; }

        /// <summary>
        /// Time Before Sample
        /// [tseg1](us)
        /// </summary>
        public double TimeBeforeSample { get; private set; }

        /// <summary>
        /// Time After Sample
        /// [tseg2](us)
        /// </summary>
        public double TimeAfterSample { get; private set; }

        /// <summary>
        /// Sync Jump Width
        /// [tjsw](us)
        /// </summary>
        public double TimeSyncJumpWidth { get; private set; }

        /// <summary>
        /// Nominal Bit Time
        /// [tnbit](us)
        /// </summary>
        public double NominalBitTime { get; private set; }

        /// <summary>
        /// Baudrate
        /// [BaudRate](Baud)
        /// </summary>
        public double Baudrate { get; private set; }

        /// <summary>
        /// Sample Point
        /// [sp](%)
        /// </summary>
        public double SamplePoint { get; private set; }

        #endregion 


        public List<string> GetCalculateDetails { get; private set; }

        ResourceManager _txtRes;

        /// <summary>
        /// Constructor
        /// </summary>
        public BaudrateCalculator():this(null)
        {
        }

        public BaudrateCalculator(ResourceManager textResource)
        {
            GetCalculateDetails = new List<string>();
            _txtRes = textResource;
           
        }

        /// <summary>
        /// STM32F1=> APB
        /// </summary>
        const double MCAN120803SystemClock = 42000000;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="brp">Baudrate Rate Prescaler [1..1024]</param>
        /// <param name="tseg1">Time Segment1 [1..16] </param>
        /// <param name="tseg2">Time Segment2 [1..8]</param>
        /// <param name="sjw">Sync Jump Width [1..4]</param>
        public void Calculate(int brp,  int tseg1, int tseg2, int sjw)
        {
            Segments = new TimeSegemnts(MCAN120803SystemClock, brp, tseg1, tseg2, sjw);
            Calculate(Segments);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="systemClock">System Clock [Hz]</param>
        /// <param name="brp">Baudrate Rate Prescaler [1..1024]</param>
        /// <param name="tseg1">Time Segment1 [1..16] </param>
        /// <param name="tseg2">Time Segment2 [1..8]</param>
        /// <param name="sjw">Sync Jump Width [1..4]</param>
        public void Calculate(double systemClock, int brp,  int tseg1, int tseg2, int sjw)
        {
            Segments = new TimeSegemnts(systemClock, brp, tseg1, tseg2, sjw);
            Calculate(Segments);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customBaudRateValue"></param>
        public void Calculate(string customBaudRateValue)
        {
            Segments = CustomBaudRateValueToSegemnts(customBaudRateValue);
            Calculate(Segments);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="segments"></param>
        public void Calculate(TimeSegemnts segments)
        {
            GetCalculateDetails.Clear();

            //_txtRes = new System.Resources.ResourceManager("Konvolucio.Project.Localization.TextResource", typeof(BaudrateCalculator).Assembly);

            if (_txtRes != null)
            {
                /*text_BaudrateSystemClock*/
                /*"System Clock[PCLK]: {0:N4} MHz"*/
                /*Rendszer órajel[PCLK]: {0:N4} MHz*/
                GetCalculateDetails.Add(string.Format(_txtRes.GetString("text_BaudrateSystemClock"), (segments.SystemClock / 1000000)));
                /*text_BaudRatePrescaler*/
                /*Baud Rate Prescaler[BRP]: {0}*/
                /*Átviteli sebesség osztó[BRP]: {0}*/
                GetCalculateDetails.Add(string.Format(_txtRes.GetString("text_BaudRatePrescaler"), segments.Brp));
            }
            else
            {
                GetCalculateDetails.Add("System Clock[PCLK]: " + (segments.SystemClock / 1000000).ToString("N4") + " MHz");
                GetCalculateDetails.Add("Baud Rate Prescaler[BRP]: " + segments.Brp.ToString() + "");
                GetCalculateDetails.Add("Time Segments Before Sample[TSEG1]: " + segments.Tseg1.ToString() + " Tq");
                GetCalculateDetails.Add("Time Segments After Sample[TSEG2]: " + segments.Tseg2.ToString() + " Tq");
                GetCalculateDetails.Add("Max Sync Jump Width[SJW]: " + segments.Sjw.ToString() + " Tq");            
            }
            
            TotalNumberOfTimeQuanta = segments.Tseg1 + segments.Tseg2;

            GetCalculateDetails.Add("Total Number Of Time Quanta[tbit]: " + TotalNumberOfTimeQuanta.ToString());

            TimeQuanta = (1 / segments.SystemClock) * segments.Brp * 1000000; /*sec -> usec*/
            GetCalculateDetails.Add("Time Quanta[Tq]: " + TimeQuanta.ToString("N5") + " us");

            TimeBeforeSample = segments.Tseg1 * TimeQuanta;
            GetCalculateDetails.Add("Time Before Sample[tseg1]: " + TimeBeforeSample.ToString("N5") + " us");

            TimeAfterSample = segments.Tseg2 * TimeQuanta;
            GetCalculateDetails.Add("Time After Sample[tseg2]: " + TimeAfterSample.ToString("N5") + " us");

            NominalBitTime = 1 * TimeQuanta + TimeBeforeSample + TimeAfterSample; /* Mindig bele kell számolni a SYNC-et! ami 1 Tq */
            GetCalculateDetails.Add("Nominal Bit Time: " + NominalBitTime.ToString("N5") + " us");

            Baudrate = 1 / (NominalBitTime / 1000000); /* osztva ezerrel a usec -> sec miatt*/
            GetCalculateDetails.Add("Baud Rate: " + Baudrate.ToString("N") + " Buad");

            SamplePoint = ((1.0 + segments.Tseg1) / (double)(segments.Tseg1 + segments.Tseg2 + 1.0)) * 100.0;
            GetCalculateDetails.Add("Sample Point: " + SamplePoint.ToString("N") + " %");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expectedBaudRate"></param>
        /// <returns></returns>
        public double GetBaudrateAbsoluteError(double expectedBaudRate)
        {
            if (expectedBaudRate != double.NaN)
            {
                return Baudrate - expectedBaudRate;
            }
            else
            { 
               return double.NaN; 
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expectedBaudRate"></param>
        /// <returns></returns>
        public double GetBaudrateRealtiveError(double expectedBaudRate)
        {
            if (expectedBaudRate != double.NaN)
            {
                return (Baudrate - expectedBaudRate) / expectedBaudRate * 100;
            }
            else
            {
                return double.NaN;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string GetCustomBaudRateValue()
        {
            uint temp = 0;
            temp |= (uint)0xB0 << 24; //Fix
            temp |= (uint)(Segments.Tseg1 - 1 & 0x0F) << 12;
            temp |= (uint)(Segments.Tseg2 - 1 & 0x07) << 16;
            temp |= (uint)(Segments.Sjw  - 1 & 0x03) << 10;
            temp |= (uint)(Segments.Brp - 1 & 0x3FF);
            return temp.ToString("X8");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customBaudRateValue"></param>
        /// <returns></returns>
        private TimeSegemnts CustomBaudRateValueToSegemnts(string customBaudRateValue)
        {
            double systemClcok = MCAN120803SystemClock;
            int brp = 0;
            int tseg1 = 0;
            int tseg2 = 0;
            int sjw = 0;

            if (customBaudRateValue[0] != 'B')
            {
                throw new ArgumentException("Az érték formátuma helytelen.", customBaudRateValue);
            }

            uint value = UInt32.Parse(customBaudRateValue, System.Globalization.NumberStyles.HexNumber);

            brp = ((int)value & 0x3FF) + 1;
            sjw = ((int)((value >> 10) & 0x03)) + 1;
            tseg1 = ((int)((value >> 12) & 0x0F)) + 1;
            tseg2 = ((int)((value >> 16) & 0x07)) + 1;

            return new TimeSegemnts(systemClcok, brp, tseg1, tseg2, sjw);
        }
    }
}
