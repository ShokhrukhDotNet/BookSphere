//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Bridge Knowledge and Curiosity
//==================================================

using System.Threading.Tasks;
using BookSphere.Api.Models.Foundations.Readers;
using BookSphere.Api.Models.Foundations.Readers.Exceptions;
using Xeptions;

namespace BookSphere.Api.Services.Foundations.Readers
{
    public partial class ReaderService
    {
        private delegate ValueTask<Reader> ReturningReaderFunction();

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
        }

        private ReaderValidationException CreateAndLogValidationException(Xeption exception)
        {
            var readerValidationException =
                new ReaderValidationException(exception);

            this.loggingBroker.LogError(readerValidationException);

            return readerValidationException;
        }
    }
}
