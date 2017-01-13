

namespace Konvolucio.MCAN120803.GUI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using AppModules.Log.Model;

    /// <summary>
    /// Egy darab Log fájl eseményeit kezeli.
    /// </summary>
    class LogFileAppEvent : IApplicationEvent
    {

        public FileChangingType ChangingType { get; private set; }
        public ILogFileItem LogFile { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="collection"></param>
        public LogFileAppEvent(ILogFileItem logFile, FileChangingType changingType)
        {
            ChangingType = changingType;
            LogFile = logFile;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string str = string.Empty;
            str += this.GetType().Name + ", ";
            str += "Name: " + (LogFile != null ? LogFile.Name : "UnKnown") +", ";
            str += "Event: " + ChangingType.ToString() + ".";
            return str;
        }
    }
}
