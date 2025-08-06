//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Bridge Knowledge and Curiosity
//==================================================

using System;
using System.Threading.Tasks;
using BookSphere.Api.Models.Foundations.Readers;
using BookSphere.Api.Models.Foundations.Readers.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RESTFulSense.Clients.Extensions;
using RESTFulSense.Models;
using Xeptions;

namespace BookSphere.Api.Tests.Unit.Controllers.Readers
{
    public partial class ReadersControllerTests
    {
        [Theory]
        [MemberData(nameof(ValidationExceptions))]
        public async Task ShouldReturnBadRequestOnGetByIdIfValidationErrorOccurredAsync(Xeption validationException)
        {
            // given
            Guid someId = Guid.NewGuid();

            BadRequestObjectResult expectedBadRequestObjectResult =
                BadRequest(validationException.InnerException);

            var expectedActionResult =
                new ActionResult<Reader>(expectedBadRequestObjectResult);

            this.readerProcessingServiceMock.Setup(service =>
                service.RetrieveReaderByIdAsync(It.IsAny<Guid>()))
                    .ThrowsAsync(validationException);

            // when
            ActionResult<Reader> actualActionResult =
                await this.readersController.GetReaderByIdAsync(someId);

            // then
            actualActionResult.ShouldBeEquivalentTo(expectedActionResult);

            this.readerProcessingServiceMock.Verify(service =>
                service.RetrieveReaderByIdAsync(It.IsAny<Guid>()),
                    Times.Once);

            this.readerProcessingServiceMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(ServerExceptions))]
        public async Task ShouldReturnInternalServerErrorOnGetByIdIfServerErrorOccurredAsync(
            Xeption serverException)
        {
            // given
            Guid someId = Guid.NewGuid();

            InternalServerErrorObjectResult expectedInternalServerErrorObjectResult =
                InternalServerError(serverException.InnerException);

            var expectedActionResult =
                new ActionResult<Reader>(expectedInternalServerErrorObjectResult);

            this.readerProcessingServiceMock.Setup(service =>
                service.RetrieveReaderByIdAsync(It.IsAny<Guid>()))
                    .ThrowsAsync(serverException);

            // when
            ActionResult<Reader> actualActionResult =
                await this.readersController.GetReaderByIdAsync(someId);

            // then
            actualActionResult.ShouldBeEquivalentTo(expectedActionResult);

            this.readerProcessingServiceMock.Verify(service =>
                service.RetrieveReaderByIdAsync(It.IsAny<Guid>()),
                    Times.Once);

            this.readerProcessingServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldReturnNotFoundOnGetByIdIfItemDoesNotExistAsync()
        {
            // given
            Guid someId = Guid.NewGuid();

            var notFoundReaderException =
                new NotFoundReaderException(someId);

            var readerValidationException =
                new ReaderValidationException(notFoundReaderException);

            NotFoundObjectResult expectedNotFoundObjectResult =
                NotFound(notFoundReaderException);

            var expectedActionResult =
                new ActionResult<Reader>(expectedNotFoundObjectResult);

            this.readerProcessingServiceMock.Setup(service =>
                service.RetrieveReaderByIdAsync(It.IsAny<Guid>()))
                    .ThrowsAsync(readerValidationException);

            // when
            ActionResult<Reader> actualActionResult =
                await this.readersController.GetReaderByIdAsync(someId);

            // then
            actualActionResult.ShouldBeEquivalentTo(expectedActionResult);

            this.readerProcessingServiceMock.Verify(service =>
                service.RetrieveReaderByIdAsync(It.IsAny<Guid>()),
                    Times.Once);

            this.readerProcessingServiceMock.VerifyNoOtherCalls();
        }
    }
}
