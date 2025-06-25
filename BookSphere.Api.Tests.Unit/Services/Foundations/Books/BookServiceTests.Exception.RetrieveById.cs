//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Bridge Knowledge and Curiosity
//==================================================

using System;
using System.Threading.Tasks;
using BookSphere.Api.Models.Foundations.Books;
using BookSphere.Api.Models.Foundations.Books.Exceptions;
using FluentAssertions;
using Microsoft.Data.SqlClient;
using Moq;

namespace BookSphere.Api.Tests.Unit.Services.Foundations.Books
{
    public partial class BookServiceTests
    {
        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnRetrieveByIdIfSqlErrorOccursAndLogItAsync()
        {
            // given
            Guid someId = Guid.NewGuid();
            SqlException sqlException = GetSqlError();

            var failedBookStorageException =
                new FailedBookStorageException(sqlException);

            BookDependencyException expectedBookDependencyException =
                new BookDependencyException(failedBookStorageException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectBookByIdAsync(It.IsAny<Guid>()))
                    .ThrowsAsync(sqlException);

            // when
            ValueTask<Book> retrieveBookById =
                this.bookService.RetrieveBookByIdAsync(someId);

            BookDependencyException actualBookDependencyException =
                await Assert.ThrowsAsync<BookDependencyException>(
                    retrieveBookById.AsTask);

            // then
            actualBookDependencyException.Should()
                .BeEquivalentTo(expectedBookDependencyException);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectBookByIdAsync(someId), Times.Once());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is(SameExceptionAs(
                    expectedBookDependencyException))), Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
