//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Bridge Knowledge and Curiosity
//==================================================

using System;
using System.Linq;
using System.Threading.Tasks;
using BookSphere.Api.Models.Foundations.Books;
using BookSphere.Api.Models.Foundations.Books.Exceptions;
using BookSphere.Api.Services.Processings.Books;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;

namespace BookSphere.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : RESTFulController
    {
        private readonly IBookProcessingService bookProcessingService;

        public BooksController(IBookProcessingService bookProcessingService) =>
            this.bookProcessingService = bookProcessingService;

        [HttpPost]
        public async ValueTask<ActionResult<Book>> PostBookAsync(Book book)
        {
            try
            {
                Book postedBook = await this.bookProcessingService.RegisterAndSaveBookAsync(book);

                return Created(postedBook);
            }
            catch (BookValidationException bookValidationException)
            {
                return BadRequest(bookValidationException.InnerException);
            }
            catch (BookDependencyValidationException bookDependencyValidationException)
                when (bookDependencyValidationException.InnerException is AlreadyExistBookException)
            {
                return Conflict(bookDependencyValidationException.InnerException);
            }
            catch (BookDependencyValidationException bookDependencyValidationException)
            {
                return BadRequest(bookDependencyValidationException.InnerException);
            }
            catch (BookDependencyException bookDependencyException)
            {
                return InternalServerError(bookDependencyException.InnerException);
            }
            catch (BookServiceException bookServiceException)
            {
                return InternalServerError(bookServiceException.InnerException);
            }
        }

        [HttpGet("ById")]
        public async ValueTask<ActionResult<Book>> GetBookByIdAsync(Guid bookId)
        {
            try
            {
                return await this.bookProcessingService.RetrieveBookByIdAsync(bookId);
            }
            catch (BookDependencyException bookDependencyException)
            {
                return InternalServerError(bookDependencyException.InnerException);
            }
            catch (BookValidationException bookValidationException)
                when (bookValidationException.InnerException is InvalidBookException)
            {
                return BadRequest(bookValidationException.InnerException);
            }
            catch (BookValidationException bookValidationException)
                when (bookValidationException.InnerException is NotFoundBookException)
            {
                return NotFound(bookValidationException.InnerException);
            }
            catch (BookServiceException bookServiceException)
            {
                return InternalServerError(bookServiceException.InnerException);
            }
        }

        [HttpGet("All")]
        public ActionResult<IQueryable<Book>> GetAllBooks()
        {
            try
            {
                IQueryable<Book> allBooks = this.bookProcessingService.RetrieveAllBooks();

                return Ok(allBooks);
            }
            catch (BookDependencyException bookDependencyException)
            {
                return InternalServerError(bookDependencyException.InnerException);
            }
            catch (BookServiceException bookServiceException)
            {
                return InternalServerError(bookServiceException.InnerException);
            }
        }

        [HttpPut]
        public async ValueTask<ActionResult<Book>> PutBookAsync(Book book)
        {
            try
            {
                Book modifyBook =
                    await this.bookProcessingService.ModifyBookAsync(book);

                return Ok(modifyBook);
            }
            catch (BookValidationException bookValidationException)
                when (bookValidationException.InnerException is NotFoundBookException)
            {
                return NotFound(bookValidationException.InnerException);
            }
            catch (BookValidationException bookValidationException)
            {
                return BadRequest(bookValidationException.InnerException);
            }
            catch (BookDependencyValidationException bookDependencyValidationException)
            {
                return Conflict(bookDependencyValidationException.InnerException);
            }
            catch (BookDependencyException bookDependencyException)
            {
                return InternalServerError(bookDependencyException.InnerException);
            }
            catch (BookServiceException bookServiceException)
            {
                return InternalServerError(bookServiceException.InnerException);
            }
        }

        [HttpDelete]
        public async ValueTask<ActionResult<Book>> DeleteBookAsync(Guid bookId)
        {
            try
            {
                Book deleteBook = await this.bookProcessingService.RemoveBookByIdAsync(bookId);

                return Ok(deleteBook);
            }
            catch (BookValidationException bookValidationException)
                when (bookValidationException.InnerException is NotFoundBookException)
            {
                return NotFound(bookValidationException.InnerException);
            }
            catch (BookValidationException bookValidationException)
            {
                return BadRequest(bookValidationException.InnerException);
            }
            catch (BookDependencyValidationException bookDependencyValidationException)
                when (bookDependencyValidationException.InnerException is LockedBookException)
            {
                return Locked(bookDependencyValidationException.InnerException);
            }
            catch (BookDependencyValidationException bookDependencyValidationException)
            {
                return BadRequest(bookDependencyValidationException.InnerException);
            }
            catch (BookDependencyException bookDependencyException)
            {
                return InternalServerError(bookDependencyException.InnerException);
            }
            catch (BookServiceException bookServiceException)
            {
                return InternalServerError(bookServiceException.InnerException);
            }
        }
    }
}
