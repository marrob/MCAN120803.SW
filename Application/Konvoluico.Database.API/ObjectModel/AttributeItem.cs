// -----------------------------------------------------------------------
// <copyright file="AttributeItem.cs" company="">
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
    public class AttributeItem
    {
        public string Name;

        public string Object;

        public string Type;

        public string ToolTip;

        public string ShowName;

        public string Unit;

        public AttributeItem(string name, string objectName, string type, string toolTip, string showName, string unit )
        {
            Name = name;
            Object = objectName;
            Type = type;
            ToolTip = toolTip;
            ShowName = showName;
            Unit = unit;
        }
    }
}
