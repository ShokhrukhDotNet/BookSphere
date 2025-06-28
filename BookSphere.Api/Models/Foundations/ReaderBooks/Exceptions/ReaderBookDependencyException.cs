//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Bridge Knowledge and Curiosity
//==================================================

using Xeptions;

namespace BookSphere.Api.Models.Foundations.ReaderBooks.Exceptions
{
    public class ReaderBookDependencyException : Xeption
    {
        public ReaderBookDependencyException(Xeption innerException)
            : base(message: "ReaderBook dependency error occurred, contact support",
                  innerException)
        { }
    }
}
