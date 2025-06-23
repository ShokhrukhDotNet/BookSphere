//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Bridge Knowledge and Curiosity
//==================================================

using System;
using System.Threading.Tasks;
using BookSphere.Api.Models.Foundations.Readers;
using FluentAssertions;
using Moq;

namespace BookSphere.Api.Tests.Unit.Services.Foundations.Readers
{
    public partial class ReaderServiceTests
    {
        [Fact]
        public async Task ShouldRemoveReaderById()
        {
            // given
            Guid randomId = Guid.NewGuid();
            Guid inputReaderId = randomId;
            Reader randomReader = CreateRandomReader();
            Reader storageReader = randomReader;
            Reader expectedInputReader = storageReader;
            Reader deletedReader = expectedInputReader;
            Reader expectedReader = deletedReader;

            this.storageBrokerMock.Setup(broker =>
                broker.SelectReaderByIdAsync(inputReaderId))
                    .ReturnsAsync(storageReader);

            this.storageBrokerMock.Setup(broker =>
                broker.DeleteReaderAsync(expectedInputReader))
                    .ReturnsAsync(deletedReader);

            // when
            Reader actualReader = await this
                .readerService.RemoveReaderByIdAsync(randomId);

            // then
            actualReader.Should().BeEquivalentTo(expectedReader);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectReaderByIdAsync(inputReaderId), Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.DeleteReaderAsync(expectedInputReader),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }
    }
}
