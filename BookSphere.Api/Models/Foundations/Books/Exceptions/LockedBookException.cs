//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Bridge Knowledge and Curiosity
//==================================================

using System;
using Xeptions;

namespace BookSphere.Api.Models.Foundations.Books.Exceptions
{
    public class LockedBookException : Xeption
    {
        public LockedBookException(Exception innerException)
            : base(message: "Book is locked, please try again", innerException)
        { }
    }
}
