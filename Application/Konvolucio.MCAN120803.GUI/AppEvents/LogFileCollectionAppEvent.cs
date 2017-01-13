

namespace Konvolucio.MCAN120803.GUI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using AppModules.Log.Model;

    /// <summary>
    /// Az Össze logáfjl-ra egyben vonatkozik!
    /// </summary>
    class LogFileCollectionAppEvent : IApplicationEvent
    {

        public FileChangingType ChangingType { get; private set; }
        public ILogFileCollection LogFiles { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="collection"></param>
        public LogFileCollectionAppEvent(ILogFileCollection logFiles, FileChangingType changingType)
        {
            ChangingType = changingType;
            LogFiles = logFiles;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string str = string.Empty;
            str += this.GetType().Name + ", ";
            str += (LogFiles != null ? "IsLoaded:" + LogFiles.IsLoaded.ToString() : "UnKnown") + ", ";
            str += (LogFiles != null ? "Loaded Count:" + LogFiles.Count.ToString() : "UnKnown") + ", ";
            str += "Event: " + ChangingType.ToString() + ".";
            return str;
        }
    }
}
