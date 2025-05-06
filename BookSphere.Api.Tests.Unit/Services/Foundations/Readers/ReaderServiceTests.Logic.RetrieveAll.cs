//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Bridge Knowledge and Curiosity
//==================================================

using System.Linq;
using BookSphere.Api.Models.Foundations.Readers;
using FluentAssertions;
using Force.DeepCloner;
using Moq;

namespace BookSphere.Api.Tests.Unit.Services.Foundations.Readers
{
    public partial class ReaderServiceTests
    {
        [Fact]
        public void ShouldRetrieveAllReaders()
        {
            // given
            IQueryable<Reader> randomReader = CreateRandomReaders();
            IQueryable<Reader> storageReader = randomReader;
            IQueryable<Reader> expectedReader = storageReader.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.SelectAllReaders())
                    .Returns(storageReader);

            // when
            IQueryable<Reader> actualReader =
                this.readerService.RetrieveAllReaders();

            // then
            actualReader.Should().BeEquivalentTo(expectedReader);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectAllReaders(),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }
    }
}
