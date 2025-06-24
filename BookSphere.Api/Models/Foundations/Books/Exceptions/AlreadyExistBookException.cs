//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Bridge Knowledge and Curiosity
//==================================================

using System;
using Xeptions;

namespace BookSphere.Api.Models.Foundations.Books.Exceptions
{
    public class AlreadyExistBookException : Xeption
    {
        public AlreadyExistBookException(Exception innerException)
            : base(message: "Book already exists", innerException)
        { }
    }
}
