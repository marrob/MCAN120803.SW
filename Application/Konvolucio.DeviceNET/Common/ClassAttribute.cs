// -----------------------------------------------------------------------
// <copyright file="ClassAttribute.cs" company="">
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
    public class ClassAttribute<T>
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public DataType Type { get; private set; }
        private T Value { get; set; }

        public ClassAttribute(IObject obj, byte instanceId, byte attributeId, string name, DataType type)
        {

        }
    }
}
