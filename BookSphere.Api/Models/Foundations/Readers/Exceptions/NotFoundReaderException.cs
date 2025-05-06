//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Bridge Knowledge and Curiosity
//==================================================

using System;
using Xeptions;

namespace BookSphere.Api.Models.Foundations.Readers.Exceptions
{
    public class NotFoundReaderException : Xeption
    {
        public NotFoundReaderException(Guid readerId)
            : base(message: $"Couldn't find reader with id {readerId}")
        { }
    }
}
