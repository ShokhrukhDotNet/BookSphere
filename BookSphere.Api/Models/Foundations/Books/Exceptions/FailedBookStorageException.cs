//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Bridge Knowledge and Curiosity
//==================================================

using System;
using Xeptions;

namespace BookSphere.Api.Models.Foundations.Books.Exceptions
{
    public class FailedBookStorageException : Xeption
    {
        public FailedBookStorageException(Exception innerException)
            : base(message: "Failed book storage error occurred, contact support",
                  innerException)
        { }
    }
}
