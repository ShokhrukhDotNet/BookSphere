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

        public IQueryable<Book> SelectAllBooks() => SelectAll<Book>();

        public async ValueTask<Book> SelectBookByIdAsync(Guid bookId) =>
            await SelectAsync<Book>(bookId);
    }
}
