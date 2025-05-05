//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Bridge Knowledge and Curiosity
//==================================================

using System;
using Xeptions;

namespace BookSphere.Api.Models.Foundations.Readers.Exceptions
{
    public class AlreadyExistReaderException : Xeption
    {
        public AlreadyExistReaderException(Exception innerException)
            : base(message: "Reader already exists", innerException)
        { }
    }
}
