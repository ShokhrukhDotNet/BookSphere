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
        public async Task ShouldReturnBadRequestOnPutIfValidationErrorOccurredAsync(Xeption validationException)
        {
            // given
            Reader someReader = CreateRandomReader();

            BadRequestObjectResult expectedBadRequestObjectResult =
                BadRequest(validationException.InnerException);

            var expectedActionResult =
                new ActionResult<Reader>(expectedBadRequestObjectResult);

            this.readerProcessingServiceMock.Setup(service =>
                service.ModifyReaderAsync(It.IsAny<Reader>()))
                    .ThrowsAsync(validationException);

            // when
            ActionResult<Reader> actualActionResult =
                await this.readersController.PutReaderAsync(someReader);

            // then
            actualActionResult.ShouldBeEquivalentTo(expectedActionResult);

            this.readerProcessingServiceMock.Verify(service =>
                service.ModifyReaderAsync(It.IsAny<Reader>()),
                    Times.Once);

            this.readerProcessingServiceMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(ServerExceptions))]
        public async Task ShouldReturnInternalServerErrorOnPutIfServerErrorOccurredAsync(
            Xeption serverException)
        {
            // given
            Reader someReader = CreateRandomReader();

            InternalServerErrorObjectResult expectedInternalServerErrorObjectResult =
                InternalServerError(serverException.InnerException);

            var expectedActionResult =
                new ActionResult<Reader>(expectedInternalServerErrorObjectResult);

            this.readerProcessingServiceMock.Setup(service =>
                service.ModifyReaderAsync(It.IsAny<Reader>()))
                    .ThrowsAsync(serverException);

            // when
            ActionResult<Reader> actualActionResult =
                await this.readersController.PutReaderAsync(someReader);

            // then
            actualActionResult.ShouldBeEquivalentTo(expectedActionResult);

            this.readerProcessingServiceMock.Verify(service =>
                service.ModifyReaderAsync(It.IsAny<Reader>()),
                    Times.Once);

            this.readerProcessingServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldReturnNotFoundOnPutIfItemDoesNotExistAsync()
        {
            // given
            Reader someReader = CreateRandomReader();

            var notFoundReaderException =
                new NotFoundReaderException(someReader.ReaderId);

            var readerValidationException =
                new ReaderValidationException(notFoundReaderException);

            NotFoundObjectResult expectedNotFoundObjectResult =
                NotFound(notFoundReaderException);

            var expectedActionResult =
                new ActionResult<Reader>(expectedNotFoundObjectResult);

            this.readerProcessingServiceMock.Setup(service =>
                service.ModifyReaderAsync(It.IsAny<Reader>()))
                    .ThrowsAsync(readerValidationException);

            // when
            ActionResult<Reader> actualActionResult =
                await this.readersController.PutReaderAsync(someReader);

            // then
            actualActionResult.ShouldBeEquivalentTo(expectedActionResult);

            this.readerProcessingServiceMock.Verify(service =>
                service.ModifyReaderAsync(It.IsAny<Reader>()),
                    Times.Once);

            this.readerProcessingServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldReturnConflictOnPutIfAlreadyExistsReaderErrorOccurredAsync()
        {
            // given
            Reader someReader = CreateRandomReader();
            var someInnerException = new Exception();

            var alreadyExistReaderException =
                new AlreadyExistReaderException(someInnerException);

            var readerDependencyValidationException =
                new ReaderDependencyValidationException(alreadyExistReaderException);

            ConflictObjectResult expectedConflictObjectResult =
                Conflict(alreadyExistReaderException);

            var expectedActionResult =
                new ActionResult<Reader>(expectedConflictObjectResult);

            this.readerProcessingServiceMock.Setup(service =>
                service.ModifyReaderAsync(It.IsAny<Reader>()))
                    .ThrowsAsync(readerDependencyValidationException);

            // when
            ActionResult<Reader> actualActionResult =
                await this.readersController.PutReaderAsync(someReader);

            // then
            actualActionResult.ShouldBeEquivalentTo(expectedActionResult);

            this.readerProcessingServiceMock.Verify(service =>
                service.ModifyReaderAsync(It.IsAny<Reader>()),
                    Times.Once);

            this.readerProcessingServiceMock.VerifyNoOtherCalls();
        }
    }
}
