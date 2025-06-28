//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Bridge Knowledge and Curiosity
//==================================================

using System;
using Xeptions;

namespace BookSphere.Api.Models.Foundations.ReaderBooks.Exceptions
{
    public class FailedReaderBookStorageException : Xeption
    {
        public FailedReaderBookStorageException(Exception innerException)
            : base(message: "Failed readerbook storage error occurred, contact support",
                  innerException)
        { }
    }
}
