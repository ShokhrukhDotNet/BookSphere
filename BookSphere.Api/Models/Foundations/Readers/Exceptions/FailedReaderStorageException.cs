//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Bridge Knowledge and Curiosity
//==================================================

using System;
using Xeptions;

namespace BookSphere.Api.Models.Foundations.Readers.Exceptions
{
    public class FailedReaderStorageException : Xeption
    {
        public FailedReaderStorageException(Exception innerException)
            : base(message: "Failed reader storage error occurred, contact support",
                  innerException)
        { }
    }
}
