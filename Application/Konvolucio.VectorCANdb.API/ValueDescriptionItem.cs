// -----------------------------------------------------------------------
// <copyright file="ValueDescriptionItem.cs" company="">
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
    public class ValueDescriptionItem
    {
        public int Value;
        public string Description;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="descripiton"></param>
        public ValueDescriptionItem(int value, string descripiton)
        {
            Value = value;
            Description = descripiton;
        }
    }
}
