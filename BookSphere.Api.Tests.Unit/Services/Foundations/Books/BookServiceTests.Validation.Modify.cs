//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Bridge Knowledge and Curiosity
//==================================================

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
        public async Task ShouldThrowValidationExceptionOnModifyIfBookIsNullAndLogItAsync()
        {
            // given
            Book nullBook = null;
            var nullBookException = new NullBookException();

            var expectedBookValidationException =
                new BookValidationException(nullBookException);

            // when
            ValueTask<Book> modifyBookTask =
                this.bookService.ModifyBookAsync(nullBook);

            BookValidationException actualBookValidationException =
                await Assert.ThrowsAsync<BookValidationException>(
                    modifyBookTask.AsTask);

            // then
            actualBookValidationException.Should()
                .BeEquivalentTo(expectedBookValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedBookValidationException))), Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.UpdateBookAsync(It.IsAny<Book>()), Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task ShouldThrowValidationExceptionOnModifyIfBookIsInvalidAndLogItAsync(string invalidString)
        {
            // given
            Book invalidBook = new Book
            {
                BookTitle = invalidString
            };

            var invalidBookException =
                new InvalidBookException();

            invalidBookException.AddData(
                key: nameof(Book.BookId),
                values: "Id is required");

            invalidBookException.AddData(
                key: nameof(Book.BookTitle),
                values: "Text is required");

            invalidBookException.AddData(
                key: nameof(Book.Author),
                values: "Text is required");

            invalidBookException.AddData(
                key: nameof(Book.Genre),
                values: "Text is required");

            invalidBookException.AddData(
                key: nameof(Book.ReaderId),
                values: "Id is required");

            var expectedBookValidationException =
                new BookValidationException(invalidBookException);

            // when
            ValueTask<Book> modifyBookTask =
                this.bookService.ModifyBookAsync(invalidBook);

            BookValidationException actualBookValidationException =
                await Assert.ThrowsAsync<BookValidationException>(
                    modifyBookTask.AsTask);

            // then
            actualBookValidationException.Should()
                .BeEquivalentTo(expectedBookValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedBookValidationException))), Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.UpdateBookAsync(It.IsAny<Book>()), Times.Never);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowValidationExceptionOnModifyIfBookDoesNotExistAndLogItAsync()
        {
            // given
            Book randomBook = CreateRandomBook();
            Book nonExistBook = randomBook;
            Book nullBook = null;

            var notFoundBookException =
                new NotFoundBookException(nonExistBook.BookId);

            var expectedBookValidationException =
                new BookValidationException(notFoundBookException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectBookByIdAsync(nonExistBook.BookId))
                    .ReturnsAsync(nullBook);

            // when
            ValueTask<Book> modifyBookTask =
                this.bookService.ModifyBookAsync(nonExistBook);

            BookValidationException actualBookValidationException =
                await Assert.ThrowsAsync<BookValidationException>(
                    modifyBookTask.AsTask);

            // then
            actualBookValidationException.Should()
                .BeEquivalentTo(expectedBookValidationException);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectBookByIdAsync(nonExistBook.BookId), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedBookValidationException))), Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.UpdateBookAsync(nonExistBook), Times.Never);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }
    }
}
