//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Bridge Knowledge and Curiosity
//==================================================

using System;
using Xeptions;

namespace BookSphere.Api.Models.Foundations.Readers.Exceptions
{
    public class FailedReaderServiceException : Xeption
    {
        public FailedReaderServiceException(Exception innerException)
            : base(message: "Failed reader service error occurred, contact support",
                  innerException)
        { }
    }
}
