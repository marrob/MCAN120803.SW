

namespace Konvolucio.MCAN120803.GUI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using AppModules.Log.Model;

    /// <summary>
    /// Küldi, ha LogGrid-ben új kiejlölés történt
    /// </summary>
    class LogSelectionChangedAppEvent : IApplicationEvent
    {
        public List<ILogMessageItem> SelectedItems { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="collection"></param>
        public LogSelectionChangedAppEvent(List<ILogMessageItem> collection)
        {
            this.SelectedItems = collection;
        }

        /// <summary>
        /// 
        /// </summary>
        public LogSelectionChangedAppEvent()
        {
            this.SelectedItems = new List<ILogMessageItem>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (SelectedItems != null)
                return "LogSelectionChangedAppEvent, Count:" + SelectedItems.Count.ToString();
            else
                return "LogSelectionChangedAppEvent";
        }
    }
}
