//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Bridge Knowledge and Curiosity
//==================================================

using Xeptions;

namespace BookSphere.Api.Models.Foundations.Books.Exceptions
{
    public class InvalidBookException : Xeption
    {
        public InvalidBookException()
            : base(message: "Book is invalid")
        { }
    }
}
