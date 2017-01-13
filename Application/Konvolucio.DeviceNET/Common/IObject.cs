// -----------------------------------------------------------------------
// <copyright file="IObject.cs" company="">
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
    public interface IObject
    {
        byte ClassCode { get; }
        ISlaveDevice SlaveDevice { get; }
    }
}
