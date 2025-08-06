//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Bridge Knowledge and Curiosity
//==================================================

using System;
using System.Threading.Tasks;
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
        public async Task ShouldRemoveReaderOnDeleteByIdAsync()
        {
            // given
            Reader randomReader = CreateRandomReader();
            Guid inputId = randomReader.ReaderId;
            Reader storageReader = randomReader;
            Reader expectedReader = storageReader.DeepClone();

            var expectedObjectResult =
                new OkObjectResult(expectedReader);

            var expectedActionResult =
                new ActionResult<Reader>(expectedObjectResult);

            this.readerProcessingServiceMock.Setup(service =>
                service.RemoveReaderByIdAsync(It.IsAny<Guid>()))
                    .ReturnsAsync(storageReader);

            // when
            ActionResult<Reader> actualActionResult =
                await this.readersController.DeleteReaderAsync(inputId);

            // then
            actualActionResult.ShouldBeEquivalentTo(expectedActionResult);

            this.readerProcessingServiceMock.Verify(service =>
                service.RemoveReaderByIdAsync(It.IsAny<Guid>()),
                    Times.Once);

            this.readerProcessingServiceMock.VerifyNoOtherCalls();
        }
    }
}
