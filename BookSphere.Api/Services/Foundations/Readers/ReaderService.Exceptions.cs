﻿//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Bridge Knowledge and Curiosity
//==================================================

using System;
using System.Linq;
using System.Threading.Tasks;
using BookSphere.Api.Models.Foundations.Readers;
using BookSphere.Api.Models.Foundations.Readers.Exceptions;
using EFxceptions.Models.Exceptions;
using Microsoft.Data.SqlClient;
using Xeptions;

namespace BookSphere.Api.Services.Foundations.Readers
{
    public partial class ReaderService
    {
        private delegate ValueTask<Reader> ReturningReaderFunction();
        private delegate IQueryable<Reader> ReturningReadersFunction();

        private async ValueTask<Reader> TryCatch(ReturningReaderFunction returningReaderFunction)
        {
            try
            {
                return await returningReaderFunction();
            }
            catch (NullReaderException nullReaderException)
            {
                throw CreateAndLogValidationException(nullReaderException);
            }
            catch (InvalidReaderException invalidReaderException)
            {
                throw CreateAndLogValidationException(invalidReaderException);
            }
            catch (SqlException sqlException)
            {
                var failedReaderStorageException = new FailedReaderStorageException(sqlException);

                throw CreateAndLogCriticalDependencyException(failedReaderStorageException);
            }
            catch (DuplicateKeyException duplicateKeyException)
            {
                var alreadyExistReaderException =
                    new AlreadyExistReaderException(duplicateKeyException);

                throw CreateAndLogDependencyValidationException(alreadyExistReaderException);
            }
            catch (Exception exception)
            {
                var failedReaderServiceException =
                    new FailedReaderServiceException(exception);

                throw CreateAndLogServiceException(failedReaderServiceException);
            }
        }

        private IQueryable<Reader> TryCatch(ReturningReadersFunction returningReadersFunction)
        {
            try
            {
                return returningReadersFunction();
            }
            catch (SqlException sqlException)
            {
                var failedReaderStorageException =
                    new FailedReaderStorageException(sqlException);

                throw CreateAndLogCriticalDependencyException(failedReaderStorageException);
            }
        }

        private ReaderValidationException CreateAndLogValidationException(Xeption exception)
        {
            var readerValidationException =
                new ReaderValidationException(exception);

            this.loggingBroker.LogError(readerValidationException);

            return readerValidationException;
        }

        private ReaderDependencyException CreateAndLogCriticalDependencyException(Xeption exception)
        {
            var readerDependencyException = new ReaderDependencyException(exception);
            this.loggingBroker.LogCritical(readerDependencyException);

            return readerDependencyException;
        }

        private ReaderDependencyValidationException CreateAndLogDependencyValidationException(
            Xeption exception)
        {
            var readerDependencyValidationException =
                new ReaderDependencyValidationException(exception);

            this.loggingBroker.LogError(readerDependencyValidationException);

            return readerDependencyValidationException;
        }

        private ReaderServiceException CreateAndLogServiceException(Xeption exception)
        {
            var readerServiceException = new ReaderServiceException(exception);
            this.loggingBroker.LogError(readerServiceException);

            return readerServiceException;
        }
    }
}
