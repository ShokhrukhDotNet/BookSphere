//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Bridge Knowledge and Curiosity
//==================================================

using System.Linq;
using BookSphere.Api.Models.Foundations.Readers;
using Force.DeepCloner;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RESTFulSense.Clients.Extensions;

namespace BookSphere.Api.Tests.Unit.Controllers.Readers
{
    public partial class ReadersControllerTests
    {
        [Fact]
        public void ShouldReturnOkWithReadersOnGet()
        {
            // given
            IQueryable<Reader> randomReaders =
                CreateRandomReaders();

            IQueryable<Reader> storageReaders =
                randomReaders.DeepClone();

            IQueryable<Reader> expectedReaders =
                storageReaders.DeepClone();

            var expectedObjectResult =
                new OkObjectResult(expectedReaders);

            var expectedActionResult =
                new ActionResult<IQueryable<Reader>>(
                    expectedObjectResult);

            this.readerProcessingServiceMock.Setup(service =>
                service.RetrieveAllReaders())
                    .Returns(storageReaders);

            // when
            ActionResult<IQueryable<Reader>> actualActionResult =
                this.readersController.GetAllReaders();

            // then
            actualActionResult.ShouldBeEquivalentTo(
                expectedActionResult);

            this.readerProcessingServiceMock
                .Verify(service =>
                    service.RetrieveAllReaders(),
                        Times.Once);

            this.readerProcessingServiceMock.VerifyNoOtherCalls();
        }
    }
}
