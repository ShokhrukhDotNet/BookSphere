//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Bridge Knowledge and Curiosity
//==================================================

using System;
using System.Threading.Tasks;
using BookSphere.Api.Models.Foundations.Readers;
using BookSphere.Api.Models.Foundations.Readers.Exceptions;
using FluentAssertions;
using Microsoft.Data.SqlClient;
using Moq;

namespace BookSphere.Api.Tests.Unit.Services.Foundations.Readers
{
    public partial class ReaderServiceTests
    {
        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnModifyIfSqlErrorOccursAndLogItAsync()
        {
            // given
            Reader randomReader = CreateRandomReader();
            Reader someReader = randomReader;
            Guid readerId = someReader.Id;
            SqlException sqlException = GetSqlError();

            var failedReaderStorageException =
                new FailedReaderStorageException(sqlException);

            var expectedReaderDependencyException =
                new ReaderDependencyException(failedReaderStorageException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectReaderByIdAsync(readerId)).Throws(sqlException);

            // when
            ValueTask<Reader> modifyReaderTask =
                this.readerService.ModifyReaderAsync(someReader);

            ReaderDependencyException actualReaderDependencyException =
                await Assert.ThrowsAsync<ReaderDependencyException>(
                    modifyReaderTask.AsTask);

            // then
            actualReaderDependencyException.Should()
                .BeEquivalentTo(expectedReaderDependencyException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is(SameExceptionAs(
                    expectedReaderDependencyException))), Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectReaderByIdAsync(readerId), Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.UpdateReaderAsync(someReader), Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }
    }
}
