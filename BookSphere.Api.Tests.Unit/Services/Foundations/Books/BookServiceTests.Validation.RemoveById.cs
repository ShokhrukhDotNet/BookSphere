﻿//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Bridge Knowledge and Curiosity
//==================================================

using System;
using System.Threading.Tasks;
using BookSphere.Api.Models.Foundations.Books;
using BookSphere.Api.Models.Foundations.Books.Exceptions;
using FluentAssertions;
using Moq;

namespace BookSphere.Api.Tests.Unit.Services.Foundations.Books
{
    public partial class BookServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnRemoveIfIdIsInvalidAndLogItAsync()
        {
            // given
            Guid invalidBookId = Guid.Empty;

            var invalidBookException =
                new InvalidBookException();

            invalidBookException.AddData(
                key: nameof(Book.BookId),
                values: "Id is required");

            BookValidationException expectedBookValidationException =
                new BookValidationException(invalidBookException);

            // when
            ValueTask<Book> removeBookById =
                this.bookService.RemoveBookByIdAsync(invalidBookId);

            BookValidationException actualBookValidationException =
                await Assert.ThrowsAsync<BookValidationException>(
                    removeBookById.AsTask);

            // then
            actualBookValidationException.Should()
                .BeEquivalentTo(expectedBookValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedBookValidationException))), Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectBookByIdAsync(It.IsAny<Guid>()), Times.Never);

            this.storageBrokerMock.Verify(broker =>
                broker.DeleteBookAsync(It.IsAny<Book>()), Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }
    }
}
