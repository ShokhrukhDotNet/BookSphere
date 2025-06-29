//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Bridge Knowledge and Curiosity
//==================================================

using System;
using System.Linq;
using System.Threading.Tasks;
using BookSphere.Api.Models.Foundations.Readers;

namespace BookSphere.Api.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<Reader> InsertReaderAsync(Reader reader);
        IQueryable<Reader> SelectAllReaders();
        ValueTask<Reader> SelectReaderByIdAsync(Guid readerId);
        ValueTask<Reader> UpdateReaderAsync(Reader reader);
        ValueTask<Reader> DeleteReaderAsync(Reader reader);
        IQueryable<Reader> SelectAllReadersWithBooks();
        ValueTask<Reader> SelectReaderWithBooksByIdAsync(Guid readerId);
    }
}
