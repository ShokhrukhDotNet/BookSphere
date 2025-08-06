//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Bridge Knowledge and Curiosity
//==================================================

using System;
using System.Linq;
using System.Threading.Tasks;
using BookSphere.Api.Models.Foundations.Readers;
using BookSphere.Api.Models.Foundations.Readers.Exceptions;
using BookSphere.Api.Services.Processings.Readers;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;

namespace BookSphere.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReadersController : RESTFulController
    {
        private readonly IReaderProcessingService readerProcessingService;

        public ReadersController(IReaderProcessingService readerProcessingService) =>
            this.readerProcessingService = readerProcessingService;

        [HttpPost]
        public async ValueTask<ActionResult<Reader>> PostReaderAsync(Reader reader)
        {
            try
            {
                Reader postedReader = await this.readerProcessingService.RegisterReaderWithBooksAsync(reader);

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

        [HttpGet("ById")]
        public async ValueTask<ActionResult<Reader>> GetReaderByIdAsync(Guid readerId)
        {
            try
            {
                Reader reader =
                    await this.readerProcessingService.RetrieveReaderByIdAsync(readerId);

                return Ok(reader);
            }
            catch (ReaderValidationException readerValidationException)
                when (readerValidationException.InnerException is NotFoundReaderException)
            {
                return NotFound(readerValidationException.InnerException);
            }
            catch (ReaderValidationException readerValidationException)
                when (readerValidationException.InnerException is InvalidReaderException)
            {
                return BadRequest(readerValidationException.InnerException);
            }
            catch (ReaderValidationException readerValidationException)
            {
                return BadRequest(readerValidationException.InnerException);
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

        [HttpGet("All")]
        public ActionResult<IQueryable<Reader>> GetAllReaders()
        {
            try
            {
                IQueryable<Reader> allReaders = this.readerProcessingService.RetrieveAllReaders();

                return Ok(allReaders);
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

        [HttpPut]
        public async ValueTask<ActionResult<Reader>> PutReaderAsync(Reader reader)
        {
            try
            {
                Reader modifyReader =
                    await this.readerProcessingService.ModifyReaderAsync(reader);

                return Ok(modifyReader);
            }
            catch (ReaderValidationException readerValidationException)
                when (readerValidationException.InnerException is NotFoundReaderException)
            {
                return NotFound(readerValidationException.InnerException);
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

        [HttpDelete]
        public async ValueTask<ActionResult<Reader>> DeleteReaderAsync(Guid readerId)
        {
            try
            {
                Reader deleteReader = await this.readerProcessingService.RemoveReaderByIdAsync(readerId);

                return Ok(deleteReader);
            }
            catch (ReaderValidationException readerValidationException)
                when (readerValidationException.InnerException is NotFoundReaderException)
            {
                return NotFound(readerValidationException.InnerException);
            }
            catch (ReaderValidationException readerValidationException)
            {
                return BadRequest(readerValidationException.InnerException);
            }
            catch (ReaderDependencyValidationException readerDependencyValidationException)
                when (readerDependencyValidationException.InnerException is LockedReaderException)
            {
                return Locked(readerDependencyValidationException.InnerException);
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
