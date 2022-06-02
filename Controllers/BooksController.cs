using AutoMapper;
using BooksApi.DB;
using BooksApi.DTO;
using BooksApi.Repos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BooksApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BooksController : ControllerBase
    {
        private readonly IBooksRepository _bookRepository;
        private readonly IMapper _mapper;
        private readonly BooksContext _context;

        public BooksController(IBooksRepository bookRepository, IMapper mapper, BooksContext context)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
            _context = context;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllBooks()
        {
            var books = await _bookRepository.GetAllBooksAsync();
            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById([FromRoute] int id)
        {
            //var book = await _bookRepository.GetBookByIdAsync(id);

            //if (book is null)
            //{
            //    return NotFound();
            //}
              
            var book = await _context.Books.FindAsync(id);
            return Ok(_mapper.Map<BookModel>(book));

            //return Ok(book);

        }

        [HttpPost]
        public async Task<IActionResult> AddNewBook([FromBody] BookModel bookModel)
        {
            var id = await _bookRepository.AddBookAsync(bookModel);
            //code 201
            //body 
            //headers : location "https://fffffff/"
            return CreatedAtAction(nameof(GetBookById), new { id = id, controller = "books" }, id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook([FromRoute] int id, [FromBody] BookModel bookModel)
        {
            await _bookRepository.UpdateBookAsync(id, bookModel);
            return Ok();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateBookPatch([FromRoute] int id, [FromBody] JsonPatchDocument bookModel)
        {
            await _bookRepository.UpdateBookAsync(id, bookModel);
            return Ok();

        }

        //public class SomeModelForPatch
        //{
        //    public string Title { get; set; }
        //    public int? Price { get; set; }
        //}

        //[HttpPatch("{id}")]
        //public async Task<IActionResult> UpdateBookPatch([FromRoute] int id, [FromBody] SomeModelForPatch bookFromRequest)
        //{

        //    var bookFromDb = await _context.Books.FindAsync(id);
        //    if (bookFromRequest.Title != null)
        //    {
        //        bookFromDb.Title = bookFromRequest.Title;
        //    }
        //    if (bookFromRequest.Price != null)
        //    {
        //        bookFromDb.Price = bookFromRequest.Price;
        //    }

        //    _context.SaveChangesAsync();
        //    return Ok();

        //}

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook([FromRoute] int id)
        {
            await _bookRepository.DeleteBookAsync(id);

            return Ok();
        }


    }
}
