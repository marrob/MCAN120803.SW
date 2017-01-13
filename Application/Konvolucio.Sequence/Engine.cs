using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Konvolucio.Sequence
{
    class Engine
    {
        #region Public Const
        public const string TargetInvalid = "<Invalid Target>";
        public const string TargetNoDestination = "<No Target>";
        public const string ValueNotAvailable = "na";
        public const string GenericTimestampFomrat = "MM dd yyyy HH mm ss";
        public const string GenericDateFormat = "{0:MM} {0:dd} {0:yyyy}";
        public const string ThreadName = "MctEngineThread";
        #endregion 

        public StepItem CalledStep { get; set; }
        public Variables Variables;
        public Devices Devices;
        public StepCollection Sequence;

        /****************************************************************/
        public Engine()
        {

        }
    }
}
