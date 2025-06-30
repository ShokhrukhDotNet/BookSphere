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
            book.BookId = Guid.NewGuid();
            book.ReaderId = book.Reader?.ReaderId ?? book.ReaderId;

            return await this.bookService.AddBookAsync(book);
        }

        public async ValueTask<Book> RetrieveBookByIdAsync(Guid bookId) =>
            await this.bookService.RetrieveBookByIdAsync(bookId);

        public IQueryable<Book> RetrieveAllBooks() =>
            this.bookService.RetrieveAllBooks();

        public async ValueTask<Book> ModifyBookAsync(Book book)
        {
            var updatedBook = CreateBook(book);

            return await this.bookService.ModifyBookAsync(updatedBook);
        }

        public async ValueTask<Book> RemoveBookByIdAsync(Guid bookId) =>
            await this.bookService.RemoveBookByIdAsync(bookId);

        private static Book CreateBook(Book inputBook)
        {
            return new Book
            {
                BookId = inputBook.BookId,
                BookTitle = inputBook.BookTitle,
                Author = inputBook.Author,
                Genre = inputBook.Genre,
                ReaderId = inputBook.Reader?.ReaderId ?? inputBook.ReaderId,
                Reader = inputBook.Reader
            };
        }
    }
}
