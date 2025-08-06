//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Bridge Knowledge and Curiosity
//==================================================

using System.Linq;
using BookSphere.Api.Models.Foundations.Readers;
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
        [MemberData(nameof(ServerExceptions))]
        public void ShouldReturnInternalServerErrorOnGetIfServerErrorOccurred(
            Xeption serverException)
        {
            // given
            InternalServerErrorObjectResult expectedInternalServerErrorObjectResult =
                InternalServerError(serverException.InnerException);

            var expectedActionResult =
                new ActionResult<IQueryable<Reader>>(
                    expectedInternalServerErrorObjectResult);

            this.readerProcessingServiceMock.Setup(service =>
                service.RetrieveAllReaders())
                    .Throws(serverException);

            // when
            ActionResult<IQueryable<Reader>> actualActionResult =
                this.readersController.GetAllReaders();

            // then
            actualActionResult.ShouldBeEquivalentTo(expectedActionResult);

            this.readerProcessingServiceMock.Verify(service =>
                service.RetrieveAllReaders(),
                    Times.Once);

            this.readerProcessingServiceMock.VerifyNoOtherCalls();
        }
    }
}
