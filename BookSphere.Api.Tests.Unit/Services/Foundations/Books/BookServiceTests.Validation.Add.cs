//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Bridge Knowledge and Curiosity
//==================================================

using System.Threading.Tasks;
using BookSphere.Api.Models.Foundations.Books;
using BookSphere.Api.Models.Foundations.Books.Exceptions;
using Moq;

namespace BookSphere.Api.Tests.Unit.Services.Foundations.Books
{
    public partial class BookServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfBookIsNullAndLogItAsync()
        {
            // given
            Book nullBook = null;
            var nullBookException = new NullBookException();

            var expectedBookValidationException =
                new BookValidationException(nullBookException);

            // when
            ValueTask<Book> addBookTask =
                this.bookService.AddBookAsync(nullBook);

            // then
            await Assert.ThrowsAsync<BookValidationException>(() =>
                addBookTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedBookValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertBookAsync(It.IsAny<Book>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task ShouldThrowValidationExceptionOnAddIfBookIsInvalidAndLogItAsync(
            string invalidText)
        {
            // given
            var invalidBook = new Book
            {
                BookTitle = invalidText
            };

            var invalidBookException = new InvalidBookException();

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
            ValueTask<Book> addBookTask =
                this.bookService.AddBookAsync(invalidBook);

            // then
            await Assert.ThrowsAsync<BookValidationException>(() =>
                addBookTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedBookValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertBookAsync(It.IsAny<Book>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
