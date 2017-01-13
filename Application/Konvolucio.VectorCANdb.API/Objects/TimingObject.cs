// -----------------------------------------------------------------------
// <copyright file="TimingObject.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Konvolucio.VectorCANdb.API
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    /// <summary>
    /// 5. Bit Timing Definition
    /// Keyword: BS_
    /// </summary>
    public class TimingObject : DbcObject<TimingObject>
    {
        public int Baudrate;
        public int BTR1;
        public int BTR2;
        public string Input;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public TimingObject Parse(string input)
        {
            Input = input;
            return this;
        }

        public string ToDbcFormat()
        {
            throw new NotImplementedException();
        }
    }
}
