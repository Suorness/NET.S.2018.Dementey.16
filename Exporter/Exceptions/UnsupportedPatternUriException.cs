namespace Exporter.Exceptions
{
    using System;

    /// <summary>
    /// It is thrown when working with an unknown pattern.
    /// </summary>
    public class UnsupportedPatternUriException : Exception
    {
        public UnsupportedPatternUriException()
        {
        }

        public UnsupportedPatternUriException(string message) : base(message)
        {
        }

        public UnsupportedPatternUriException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
