//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Bridge Knowledge and Curiosity
//==================================================

using Xeptions;

namespace BookSphere.Api.Models.Foundations.Books.Exceptions
{
    public class BookDependencyException : Xeption
    {
        public BookDependencyException(Xeption innerException)
            : base(message: "Book dependency error occurred, contact support",
                  innerException)
        { }
    }
}
