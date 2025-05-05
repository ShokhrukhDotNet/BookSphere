//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Bridge Knowledge and Curiosity
//==================================================

using Xeptions;

namespace BookSphere.Api.Models.Foundations.Readers.Exceptions
{
    public class ReaderDependencyValidationException : Xeption
    {
        public ReaderDependencyValidationException(Xeption innerException)
            : base(message: "Reader dependency validation error occurred, fix the errors and try again",
                  innerException)
        { }
    }
}
