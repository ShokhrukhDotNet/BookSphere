//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Bridge Knowledge and Curiosity
//==================================================

using BookSphere.Api.Models.Foundations.Readers;
using BookSphere.Api.Models.Foundations.Readers.Exceptions;

namespace BookSphere.Api.Services.Foundations.Readers
{
    public partial class ReaderService
    {
        private static void ValidateReaderNotNull(Reader reader)
        {
            if (reader is null)
            {
                throw new NullReaderException();
            }
        }
    }
}
