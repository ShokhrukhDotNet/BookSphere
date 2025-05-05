//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Bridge Knowledge and Curiosity
//==================================================

using Xeptions;

namespace BookSphere.Api.Models.Foundations.Readers.Exceptions
{
    public class ReaderServiceException : Xeption
    {
        public ReaderServiceException(Xeption innerException)
            : base(message: "Reader service error occurred, contact support",
                  innerException)
        { }
    }
}
