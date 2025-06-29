//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Bridge Knowledge and Curiosity
//==================================================

using System;
using System.Linq;
using System.Threading.Tasks;
using BookSphere.Api.Models.Foundations.Readers;

namespace BookSphere.Api.Services.Processings.Readers
{
    public interface IReaderProcessingService
    {
        ValueTask<Reader> RegisterReaderWithBooksAsync(Reader reader);
        ValueTask<Reader> RetrieveReaderByIdAsync(Guid readerId);
        IQueryable<Reader> RetrieveAllReaders();
        ValueTask<Reader> ModifyReaderAsync(Reader reader);
        ValueTask<Reader> RemoveReaderByIdAsync(Guid readerId);
    }
}
