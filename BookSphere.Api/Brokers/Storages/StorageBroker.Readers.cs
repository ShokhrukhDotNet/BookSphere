//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Bridge Knowledge and Curiosity
//==================================================

using System;
using System.Linq;
using System.Threading.Tasks;
using BookSphere.Api.Models.Foundations.Readers;
using Microsoft.EntityFrameworkCore;

namespace BookSphere.Api.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<Reader> Readers { get; set; }

        public async ValueTask<Reader> InsertReaderAsync(Reader reader) =>
            await InsertAsync(reader);

        public IQueryable<Reader> SelectAllReaders() => SelectAll<Reader>();

        public async ValueTask<Reader> SelectReaderByIdAsync(Guid readerId) =>
            await SelectAsync<Reader>(readerId);

        public IQueryable<Reader> SelectAllReadersWithBooks() =>
            this.Readers.Include(reader => reader.Books);

        public async ValueTask<Reader> SelectReaderWithBooksByIdAsync(Guid readerId) =>
            await this.Readers
                .Include(reader => reader.Books)
                .FirstOrDefaultAsync(reader => reader.ReaderId == readerId);


        public async ValueTask<Reader> UpdateReaderAsync(Reader reader) =>
            await UpdateAsync(reader);

        public async ValueTask<Reader> DeleteReaderAsync(Reader reader) =>
            await DeleteAsync(reader);
    }
}
