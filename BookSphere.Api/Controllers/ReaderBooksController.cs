//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Bridge Knowledge and Curiosity
//==================================================

using System;
using System.Linq;
using System.Threading.Tasks;
using BookSphere.Api.Models.Foundations.ReaderBooks;
using BookSphere.Api.Models.Foundations.ReaderBooks.Exceptions;
using BookSphere.Api.Models.Foundations.Readers.Exceptions;
using BookSphere.Api.Services.Foundations.ReaderBooks;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;

namespace BookSphere.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReaderBooksController : RESTFulController
    {
        private readonly IReaderBookService readerBookService;

        public ReaderBooksController(IReaderBookService readerBookService)
        {
            this.readerBookService = readerBookService;
        }

        [HttpGet("readerId")]
        public async ValueTask<ActionResult<ReaderBook>> GetReaderBookByIdAsync(Guid readerId)
        {
            try
            {
                return await this.readerBookService.RetrieveReaderBookByIdAsync(readerId);
            }
            catch (ReaderBookDependencyException readerBookDependencyException)
            {
                return InternalServerError(readerBookDependencyException.InnerException);
            }
            catch (ReaderBookValidationException readerBookValidationException)
                when (readerBookValidationException.InnerException is InvalidReaderException)
            {
                return BadRequest(readerBookValidationException.InnerException);
            }
            catch (ReaderBookValidationException readerBookValidationException)
                when (readerBookValidationException.InnerException is NotFoundReaderException)
            {
                return NotFound(readerBookValidationException.InnerException);
            }
            catch (ReaderBookServiceException readerBookServiceException)
            {
                return InternalServerError(readerBookServiceException.InnerException);
            }
        }

        [HttpGet("All")]
        public ActionResult<IQueryable<ReaderBook>> GetAllReaderBooks()
        {
            try
            {
                IQueryable<ReaderBook> allReaderBooks = this.readerBookService.RetrieveAllReaderBooks();

                return Ok(allReaderBooks);
            }
            catch (ReaderBookDependencyException readerBookDependencyException)
            {
                return InternalServerError(readerBookDependencyException.InnerException);
            }
            catch (ReaderBookServiceException readerBookServiceException)
            {
                return InternalServerError(readerBookServiceException.InnerException);
            }
        }
    }
}
