

namespace Konvolucio.MCAN120803.API
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// 
    /// </summary>
    public enum CanState:uint
    {
        SDEV_NONE  = 0x00000,
        SDEV_START = 0x00001,     
        SDEV_STOP  = 0x00002,
        SDEV_RESET = 0x00003,
        SDEV_FAIL =  0x00004,
        SDEV_IDLE  = 0x00005,     
    }
}
