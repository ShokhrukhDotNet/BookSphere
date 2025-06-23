//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Bridge Knowledge and Curiosity
//==================================================

using Xeptions;

namespace BookSphere.Api.Models.Foundations.Books.Exceptions
{
    public class BookValidationException : Xeption
    {
        public BookValidationException(Xeption innerException)
            : base(message: "Book validation error occured, fix the errors and try again",
                  innerException)
        { }
    }
}
