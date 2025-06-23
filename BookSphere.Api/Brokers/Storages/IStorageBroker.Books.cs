//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Bridge Knowledge and Curiosity
//==================================================

using System.Threading.Tasks;
using BookSphere.Api.Models.Foundations.Books;

namespace BookSphere.Api.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<Book> InsertBookAsync(Book book);
    }
}
