//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Bridge Knowledge and Curiosity
//==================================================

using Xeptions;

namespace BookSphere.Api.Models.Foundations.Books.Exceptions
{
    public class BookServiceException : Xeption
    {
        public BookServiceException(Xeption innerException)
            : base(message: "Book service error occurred, contact support",
                  innerException)
        { }
    }
}
