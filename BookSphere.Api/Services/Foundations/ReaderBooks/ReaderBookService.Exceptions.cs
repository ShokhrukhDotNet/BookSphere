//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Bridge Knowledge and Curiosity
//==================================================

using System;
using System.Linq;
using System.Threading.Tasks;
using BookSphere.Api.Models.Foundations.ReaderBooks;
using BookSphere.Api.Models.Foundations.ReaderBooks.Exceptions;
using BookSphere.Api.Models.Foundations.Readers.Exceptions;
using Microsoft.Data.SqlClient;
using Xeptions;

namespace BookSphere.Api.Services.Foundations.ReaderBooks
{
    public partial class ReaderBookService
    {
        private delegate ValueTask<ReaderBook> ReturningReaderBookFunction();
        private delegate IQueryable<ReaderBook> ReturningReaderBooksFunction();

        private async ValueTask<ReaderBook> TryCatch(ReturningReaderBookFunction returningReaderBookFunction)
        {
            try
            {
                return await returningReaderBookFunction();
            }
            catch (NullReaderException nullReaderException)
            {
                throw CreateAndLogValidationException(nullReaderException);
            }
            catch (InvalidReaderException invalidReaderException)
            {
                throw CreateAndLogValidationException(invalidReaderException);
            }
            catch (NotFoundReaderException notFoundReaderException)
            {
                throw CreateAndLogValidationException(notFoundReaderException);
            }
            catch (SqlException sqlException)
            {
                var failedReaderBookStorageException = new FailedReaderBookStorageException(sqlException);

                throw CreateAndLogCriticalDependencyException(failedReaderBookStorageException);
            }
            catch (Exception exception)
            {
                var failedReaderBookServiceException = new FailedReaderBookServiceException(exception);

                throw CreateAndLogServiceException(failedReaderBookServiceException);
            }
        }

        private IQueryable<ReaderBook> TryCatch(ReturningReaderBooksFunction returningReaderBooksFunction)
        {
            try
            {
                return returningReaderBooksFunction();
            }
            catch (SqlException sqlException)
            {
                var failedReaderBookStorageException = new FailedReaderBookStorageException(sqlException);

                throw CreateAndLogCriticalDependencyException(failedReaderBookStorageException);
            }
            catch (Exception exception)
            {
                var failedReaderBookServiceException = new FailedReaderBookServiceException(exception);

                throw CreateAndLogServiceException(failedReaderBookServiceException);
            }
        }

        private ReaderBookValidationException CreateAndLogValidationException(Xeption exception)
        {
            var readerBookValidationException = new ReaderBookValidationException(exception);

            this.loggingBroker.LogError(readerBookValidationException);

            return readerBookValidationException;
        }

        private ReaderBookDependencyException CreateAndLogCriticalDependencyException(Xeption exception)
        {
            var readerBookDependencyException = new ReaderBookDependencyException(exception);

            this.loggingBroker.LogCritical(readerBookDependencyException);

            return readerBookDependencyException;
        }

        private ReaderBookServiceException CreateAndLogServiceException(Xeption exception)
        {
            var readerBookServiceException = new ReaderBookServiceException(exception);

            this.loggingBroker.LogError(readerBookServiceException);

            return readerBookServiceException;
        }
    }
}
