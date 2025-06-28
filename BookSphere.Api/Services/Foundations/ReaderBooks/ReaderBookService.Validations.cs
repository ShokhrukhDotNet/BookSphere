//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Bridge Knowledge and Curiosity
//==================================================

using System;
using BookSphere.Api.Models.Foundations.Readers;
using BookSphere.Api.Models.Foundations.Readers.Exceptions;

namespace BookSphere.Api.Services.Foundations.ReaderBooks
{
    public partial class ReaderBookService
    {
        private static void ValidateReaderId(Guid readerId)
        {
            if (readerId == Guid.Empty)
            {
                throw new InvalidReaderException(
                    parameterName: nameof(Reader.ReaderId),
                    parameterValue: readerId);
            }
        }

        private static void ValidateStorageReader(Reader maybeReader, Guid readerId)
        {
            if (maybeReader is null)
            {
                throw new NotFoundReaderException(readerId);
            }
        }
    }
}
