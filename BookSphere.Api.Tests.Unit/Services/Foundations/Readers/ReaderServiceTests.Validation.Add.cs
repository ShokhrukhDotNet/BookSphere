//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Bridge Knowledge and Curiosity
//==================================================

using BookSphere.Api.Models.Foundations.Readers;
using BookSphere.Api.Models.Foundations.Readers.Exceptions;
using Moq;
using System.Threading.Tasks;

namespace BookSphere.Api.Tests.Unit.Services.Foundations.Readers
{
    public partial class ReaderServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfReaderIsNullAndLogItAsync()
        {
            // given
            Reader nullReader = null;
            var nullReaderException = new NullReaderException();

            var expectedReaderValidationException =
                new ReaderValidationException(nullReaderException);

            // when
            ValueTask<Reader> addReaderTask =
                this.readerService.AddReaderAsync(nullReader);

            // then
            await Assert.ThrowsAsync<ReaderValidationException>(() =>
                addReaderTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(expectedReaderValidationException))),
                    Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertReaderAsync(It.IsAny<Reader>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
