//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Bridge Knowledge and Curiosity
//==================================================

using System;
using System.Linq;
using System.Threading.Tasks;
using BookSphere.Api.Models.Foundations.Books;

namespace BookSphere.Api.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<Book> InsertBookAsync(Book book);
        IQueryable<Book> SelectAllBooks();
        ValueTask<Book> SelectBookByIdAsync(Guid bookId);
        ValueTask<Book> UpdateBookAsync(Book book);
    }
}
