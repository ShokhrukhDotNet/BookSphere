//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Bridge Knowledge and Curiosity
//==================================================

using Xeptions;

namespace BookSphere.Api.Models.Foundations.Readers.Exceptions
{
    public class NullReaderException : Xeption
    {
        public NullReaderException()
            : base(message: "Reader is null")
        { }
    }
}
