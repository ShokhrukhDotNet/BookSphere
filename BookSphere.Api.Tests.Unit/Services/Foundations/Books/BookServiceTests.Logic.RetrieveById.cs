//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Bridge Knowledge and Curiosity
//==================================================

using System;
using System.Threading.Tasks;
using BookSphere.Api.Models.Foundations.Books;
using FluentAssertions;
using Force.DeepCloner;
using Moq;

namespace BookSphere.Api.Tests.Unit.Services.Foundations.Books
{
    public partial class BookServiceTests
    {
        [Fact]
        public async Task ShouldRetrieveBookByIdAsync()
        {
            // given
            Guid randomBookId = Guid.NewGuid();
            Guid inputBookId = randomBookId;
            Book randomBook = CreateRandomBook();
            Book persistedBook = randomBook;
            Book expectedBook = persistedBook.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.SelectBookByIdAsync(inputBookId))
                    .ReturnsAsync(persistedBook);

            // when
            Book actualBook = await this
                .bookService.RetrieveBookByIdAsync(inputBookId);

            // then
            actualBook.Should().BeEquivalentTo(expectedBook);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectBookByIdAsync(inputBookId), Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
