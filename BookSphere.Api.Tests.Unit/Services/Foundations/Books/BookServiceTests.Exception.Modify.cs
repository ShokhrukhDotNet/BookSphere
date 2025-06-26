﻿//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Bridge Knowledge and Curiosity
//==================================================

using System;
using System.Net;
using System.Threading.Tasks;
using BookSphere.Api.Models.Foundations.Books;
using BookSphere.Api.Models.Foundations.Books.Exceptions;
using FluentAssertions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace BookSphere.Api.Tests.Unit.Services.Foundations.Books
{
    public partial class BookServiceTests
    {
        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnModifyIfSqlErrorOccursAndLogItAsync()
        {
            // given
            Book randomBook = CreateRandomBook();
            Book someBook = randomBook;
            Guid bookId = someBook.BookId;
            SqlException sqlException = GetSqlError();

            var failedBookStorageException =
                new FailedBookStorageException(sqlException);

            var expectedBookDependencyException =
                new BookDependencyException(failedBookStorageException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectBookByIdAsync(bookId)).Throws(sqlException);

            // when
            ValueTask<Book> modifyBookTask =
                this.bookService.ModifyBookAsync(someBook);

            BookDependencyException actualBookDependencyException =
                await Assert.ThrowsAsync<BookDependencyException>(
                    modifyBookTask.AsTask);

            // then
            actualBookDependencyException.Should()
                .BeEquivalentTo(expectedBookDependencyException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is(SameExceptionAs(
                    expectedBookDependencyException))), Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectBookByIdAsync(bookId), Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.UpdateBookAsync(someBook), Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyExceptionOnModifyIfDatabaseUpdateExceptionOccursAndLogItAsync()
        {
            // given
            Book randomBook = CreateRandomBook();
            Book someBook = randomBook;
            Guid bookId = someBook.BookId;
            var databaseUpdateException = new DbUpdateException();

            var failedBookStorageException =
                new FailedBookStorageException(databaseUpdateException);

            var expectedBookDependencyException =
                new BookDependencyException(failedBookStorageException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectBookByIdAsync(bookId)).Throws(databaseUpdateException);

            // when
            ValueTask<Book> modifyBookTask =
                this.bookService.ModifyBookAsync(someBook);

            BookDependencyException actualBookDependencyException =
                await Assert.ThrowsAsync<BookDependencyException>(
                    modifyBookTask.AsTask);

            // then
            actualBookDependencyException.Should()
                .BeEquivalentTo(expectedBookDependencyException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedBookDependencyException))), Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectBookByIdAsync(bookId), Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.UpdateBookAsync(someBook), Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }
    }
}
