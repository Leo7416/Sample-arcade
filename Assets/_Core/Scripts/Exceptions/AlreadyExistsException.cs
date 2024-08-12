using System;

namespace SampleArcade.Exceptions
{
    internal class AlreadyExistsException : Exception
    {
        private const string BaseMessage = "This element alredy exists in collection!";

        public AlreadyExistsException() : base() { }

        public AlreadyExistsException(string message) : base(message) { }

        public AlreadyExistsException(string message, Exception innerException) : base(message, innerException) { }
    }
}