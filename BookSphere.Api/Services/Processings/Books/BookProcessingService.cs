//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Bridge Knowledge and Curiosity
//==================================================

using System;
using System.Linq;
using System.Threading.Tasks;
using BookSphere.Api.Models.Foundations.Books;
using BookSphere.Api.Services.Foundations.Books;

namespace BookSphere.Api.Services.Processings.Books
{
    public class BookProcessingService : IBookProcessingService
    {
        private readonly IBookService bookService;

        public BookProcessingService(IBookService bookService)
        {
            this.bookService = bookService;
        }

        public async ValueTask<Book> RegisterAndSaveBookAsync(Book book)
        {
            var maybeBook = await this.bookService
                .RetrieveBookByIdAsync(book.BookId);

            var updatedBook = CreateBook(book, maybeBook);

            await this.bookService.ModifyBookAsync(updatedBook);

            return updatedBook;
        }

        public async ValueTask<Book> RetrieveBookByIdAsync(Guid bookId) =>
            await this.bookService.RetrieveBookByIdAsync(bookId);

        public IQueryable<Book> RetrieveAllBooks() =>
            this.bookService.RetrieveAllBooks();

        public async ValueTask<Book> ModifyBookAsync(Book book)
        {
            var maybeBook = await this.bookService.RetrieveBookByIdAsync(book.BookId);

            var updatedBook = CreateBook(book, maybeBook);

            return await this.bookService.ModifyBookAsync(updatedBook);
        }

        public async ValueTask<Book> RemoveBookByIdAsync(Guid bookId) =>
            await this.bookService.RemoveBookByIdAsync(bookId);

        private static Book CreateBook(Book inputBook, Book maybeBook)
        {
            var bookId = maybeBook?.BookId ?? Guid.NewGuid();

            return new Book
            {
                BookId = bookId,
                BookTitle = inputBook.BookTitle,
                Author = inputBook.Author,
                Genre = inputBook.Genre,
                ReaderId = inputBook.Reader?.ReaderId ?? inputBook.ReaderId,
                Reader = inputBook.Reader
            };
        }
    }
}
