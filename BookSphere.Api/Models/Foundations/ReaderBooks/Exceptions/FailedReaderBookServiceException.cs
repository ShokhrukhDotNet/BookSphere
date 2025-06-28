//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Bridge Knowledge and Curiosity
//==================================================

using System;
using Xeptions;

namespace BookSphere.Api.Models.Foundations.ReaderBooks.Exceptions
{
    public class FailedReaderBookServiceException : Xeption
    {
        public FailedReaderBookServiceException(Exception innerException)
            : base(message: "Failed readerbook service error occurred, contact support",
                  innerException)
        { }
    }
}
