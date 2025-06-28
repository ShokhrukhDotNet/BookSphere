//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Bridge Knowledge and Curiosity
//==================================================

using System;
using System.Linq;
using System.Threading.Tasks;
using BookSphere.Api.Models.Foundations.ReaderBooks;

namespace BookSphere.Api.Services.Foundations.ReaderBooks
{
    public interface IReaderBookService
    {
        IQueryable<ReaderBook> RetrieveAllReaderBooks();
        ValueTask<ReaderBook> RetrieveReaderBookByIdAsync(Guid readerId);
    }
}
