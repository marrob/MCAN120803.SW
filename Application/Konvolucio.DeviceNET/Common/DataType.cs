// -----------------------------------------------------------------------
// <copyright file="DataType.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Konvolucio.DeviceNet.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public enum DataType
    {
        /// <summary>
        /// 1 byte
        /// </summary>
        BOOL,

        /// <summary>
        /// 1 byte unsigned
        /// </summary>
        USINT,

        /// <summary>
        /// 2 byte unsigned
        /// </summary>
        UINT,

        /// <summary>
        /// 4 byte bit string
        /// </summary>
        DWORD,

        /// <summary>
        /// 4 byte unsigned
        /// </summary>
        UDINT,

        /// <summary>
        /// 1 byte length + 1 byte per character
        /// </summary>
        SHORT_STRING,
    }
}
