//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Bridge Knowledge and Curiosity
//==================================================

using Xeptions;

namespace BookSphere.Api.Models.Foundations.Readers.Exceptions
{
    public class ReaderValidationException : Xeption
    {
        public ReaderValidationException(Xeption innerException)
            : base(message: "Reader validation error occured, fix the errors and try again",
                  innerException)
        { }
    }
}
