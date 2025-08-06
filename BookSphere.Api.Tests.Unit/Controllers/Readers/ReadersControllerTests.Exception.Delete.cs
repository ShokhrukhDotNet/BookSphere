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
        public async Task ShouldReturnBadRequestOnDeleteIfValidationErrorOccurredAsync(Xeption validationException)
        {
            // given
            Guid someId = Guid.NewGuid();

            BadRequestObjectResult expectedBadRequestObjectResult =
                BadRequest(validationException.InnerException);

            var expectedActionResult =
                new ActionResult<Reader>(expectedBadRequestObjectResult);

            this.readerProcessingServiceMock.Setup(service =>
                service.RemoveReaderByIdAsync(It.IsAny<Guid>()))
                    .ThrowsAsync(validationException);

            // when
            ActionResult<Reader> actualActionResult =
                await this.readersController.DeleteReaderAsync(someId);

            // then
            actualActionResult.ShouldBeEquivalentTo(expectedActionResult);

            this.readerProcessingServiceMock.Verify(service =>
                service.RemoveReaderByIdAsync(It.IsAny<Guid>()),
                    Times.Once);

            this.readerProcessingServiceMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(ServerExceptions))]
        public async Task ShouldReturnInternalServerErrorOnDeleteIfServerErrorOccurredAsync(
            Xeption serverException)
        {
            // given
            Guid someId = Guid.NewGuid();

            InternalServerErrorObjectResult expectedInternalServerErrorObjectResult =
                InternalServerError(serverException.InnerException);

            var expectedActionResult =
                new ActionResult<Reader>(expectedInternalServerErrorObjectResult);

            this.readerProcessingServiceMock.Setup(service =>
                service.RemoveReaderByIdAsync(It.IsAny<Guid>()))
                    .ThrowsAsync(serverException);

            // when
            ActionResult<Reader> actualActionResult =
                await this.readersController.DeleteReaderAsync(someId);

            // then
            actualActionResult.ShouldBeEquivalentTo(expectedActionResult);

            this.readerProcessingServiceMock.Verify(service =>
                service.RemoveReaderByIdAsync(It.IsAny<Guid>()),
                    Times.Once);

            this.readerProcessingServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldReturnNotFoundOnDeleteIfItemDoesNotExistAsync()
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
                service.RemoveReaderByIdAsync(It.IsAny<Guid>()))
                    .ThrowsAsync(readerValidationException);

            // when
            ActionResult<Reader> acualActionResult =
                await this.readersController.DeleteReaderAsync(someId);

            // then
            acualActionResult.ShouldBeEquivalentTo(expectedActionResult);

            this.readerProcessingServiceMock.Verify(service =>
                service.RemoveReaderByIdAsync(It.IsAny<Guid>()),
                    Times.Once);

            this.readerProcessingServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldReturnLockedOnDeleteIfRecordIsLockedAsync()
        {
            // given
            Guid someId = Guid.NewGuid();
            var someInnerException = new Exception();

            var lockedReaderException =
                new LockedReaderException(someInnerException);

            var readerDependencyValidationException =
                new ReaderDependencyValidationException(lockedReaderException);

            LockedObjectResult expectedConflictObjectResult =
                Locked(lockedReaderException);

            var expectedActionResult =
                new ActionResult<Reader>(expectedConflictObjectResult);

            this.readerProcessingServiceMock.Setup(service =>
                service.RemoveReaderByIdAsync(It.IsAny<Guid>()))
                    .ThrowsAsync(readerDependencyValidationException);

            // when
            ActionResult<Reader> actualActionResult =
                await this.readersController.DeleteReaderAsync(someId);

            // then
            actualActionResult.ShouldBeEquivalentTo(expectedActionResult);

            this.readerProcessingServiceMock.Verify(service =>
                service.RemoveReaderByIdAsync(It.IsAny<Guid>()),
                    Times.Once);

            this.readerProcessingServiceMock.VerifyNoOtherCalls();
        }
    }
}
