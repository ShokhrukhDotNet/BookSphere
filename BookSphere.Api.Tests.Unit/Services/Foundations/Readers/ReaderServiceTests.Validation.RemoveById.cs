﻿//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Bridge Knowledge and Curiosity
//==================================================

using System;
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
        public async Task ShouldThrowValidationExceptionOnRemoveIfIdIsInvalidAndLogItAsync()
        {
            // given
            Guid invalidReaderId = Guid.Empty;

            var invalidReaderException =
                new InvalidReaderException();

            invalidReaderException.AddData(
                key: nameof(Reader.Id),
                values: "Id is required");

            ReaderValidationException expectedReaderValidationException =
                new ReaderValidationException(invalidReaderException);

            // when
            ValueTask<Reader> removeReaderById =
                this.readerService.RemoveReaderByIdAsync(invalidReaderId);

            ReaderValidationException actualReaderValidationException =
                await Assert.ThrowsAsync<ReaderValidationException>(
                    removeReaderById.AsTask);

            // then
            actualReaderValidationException.Should()
                .BeEquivalentTo(expectedReaderValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedReaderValidationException))), Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectReaderByIdAsync(It.IsAny<Guid>()), Times.Never);

            this.storageBrokerMock.Verify(broker =>
                broker.DeleteReaderAsync(It.IsAny<Reader>()), Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowNotFoundExceptionOnRemoveReaderByIdIsNotFoundAndLogItAsync()
        {
            // given
            Guid inputReaderId = Guid.NewGuid();
            Reader noReader = null;

            var notFoundReaderException =
                new NotFoundReaderException(inputReaderId);

            var expectedReaderValidationException =
                new ReaderValidationException(notFoundReaderException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectReaderByIdAsync(It.IsAny<Guid>()))
                    .ReturnsAsync(noReader);

            // when
            ValueTask<Reader> removeReaderById =
                this.readerService.RemoveReaderByIdAsync(inputReaderId);

            var actualReaderValidationException =
                await Assert.ThrowsAsync<ReaderValidationException>(
                    removeReaderById.AsTask);

            // then
            actualReaderValidationException.Should()
                .BeEquivalentTo(expectedReaderValidationException);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectReaderByIdAsync(It.IsAny<Guid>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedReaderValidationException))), Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.DeleteReaderAsync(It.IsAny<Reader>()), Times.Never);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }
    }
}
