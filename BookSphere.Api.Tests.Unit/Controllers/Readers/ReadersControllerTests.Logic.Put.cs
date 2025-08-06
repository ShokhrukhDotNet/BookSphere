//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Bridge Knowledge and Curiosity
//==================================================

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
        public async Task ShouldReturnOkOnPutAsync()
        {
            // given
            Reader randomReader = CreateRandomReader();
            Reader inputReader = randomReader;
            Reader storageReader = inputReader.DeepClone();
            Reader expectedReader = storageReader.DeepClone();

            var expectedObjectResult =
                new OkObjectResult(expectedReader);

            var expectedActionResult =
                new ActionResult<Reader>(expectedObjectResult);

            this.readerProcessingServiceMock.Setup(service =>
                service.ModifyReaderAsync(inputReader))
                    .ReturnsAsync(storageReader);

            // when
            ActionResult<Reader> actualActionResult =
                await this.readersController.PutReaderAsync(randomReader);

            // then
            actualActionResult.ShouldBeEquivalentTo(expectedActionResult);

            this.readerProcessingServiceMock.Verify(service =>
                service.ModifyReaderAsync(inputReader),
                    Times.Once);

            this.readerProcessingServiceMock.VerifyNoOtherCalls();
        }
    }
}
