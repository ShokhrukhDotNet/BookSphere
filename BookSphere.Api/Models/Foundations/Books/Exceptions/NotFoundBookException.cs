//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Bridge Knowledge and Curiosity
//==================================================

using System;
using Xeptions;

namespace BookSphere.Api.Models.Foundations.Books.Exceptions
{
    public class NotFoundBookException : Xeption
    {
        public NotFoundBookException(Guid bookId)
            : base(message: $"Couldn't find book with id {bookId}")
        { }
    }
}
