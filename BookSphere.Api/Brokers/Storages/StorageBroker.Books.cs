//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Bridge Knowledge and Curiosity
//==================================================

using System;
using System.Linq;
using System.Threading.Tasks;
using BookSphere.Api.Models.Foundations.Books;
using Microsoft.EntityFrameworkCore;

namespace BookSphere.Api.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<Book> Books { get; set; }

        public async ValueTask<Book> InsertBookAsync(Book book) =>
            await InsertAsync(book);

        public IQueryable<Book> SelectAllBooks()
        {
            var books = SelectAll<Book>().Include(a => a.Reader);

            return books;
        }

        public async ValueTask<Book> SelectBookByIdAsync(Guid bookId)
        {
            var readertWithBooks = Books
            .Include(a => a.Reader)
                .FirstOrDefault(a => a.BookId == bookId);

            return await ValueTask.FromResult(readertWithBooks);
        }

        public async ValueTask<Book> UpdateBookAsync(Book book) =>
            await UpdateAsync(book);

        public async ValueTask<Book> DeleteBookAsync(Book book) =>
            await DeleteAsync(book);
    }
}
