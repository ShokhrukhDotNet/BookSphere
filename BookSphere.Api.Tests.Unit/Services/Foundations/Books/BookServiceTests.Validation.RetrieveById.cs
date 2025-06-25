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
        public async Task ShouldThrowValidationExceptionOnRetrieveByIdIfIdIsInvalidAndLogItAsync()
        {
            // given
            Guid invalidBookId = Guid.Empty;
            var invalidBookException = new InvalidBookException();

            invalidBookException.AddData(
                key: nameof(Book.BookId),
                values: "Id is required");

            var expectedBookValidationException =
                new BookValidationException(invalidBookException);

            // when
            ValueTask<Book> retrieveBookById =
                this.bookService.RetrieveBookByIdAsync(invalidBookId);

            BookValidationException actualBookValidationException =
                await Assert.ThrowsAsync<BookValidationException>(retrieveBookById.AsTask);

            // then
            actualBookValidationException.Should()
                .BeEquivalentTo(expectedBookValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedBookValidationException))), Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectBookByIdAsync(It.IsAny<Guid>()), Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowValidationExceptionOnRetrieveByIdIfBookNotFoundAndLogItAsync()
        {
            // given
            Guid someBookId = Guid.NewGuid();
            Book noBook = null;

            var notFoundBookException =
                new NotFoundBookException(someBookId);

            var expectedBookValidationException =
                new BookValidationException(notFoundBookException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectBookByIdAsync(
                    It.IsAny<Guid>())).ReturnsAsync(noBook);

            // when
            ValueTask<Book> retriveByIdBookTask =
                this.bookService.RetrieveBookByIdAsync(someBookId);

            var actualBookValidationException =
                await Assert.ThrowsAsync<BookValidationException>(
                    retriveByIdBookTask.AsTask);

            // then
            actualBookValidationException.Should().BeEquivalentTo(expectedBookValidationException);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectBookByIdAsync(someBookId), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedBookValidationException))), Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }
    }
}
