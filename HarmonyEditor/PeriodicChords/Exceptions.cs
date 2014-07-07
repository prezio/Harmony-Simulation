using System;

namespace PeriodicChords
{
    public class SoundOutOfRangeException : Exception
    {
        public SoundOutOfRangeException()
        {
        }
        public SoundOutOfRangeException(string message)
            : base(message)
        {
        }
        public SoundOutOfRangeException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
