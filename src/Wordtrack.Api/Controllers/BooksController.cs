using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
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
        public async Task<ActionResult<List<BookDto>>> GetBooks([FromQuery] int count)
        {
            var books = await service.GetBooks(count);
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

        [HttpPut("{id}/status")]
        public async Task<ActionResult> ChangeReadingStatus(int id, [FromQuery] bool isRead)
        {
            var book = await service.GetBook(id);

            if (book == null)
                return NotFound();

            if (book.isRead == isRead)
                return Conflict($"isRead for book ID {id} is already set to {isRead}");

            book.isRead = isRead;
            await service.EditBook(book);

            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> PatchBook(int id,
            [FromBody] JsonPatchDocument<BookForUpdateDto> patchDocument)
        {
            if (patchDocument == null)
                return BadRequest();

            var book = await service.GetBook(id);

            if (!(book is Book))
                return NotFound();

            var bookToApplyTo = mapper.Map<BookForUpdateDto>(book);

            patchDocument.ApplyTo(bookToApplyTo, ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (bookToApplyTo.Title == bookToApplyTo.Author)
                ModelState.AddModelError(
                    "Title", "Title and Author cannot be the same");

            if (!TryValidateModel(bookToApplyTo))
                return BadRequest(ModelState);

            mapper.Map(bookToApplyTo, book);
            await service.EditBook(book);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBook(int id)
        {
            return await service.RemoveBook(id) ? NoContent() : StatusCode(500);
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
