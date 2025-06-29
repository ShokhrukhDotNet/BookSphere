//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Bridge Knowledge and Curiosity
//==================================================

using System;
using System.Linq;
using System.Threading.Tasks;
using BookSphere.Api.Models.Foundations.Books;

namespace BookSphere.Api.Services.Processings.Books
{
    public interface IBookProcessingService
    {
        ValueTask<Book> RegisterAndSaveBookAsync(Book book);
        IQueryable<Book> RetrieveAllBooks();
        ValueTask<Book> RetrieveBookByIdAsync(Guid bookId);
        ValueTask<Book> RemoveBookByIdAsync(Guid locationId);
        ValueTask<Book> ModifyBookAsync(Book book);
    }
}
