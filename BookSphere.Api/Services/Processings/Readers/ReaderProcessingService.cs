//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Bridge Knowledge and Curiosity
//==================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookSphere.Api.Models.Foundations.Books;
using BookSphere.Api.Models.Foundations.Readers;
using BookSphere.Api.Services.Foundations.Books;
using BookSphere.Api.Services.Foundations.Readers;

namespace BookSphere.Api.Services.Processings.Readers
{
    public class ReaderProcessingService : IReaderProcessingService
    {
        private readonly IReaderService readerService;
        private readonly IBookService bookService;

        public ReaderProcessingService(
            IReaderService readerService,
            IBookService bookService)
        {
            this.readerService = readerService;
            this.bookService = bookService;
        }

        public async ValueTask<Reader> RegisterReaderWithBooksAsync(Reader reader)
        {
            reader.ReaderId = Guid.NewGuid();
            reader.Books ??= new List<Book>();

            foreach (Book book in reader.Books)
            {
                book.BookId = Guid.NewGuid();
                book.ReaderId = reader.ReaderId;
                book.Reader = reader;
            }

            await this.readerService.AddReaderAsync(reader);

            foreach (Book book in reader.Books)
            {
                await this.bookService.AddBookAsync(book);
            }

            return reader;
        }

        public async ValueTask<Reader> RetrieveReaderByIdAsync(Guid readerId) =>
            await this.readerService.RetrieveReaderByIdAsync(readerId);

        public IQueryable<Reader> RetrieveAllReaders() =>
            this.readerService.RetrieveAllReaders();

        public async ValueTask<Reader> ModifyReaderAsync(Reader reader) =>
            await this.readerService.ModifyReaderAsync(reader);

        public async ValueTask<Reader> RemoveReaderByIdAsync(Guid readerId) =>
            await this.readerService.RemoveReaderByIdAsync(readerId);
    }
}
