using System;

namespace Konvolucio.MUDS150628
{
    public interface IPhysicalLink
    {
        void Write(byte[] data, ref string log);
        void Read(out byte[] data, int tiemoutMs, ref string log);
    }
}
