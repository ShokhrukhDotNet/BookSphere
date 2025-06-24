//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Bridge Knowledge and Curiosity
//==================================================

using Xeptions;

namespace BookSphere.Api.Models.Foundations.Books.Exceptions
{
    public class NullBookException : Xeption
    {
        public NullBookException()
            : base(message: "Book is null")
        { }
    }
}
