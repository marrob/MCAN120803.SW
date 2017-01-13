// -----------------------------------------------------------------------
// <copyright file="NodeObjectItem.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Konvolucio.Database.API
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class NodeItem
    {
        public string Name { get; private set; }

        public int Address { get; private set; }

        public NodeItem(string name, int address)
        {
            Name = name;
            Address = address;
        }
    }
}
