//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Bridge Knowledge and Curiosity
//==================================================

using Xeptions;

namespace BookSphere.Api.Models.Foundations.Books.Exceptions
{
    public class BookDependencyValidationException : Xeption
    {
        public BookDependencyValidationException(Xeption innerException)
            : base(message: "Book dependency validation error occurred, fix the errors and try again",
                   innerException)
        { }
    }
}
