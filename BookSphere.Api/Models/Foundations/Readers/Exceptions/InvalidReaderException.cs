//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Bridge Knowledge and Curiosity
//==================================================

using Xeptions;

namespace BookSphere.Api.Models.Foundations.Readers.Exceptions
{
    public class InvalidReaderException : Xeption
    {
        public InvalidReaderException()
            : base(message: "Reader is invalid")
        { }

        public InvalidReaderException(string parameterName, object parameterValue)
            : base(message: $"Invalid Reader: Parameter '{parameterName}' is invalid, value: '{parameterValue}'.")
        { }
    }
}
