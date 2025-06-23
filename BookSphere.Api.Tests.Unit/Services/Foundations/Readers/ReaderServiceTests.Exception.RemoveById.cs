//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Bridge Knowledge and Curiosity
//==================================================

using System;
using System.Threading.Tasks;
using BookSphere.Api.Models.Foundations.Readers;
using BookSphere.Api.Models.Foundations.Readers.Exceptions;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace BookSphere.Api.Tests.Unit.Services.Foundations.Readers
{
    public partial class ReaderServiceTests
    {
        [Fact]
        public async Task ShouldThrowDependencyValidationOnRemoveIfDatabaseUpdateConcurrencyErrorOccursAndLogItAsync()
        {
            // given
            Guid someReaderId = Guid.NewGuid();
            var dbUpdateConcurrencyException = new DbUpdateConcurrencyException();

            LockedReaderException lockedReaderException =
                new LockedReaderException(dbUpdateConcurrencyException);

            var expectedReaderDependencyValidationException =
                new ReaderDependencyValidationException(lockedReaderException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectReaderByIdAsync(It.IsAny<Guid>()))
                    .ThrowsAsync(dbUpdateConcurrencyException);

            // when
            ValueTask<Reader> removeReaderById =
                this.readerService.RemoveReaderByIdAsync(someReaderId);

            var actualReaderDependencyValidationException =
                await Assert.ThrowsAsync<ReaderDependencyValidationException>(
                    removeReaderById.AsTask);

            // then
            actualReaderDependencyValidationException.Should()
                .BeEquivalentTo(expectedReaderDependencyValidationException);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectReaderByIdAsync(It.IsAny<Guid>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedReaderDependencyValidationException))), Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.DeleteReaderAsync(It.IsAny<Reader>()), Times.Never);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }
    }
}
