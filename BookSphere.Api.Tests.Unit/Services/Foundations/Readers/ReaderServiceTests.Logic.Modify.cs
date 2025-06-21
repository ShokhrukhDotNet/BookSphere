//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Bridge Knowledge and Curiosity
//==================================================

using System;
using System.Threading.Tasks;
using BookSphere.Api.Models.Foundations.Readers;
using FluentAssertions;
using Force.DeepCloner;
using Moq;

namespace BookSphere.Api.Tests.Unit.Services.Foundations.Readers
{
    public partial class ReaderServiceTests
    {
        [Fact]
        public async Task ShouldModifyReaderAsync()
        {
            // given
            Reader randomReader = CreateRandomReader();
            Reader inputReader = randomReader;
            Reader persistedReader = inputReader.DeepClone();
            Reader updatedReader = inputReader;
            Reader expectedReader = updatedReader.DeepClone();
            Guid InputReaderId = inputReader.Id;

            this.storageBrokerMock.Setup(broker =>
                broker.SelectReaderByIdAsync(InputReaderId))
                    .ReturnsAsync(persistedReader);

            this.storageBrokerMock.Setup(broker =>
                broker.UpdateReaderAsync(inputReader))
                    .ReturnsAsync(updatedReader);

            // when
            Reader actualReader =
                await this.readerService
                    .ModifyReaderAsync(inputReader);

            // then
            actualReader.Should().BeEquivalentTo(expectedReader);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectReaderByIdAsync(InputReaderId), Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.UpdateReaderAsync(inputReader), Times.Once);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
