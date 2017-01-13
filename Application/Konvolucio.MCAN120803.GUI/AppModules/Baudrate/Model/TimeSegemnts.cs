// -----------------------------------------------------------------------
// <copyright file="TimeSegemnts.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Konvolucio.MCAN120803.GUI.AppModules.Baudrate.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class TimeSegemnts
    {
        /// <summary>
        /// 
        /// </summary>
        public double SystemClock { get; private set; }
        /// <summary>
        /// Baud Rate Prescaler
        /// </summary>
        public int Brp { get; private set; }
        /// <summary>
        /// Time Segments Before Sample
        /// </summary>
        public int Tseg1 { get; private set; }
        /// <summary>
        /// Time Segments After Sample
        /// </summary>
        public int Tseg2 { get; private set; }
        /// <summary>
        /// Sync Jump Width
        /// </summary>
        public int Sjw { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="systemClock">System Clock [Hz]</param>
        /// <param name="brp">Baudrate Rate Prescaler [1..1024]</param>
        /// <param name="tseg1">Time Segment1 [1..16] </param>
        /// <param name="tseg2">Time Segment2 [1..8]</param>
        /// <param name="sjw">Sync Jump Width [1..4]</param>
        public TimeSegemnts(double systemClock, int brp, int tseg1, int tseg2, int sjw)
        {
            SystemClock = systemClock;
            Brp = brp;
            Tseg1 = tseg1;
            Tseg2 = tseg2;
            Sjw = sjw;
        }
    }
}
