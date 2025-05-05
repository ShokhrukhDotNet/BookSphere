//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Bridge Knowledge and Curiosity
//==================================================

using System.Threading.Tasks;
using BookSphere.Api.Models.Foundations.Readers;
using BookSphere.Api.Models.Foundations.Readers.Exceptions;
using BookSphere.Api.Services.Foundations.Readers;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;

namespace BookSphere.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReadersController : RESTFulController
    {
        private readonly IReaderService readerService;

        public ReadersController(IReaderService readerService)
        {
            this.readerService = readerService;
        }

        [HttpPost]
        public async ValueTask<ActionResult<Reader>> PostReaderAsync(Reader reader)
        {
            try
            {
                Reader postedReader = await this.readerService.AddReaderAsync(reader);

                return Created(postedReader);
            }
            catch (ReaderValidationException readerValidationException)
            {
                return BadRequest(readerValidationException.InnerException);
            }
            catch (ReaderDependencyValidationException readerDependencyValidationException)
                when (readerDependencyValidationException.InnerException is AlreadyExistReaderException)
            {
                return Conflict(readerDependencyValidationException.InnerException);
            }
            catch (ReaderDependencyValidationException readerDependencyValidationException)
            {
                return BadRequest(readerDependencyValidationException.InnerException);
            }
            catch (ReaderDependencyException readerDependencyException)
            {
                return InternalServerError(readerDependencyException.InnerException);
            }
            catch (ReaderServiceException readerServiceException)
            {
                return InternalServerError(readerServiceException.InnerException);
            }
        }
    }
}
