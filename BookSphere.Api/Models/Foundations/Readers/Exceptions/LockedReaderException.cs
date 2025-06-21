//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Bridge Knowledge and Curiosity
//==================================================

using System;
using Xeptions;

namespace BookSphere.Api.Models.Foundations.Readers.Exceptions
{
    public class LockedReaderException : Xeption
    {
        public LockedReaderException(Exception innerException)
            : base(message: "Reader is locked, please try again", innerException)
        { }
    }
}
