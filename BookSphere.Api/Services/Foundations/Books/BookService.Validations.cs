//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Bridge Knowledge and Curiosity
//==================================================

using BookSphere.Api.Models.Foundations.Books;
using BookSphere.Api.Models.Foundations.Books.Exceptions;

namespace BookSphere.Api.Services.Foundations.Books
{
    public partial class BookService
    {
        private static void ValidateBookNotNull(Book book)
        {
            if (book is null)
            {
                throw new NullBookException();
            }
        }
    }
}
