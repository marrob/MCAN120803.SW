// -----------------------------------------------------------------------
// <copyright file="Services.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Konvolucio.DeviceNet
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class Services
    {
        public const byte SRVS_GET_ATTRIBS_ALL	    = 0x01;
        public const byte SRVS_SET_ATTRIBS_ALL	    = 0x02;

        public const byte SRVS_RESET				= 0x05;
        public const byte SRVS_START				= 0x06;
        public const byte SRVS_STOP				    = 0x07;
        public const byte SRVS_CREATE				= 0x08;
        public const byte SRVS_DELETE				= 0x09;

        public const byte SRVS_APPLY_ATTRIBS		= 0x0D;
        public const byte SRVS_GET_ATTRIB_SINGLE	= 0x0E;

        public const byte SRVS_SET_ATTRIB_SINGLE	= 0x10;
        public const byte SRVS_FIND_NEXT_OBJ_INST	= 0x11;

        public const byte SRVS_ERROR_RESPONSE	    = 0x14;
        public const byte SRVS_RESTORE			    = 0x15;
        public const byte SRVS_SAVE				    = 0x16;
        public const byte SRVS_NO_OPERATION		    = 0x17;
        public const byte SRVS_GET_MEMBER			= 0x18;
        public const byte SRVS_SET_MEMBER			= 0x19;
        public const byte SRVS_INSERT_MEMBER		= 0x1A;
        public const byte SRVS_REMOVE_MEMBER		= 0x1B;

        public const byte SRVS_ALLOCATE_CONN		= 0x4B;
        public const byte SRVS_RELEASE_CONN         = 0x4C;

    }
}
