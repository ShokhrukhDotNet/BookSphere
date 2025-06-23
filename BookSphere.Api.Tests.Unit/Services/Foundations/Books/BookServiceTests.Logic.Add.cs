﻿//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Bridge Knowledge and Curiosity
//==================================================

using System.Threading.Tasks;
using BookSphere.Api.Models.Foundations.Books;
using FluentAssertions;
using Moq;

namespace BookSphere.Api.Tests.Unit.Services.Foundations.Books
{
    public partial class BookServiceTests
    {
        [Fact]
        public async Task ShouldAddBookAsync()
        {
            // given
            Book randomBook = CreateRandomBook();
            Book inputBook = randomBook;
            Book storageBook = inputBook;
            Book expectedBook = storageBook;

            this.storageBrokerMock.Setup(broker =>
                broker.InsertBookAsync(inputBook))
                    .ReturnsAsync(storageBook);

            // when
            Book actualBook =
                await this.bookService.AddBookAsync(inputBook);

            // then
            actualBook.Should().BeEquivalentTo(expectedBook);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertBookAsync(inputBook),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
