using System;


namespace Konvolucio.MUDS150628
{
    public class Iso15765Exception : Exception
    {
        public Iso15765Exception()
        {
        }

        public Iso15765Exception(string message)
            : base(message)
        {
        }

        public Iso15765Exception(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

    public class Iso15765TimeoutException : Exception
    {
        public Iso15765TimeoutException()
        {
        }

        public Iso15765TimeoutException(string message)
            : base(message)
        {
        }

        public Iso15765TimeoutException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
