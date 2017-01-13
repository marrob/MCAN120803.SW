

namespace Konvolucio.MCAN120803.API
{
    using System;

    class CanStatus
    {
        //Status
        const UInt16 ATM_ATTR_BAD_VALUE                 = 0x0001;
        const UInt16 ATM_ATTR_NOT_SUPPORTED             = 0x0002;
        const UInt16 ATM_ATTR_BAD_ID                    = 0x0003;
        const UInt16 ATM_ATTR_VALUE_OUT_OF_RANGE        = 0x0004;
        const UInt16 ATM_ATTR_NOT_SUPPORTED_THIS_MODE   = 0x0005;
        const UInt16 ATM_ATTR_READOLNY                  = 0x0006;

        /********************************************************************************/
        internal static string Decode(UInt16 status)
        {
            UInt16 code = (UInt16)(status & 0x3FFF);

            switch (code)
            {
                case ATM_ATTR_BAD_VALUE:
                    {
                        return "Attribute value is invalid. Code:-8613.";
                    }
                case ATM_ATTR_NOT_SUPPORTED:
                    {
                        return "Attribute value is not supported. Code:-8614.";
                    }
                case ATM_ATTR_BAD_ID:
                    {
                        return "Attribute id is unknown. Code:-8615.";
                    }
                case ATM_ATTR_VALUE_OUT_OF_RANGE:
                    {
                        return "Attribute value out of range. Code:-8616.";
                    }
                case ATM_ATTR_NOT_SUPPORTED_THIS_MODE:
                    {
                        return "This function not supported in this mode. Code:-8617.";
                    }
                case ATM_ATTR_READOLNY:
                    {
                        return "Attriubte is readonly. Code:-8618.";
                    }
                default:
                    {
                        return "Unknown status. Code:-8619.";
                    }
            }
        }
    }
}
