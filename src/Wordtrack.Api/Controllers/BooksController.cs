using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Wordtrack.Api.Dtos;
using Wordtrack.Api.Services;
using Wordtrack.Domain;

namespace Wordtrack.Api.Controllers
{
    [ApiController]
    [Route("api/books")]
    public class BooksController : ControllerBase
    {
        private readonly IBookService service;
        private readonly IMapper mapper;

        public BooksController(IBookService service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<BookDto>>> GetAllBooks()
        {
            var books = await service.GetAllBooks();
            var results = mapper.Map<List<BookDto>>(books);

            return Ok(results);
        }

        [HttpGet("{id}", Name = "GetBook")]
        public async Task<ActionResult<BookDto>> GetBook(int id) =>
            await service.GetBook(id)
                is Book book
                    ? Ok(mapper.Map<BookDto>(book))
                    : NotFound();

        [HttpPost]
        public async Task<ActionResult> CreateBook([FromBody] BookForCreationDto bookDto)
        {
            if (bookDto == null)
                return BadRequest();

            if (AreTitleAndAuthorEqual(bookDto))
            {
                ModelState.AddModelError(
                    "Title", "Title and Author cannot be the same");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var book = mapper.Map<Book>(bookDto);

            await service.AddBook(book);

            return CreatedAtAction("GetBook", new { book.Id }, book);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateBook(int id, [FromBody] BookForUpdateDto bookDto)
        {
            if (bookDto == null)
                return BadRequest();

            if (AreTitleAndAuthorEqual(bookDto))
            {
                ModelState.AddModelError(
                    "Title", "Title and Author cannot be the same");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var book = await service.GetBook(id);

            if (!(book is Book))
                return NotFound("Book ID not found");

            mapper.Map(bookDto, book);
            await service.EditBook(book);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBook(int id)
        {
            return await service.RemoveBook(id) ? NoContent() : StatusCode(500);
        }

        [HttpPut("{id}/{status}")]
        public async Task<ActionResult> ChangeReadingStatus(int id, bool status)
        {
            var book = await service.GetBook(id);

            if (book == null)
                return NotFound();

            if (book.isRead == status)
                return Conflict($"Read status for book ID {id} is already set to {status}");

            book.isRead = status;
            await service.EditBook(book);

            return NoContent();
        }

        private bool AreTitleAndAuthorEqual(BookForCreationDto dto)
        {
            return dto.Title == dto.Author;
        }

        private bool AreTitleAndAuthorEqual(BookForUpdateDto dto)
        {
            return dto.Title == dto.Author;
        }
    }
}
