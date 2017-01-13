using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Konvolucio.MCAN120803.GUI.Common
{
    public class CommonCanMessage
    {
        public string Name { get; set; }
        public string Source { get; set; }
        public ArbitrationIdType Type { get; set; }
        public bool Remote { get; set; }
        public uint ArbitrationId { get; set; }
        public byte[] Data { get; set; }
        public string Description { get; set; }
        public string Documentation { get; set; }
    }
}
      
            