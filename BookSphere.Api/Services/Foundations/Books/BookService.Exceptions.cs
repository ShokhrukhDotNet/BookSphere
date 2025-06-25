//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Bridge Knowledge and Curiosity
//==================================================

using System;
using System.Linq;
using System.Threading.Tasks;
using BookSphere.Api.Models.Foundations.Books;
using BookSphere.Api.Models.Foundations.Books.Exceptions;
using EFxceptions.Models.Exceptions;
using Microsoft.Data.SqlClient;
using Xeptions;

namespace BookSphere.Api.Services.Foundations.Books
{
    public partial class BookService
    {
        private delegate ValueTask<Book> ReturningBookFunction();
        private delegate IQueryable<Book> ReturningBooksFunction();

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
            catch (InvalidBookException invalidBookException)
            {
                throw CreateAndLogValidationException(invalidBookException);
            }
            catch (SqlException sqlException)
            {
                var failedBookStorageException = new FailedBookStorageException(sqlException);

                throw CreateAndLogCriticalDependencyException(failedBookStorageException);
            }
            catch (NotFoundBookException notFoundBookException)
            {
                throw CreateAndLogValidationException(notFoundBookException);
            }
            catch (DuplicateKeyException duplicateKeyException)
            {
                var alreadyExistBookException =
                    new AlreadyExistBookException(duplicateKeyException);

                throw CreateAndLogDependencyValidationException(alreadyExistBookException);
            }
            //catch (Exception exception)
            //{
            //    var failedBookServiceException =
            //        new FailedBookServiceException(exception);

            //    throw CreateAndLogServiceException(failedBookServiceException);
            //}
        }

        private IQueryable<Book> TryCatch(ReturningBooksFunction returningBooksFunction)
        {
            try
            {
                return returningBooksFunction();
            }
            catch (SqlException sqlException)
            {
                var failedBookStorageException =
                    new FailedBookStorageException(sqlException);

                throw CreateAndLogCriticalDependencyException(failedBookStorageException);
            }
            catch (Exception exception)
            {
                var failedBookServiceException =
                    new FailedBookServiceException(exception);

                throw CreateAndLogServiceException(failedBookServiceException);
            }
        }

        private BookValidationException CreateAndLogValidationException(Xeption exception)
        {
            var bookValidationException =
                new BookValidationException(exception);

            this.loggingBroker.LogError(bookValidationException);

            return bookValidationException;
        }

        private BookDependencyException CreateAndLogCriticalDependencyException(Xeption exception)
        {
            var bookDependencyException = new BookDependencyException(exception);
            this.loggingBroker.LogCritical(bookDependencyException);

            return bookDependencyException;
        }

        private BookDependencyValidationException CreateAndLogDependencyValidationException(
            Xeption exception)
        {
            var bookDependencyValidationException =
                new BookDependencyValidationException(exception);

            this.loggingBroker.LogError(bookDependencyValidationException);

            return bookDependencyValidationException;
        }

        private BookServiceException CreateAndLogServiceException(Xeption exception)
        {
            var bookServiceException = new BookServiceException(exception);
            this.loggingBroker.LogError(bookServiceException);

            return bookServiceException;
        }
    }
}
