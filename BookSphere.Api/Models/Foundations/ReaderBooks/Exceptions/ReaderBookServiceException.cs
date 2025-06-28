//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Bridge Knowledge and Curiosity
//==================================================

using Xeptions;

namespace BookSphere.Api.Models.Foundations.ReaderBooks.Exceptions
{
    public class ReaderBookServiceException : Xeption
    {
        public ReaderBookServiceException(Xeption innerException)
            : base(message: "ReaderBook service error occurred, contact support",
                  innerException)
        { }
    }
}
