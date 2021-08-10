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

        public BooksController(IBookService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<BookDto>>> GetAllBooks()
        {
            var books = await service.GetAllBooks();
            return Ok(books);
        }

        [HttpGet("{id}", Name = "GetBook")]
        public async Task<ActionResult<BookDto>> GetBook(int id)
        {
            var book = await service.GetBook(id);

            if (book == null)
                return NotFound();

            var bookDto = new BookDto()
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                YearPublished = book.YearPublished,
                Pages = book.Pages
            };

            return Ok(bookDto);
        }

        [HttpPost]
        public async Task<ActionResult> CreateBook([FromBody] BookForCreationDto bookDto)
        {
            if (bookDto == null)
                return BadRequest();

            if (bookDto.Title == bookDto.Author)
                ModelState.AddModelError(
                    "Title", "Title and Author cannot be the same");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var book = new Book()
            {
                Title = bookDto.Title,
                Author = bookDto.Author,
                YearPublished = bookDto.YearPublished,
                Pages = bookDto.Pages
            };

            await service.AddBook(book);

            return CreatedAtAction("GetBook", new { book.Id }, book);
        }
    }
}
