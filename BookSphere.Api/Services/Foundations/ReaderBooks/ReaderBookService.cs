//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Bridge Knowledge and Curiosity
//==================================================

using System;
using System.Linq;
using System.Threading.Tasks;
using BookSphere.Api.Brokers.DateTimes;
using BookSphere.Api.Brokers.Loggings;
using BookSphere.Api.Brokers.Storages;
using BookSphere.Api.Models.Foundations.ReaderBooks;
using BookSphere.Api.Models.Foundations.Readers;
using Microsoft.EntityFrameworkCore;

namespace BookSphere.Api.Services.Foundations.ReaderBooks
{
    public partial class ReaderBookService : IReaderBookService
    {
        private readonly IStorageBroker storageBroker;
        private readonly IDateTimeBroker dateTimeBroker;
        private readonly ILoggingBroker loggingBroker;

        public ReaderBookService(
            IStorageBroker storageBroker,
            IDateTimeBroker dateTimeBroker,
            ILoggingBroker loggingBroker)
        {
            this.storageBroker = storageBroker;
            this.dateTimeBroker = dateTimeBroker;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<ReaderBook> RetrieveReaderBookByIdAsync(Guid readerId) =>
        TryCatch(async () =>
        {
            ValidateReaderId(readerId);

            Reader maybeReader = await this.storageBroker
                .SelectReaderWithBooksByIdAsync(readerId);

            ValidateStorageReader(maybeReader, readerId);

            return new ReaderBook
            {
                Reader = maybeReader,
                Books = maybeReader.Books
            };
        });

        public IQueryable<ReaderBook> RetrieveAllReaderBooks() =>
            TryCatch(() =>
                this.storageBroker.SelectAllReadersWithBooks()
                    .Select(reader => new ReaderBook
                    {
                        Reader = reader,
                        Books = reader.Books
                    }));
    }
}
