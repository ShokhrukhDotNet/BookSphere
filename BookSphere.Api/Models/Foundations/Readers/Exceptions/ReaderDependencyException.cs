//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Bridge Knowledge and Curiosity
//==================================================

using Xeptions;

namespace BookSphere.Api.Models.Foundations.Readers.Exceptions
{
    public class ReaderDependencyException : Xeption
    {
        public ReaderDependencyException(Xeption innerException)
            : base(message: "Reader dependency error occurred, contact support",
                  innerException)
        { }
    }
}
