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
        public async Task ShouldReturnBadRequestOnPostIfValidationErrorOccurredAsync(
            Xeption validationException)
        {
            // given
            Reader someReader = CreateRandomReader();

            BadRequestObjectResult expectedBadRequestObjectResult =
                BadRequest(validationException.InnerException);

            var expectedActionResult =
                new ActionResult<Reader>(expectedBadRequestObjectResult);

            this.readerProcessingServiceMock.Setup(service =>
                service.RegisterReaderWithBooksAsync(It.IsAny<Reader>()))
                    .ThrowsAsync(validationException);

            // when
            ActionResult<Reader> actualActionResult =
                await this.readersController.PostReaderAsync(someReader);

            // then
            actualActionResult.ShouldBeEquivalentTo(expectedActionResult);

            this.readerProcessingServiceMock.Verify(service =>
                service.RegisterReaderWithBooksAsync(It.IsAny<Reader>()),
                    Times.Once);

            this.readerProcessingServiceMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(ServerExceptions))]
        public async Task ShouldReturnInternalServerErrorOnPostIfServerErrorOccurredAsync(
            Xeption serverException)
        {
            // given
            Reader someReader = CreateRandomReader();

            InternalServerErrorObjectResult expectedInternalServerErrorObjectResult =
                InternalServerError(serverException.InnerException);

            var expectedActionResult =
                new ActionResult<Reader>(expectedInternalServerErrorObjectResult);

            this.readerProcessingServiceMock.Setup(service =>
                service.RegisterReaderWithBooksAsync(It.IsAny<Reader>()))
                    .ThrowsAsync(serverException);

            // when
            ActionResult<Reader> actualActionResult =
                await this.readersController.PostReaderAsync(someReader);

            // then
            actualActionResult.ShouldBeEquivalentTo(expectedActionResult);

            this.readerProcessingServiceMock.Verify(service =>
                service.RegisterReaderWithBooksAsync(It.IsAny<Reader>()),
                    Times.Once);

            this.readerProcessingServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldReturnConflictOnPostIfAlreadyExistsSourceErrorOccurredAsync()
        {
            // given
            Reader someReader = CreateRandomReader();
            var someInnerException = new Exception();

            var alreadyExistReaderException =
                new AlreadyExistReaderException(
                    innerException: someInnerException);

            var readerDependencyValidationException =
                new ReaderDependencyValidationException(
                    innerException: alreadyExistReaderException);

            ConflictObjectResult expectedConflictObjectResult =
                Conflict(alreadyExistReaderException);

            var expectedActionResult =
                new ActionResult<Reader>(expectedConflictObjectResult);

            this.readerProcessingServiceMock.Setup(service =>
                service.RegisterReaderWithBooksAsync(It.IsAny<Reader>()))
                    .ThrowsAsync(readerDependencyValidationException);

            // when
            ActionResult<Reader> actualActionResult =
                await this.readersController.PostReaderAsync(someReader);

            // then
            actualActionResult.ShouldBeEquivalentTo(expectedActionResult);

            this.readerProcessingServiceMock.Verify(service =>
                service.RegisterReaderWithBooksAsync(It.IsAny<Reader>()),
                    Times.Once);

            this.readerProcessingServiceMock.VerifyNoOtherCalls();
        }
    }
}
