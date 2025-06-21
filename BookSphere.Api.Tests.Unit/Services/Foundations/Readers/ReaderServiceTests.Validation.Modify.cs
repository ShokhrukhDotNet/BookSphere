//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Bridge Knowledge and Curiosity
//==================================================

using System.Threading.Tasks;
using BookSphere.Api.Models.Foundations.Readers;
using BookSphere.Api.Models.Foundations.Readers.Exceptions;
using FluentAssertions;
using Moq;

namespace BookSphere.Api.Tests.Unit.Services.Foundations.Readers
{
    public partial class ReaderServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnModifyIfReaderIsNullAndLogItAsync()
        {
            // given
            Reader nullReader = null;
            var nullReaderException = new NullReaderException();

            var expectedReaderValidationException =
                new ReaderValidationException(nullReaderException);

            // when
            ValueTask<Reader> modifyReaderTask =
                this.readerService.ModifyReaderAsync(nullReader);

            ReaderValidationException actualReaderValidationException =
                await Assert.ThrowsAsync<ReaderValidationException>(
                    modifyReaderTask.AsTask);

            // then
            actualReaderValidationException.Should()
                .BeEquivalentTo(expectedReaderValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedReaderValidationException))), Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.UpdateReaderAsync(It.IsAny<Reader>()), Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task ShouldThrowValidationExceptionOnModifyIfReaderIsInvalidAndLogItAsync(string invalidString)
        {
            // given
            Reader invalidReader = new Reader
            {
                FirstName = invalidString
            };

            var invalidReaderException =
                new InvalidReaderException();

            invalidReaderException.AddData(
                key: nameof(Reader.Id),
                values: "Id is required");

            invalidReaderException.AddData(
                key: nameof(Reader.FirstName),
                values: "Text is required");

            invalidReaderException.AddData(
                key: nameof(Reader.LastName),
                values: "Text is required");

            invalidReaderException.AddData(
                key: nameof(Reader.DateOfBirth),
                values: "Date is required");

            var expectedReaderValidationException =
                new ReaderValidationException(invalidReaderException);

            // when
            ValueTask<Reader> modifyReaderTask =
                this.readerService.ModifyReaderAsync(invalidReader);

            ReaderValidationException actualReaderValidationException =
                await Assert.ThrowsAsync<ReaderValidationException>(
                    modifyReaderTask.AsTask);

            // then
            actualReaderValidationException.Should()
                .BeEquivalentTo(expectedReaderValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedReaderValidationException))), Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.UpdateReaderAsync(It.IsAny<Reader>()), Times.Never);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
