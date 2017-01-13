
namespace Konvolucio.MCAN120803.API
{
    using System;

    public class CanAdapterException : Exception
    {
        public CanAdapterException()
        {
        }

        public CanAdapterException(string message)
            : base(message)
        {
        }

        public CanAdapterException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
