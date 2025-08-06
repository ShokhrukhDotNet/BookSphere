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
using RESTFulSense.Models;

namespace BookSphere.Api.Tests.Unit.Controllers.Readers
{
    public partial class ReadersControllerTests
    {
        [Fact]
        public async Task ShouldReturnCreatedOnPostAsync()
        {
            // given
            Reader randomReader = CreateRandomReader();
            Reader inputReader = randomReader;
            Reader addedReader = inputReader;
            Reader expectedReader = addedReader.DeepClone();

            var expectedObjectResult =
                new CreatedObjectResult(expectedReader);

            var expectedActionResult =
                new ActionResult<Reader>(expectedObjectResult);

            this.readerProcessingServiceMock.Setup(service =>
                service.RegisterReaderWithBooksAsync(inputReader))
                    .ReturnsAsync(addedReader);

            // when
            ActionResult<Reader> actualActionResult =
                await this.readersController.PostReaderAsync(
                    inputReader);

            // then
            actualActionResult.ShouldBeEquivalentTo(
                expectedActionResult);

            this.readerProcessingServiceMock.Verify(service =>
                service.RegisterReaderWithBooksAsync(inputReader),
                    Times.Once);

            this.readerProcessingServiceMock.VerifyNoOtherCalls();
        }
    }
}
