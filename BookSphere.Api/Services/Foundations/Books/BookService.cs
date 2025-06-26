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
using BookSphere.Api.Models.Foundations.Books;

namespace BookSphere.Api.Services.Foundations.Books
{
    public partial class BookService : IBookService
    {
        private readonly IStorageBroker storageBroker;
        private readonly IDateTimeBroker dateTimeBroker;
        private readonly ILoggingBroker loggingBroker;

        public BookService(
            IStorageBroker storageBroker,
            IDateTimeBroker dateTimeBroker,
            ILoggingBroker loggingBroker)
        {
            this.storageBroker = storageBroker;
            this.dateTimeBroker = dateTimeBroker;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<Book> AddBookAsync(Book book) =>
        TryCatch(async () =>
        {
            ValidateBookOnAdd(book);

            return await this.storageBroker.InsertBookAsync(book);
        });

        public ValueTask<Book> RetrieveBookByIdAsync(Guid bookId) =>
        TryCatch(async () =>
        {
            ValidateBookId(bookId);

            Book maybeBook = await this.storageBroker.SelectBookByIdAsync(bookId);

            ValidateStorageBook(maybeBook, bookId);

            return maybeBook;
        });

        public IQueryable<Book> RetrieveAllBooks() =>
            TryCatch(() => this.storageBroker.SelectAllBooks());

        public ValueTask<Book> ModifyBookAsync(Book book) =>
        TryCatch(async () =>
        {
            ValidateBookNotNull(book);

            Book maybeBook =
                await this.storageBroker.SelectBookByIdAsync(book.BookId);

            return await this.storageBroker.UpdateBookAsync(book);
        });
    }
}
