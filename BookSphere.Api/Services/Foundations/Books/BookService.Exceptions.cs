//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Bridge Knowledge and Curiosity
//==================================================

using System.Threading.Tasks;
using BookSphere.Api.Models.Foundations.Books;
using BookSphere.Api.Models.Foundations.Books.Exceptions;
using Xeptions;

namespace BookSphere.Api.Services.Foundations.Books
{
    public partial class BookService
    {
        private delegate ValueTask<Book> ReturningBookFunction();

        private async ValueTask<Book> TryCatch(ReturningBookFunction returningBookFunction)
        {
            try
            {
                return await returningBookFunction();
    }
            catch (NullBookException nullBookException)
            {
                throw CreateAndLogValidationException(nullBookException);
}
        }

        private BookValidationException CreateAndLogValidationException(Xeption exception)
        {
            var bookValidationException =
                new BookValidationException(exception);

            this.loggingBroker.LogError(bookValidationException);

            return bookValidationException;
        }
    }
}
