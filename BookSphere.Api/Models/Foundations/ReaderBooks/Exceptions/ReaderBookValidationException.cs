//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Bridge Knowledge and Curiosity
//==================================================

using Xeptions;

namespace BookSphere.Api.Models.Foundations.ReaderBooks.Exceptions
{
    public class ReaderBookValidationException : Xeption
    {
        public ReaderBookValidationException(Xeption innerException)
            : base(message: "ReaderBook validation error occured, fix the errors and try again",
                  innerException)
        { }
    }
}
