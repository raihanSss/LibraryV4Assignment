using LibraryV4.Domain.Interfaces;
using LibraryV4.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryV4.API.Controllers
{
    [Route("api/buku")]
    [ApiController]
    public class BookController : ControllerBase
    {

        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Book>> GetAllBooks()
        {
            var books = _bookService.GetAllBook();
            return Ok(books);
        }

        [HttpGet("{id}")]
        public ActionResult<Book> GetBookById(int id)
        {
            var book = _bookService.GetBookById(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        [HttpPost]
        public ActionResult<string> AddBook(Book book)
        {
            var result = _bookService.AddBook(book);
            return CreatedAtAction(nameof(GetBookById), new { id = book.Id }, result);
        }

        [HttpPut("{id}")]
        public ActionResult<string> UpdateBook(int id, Book book)
        {
            
            var existingBook = _bookService.GetBookById(id);
            if (existingBook == null)
            {
                return NotFound("Buku tidak ditemukan");
            }

            var result = _bookService.UpdateBook(book, id);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public ActionResult<string> DeleteBook(int id)
        {
            var result = _bookService.DeleteBook(id);
            if (result.Contains("Buku tidak ditemukan"))
            {
                return NotFound(result);
            }
            return Ok(result);
        }

        [HttpPost("borrow")]
        public ActionResult BorrowBooks([FromBody] BorrowRequest request)
        {
            var results = new List<string>();

            foreach (var bookId in request.BookIds)
            {
                var result = _bookService.BorrowBook(bookId, request.UserId);
                results.Add(result);

                if (result.Contains("batas maksimum") || result.Contains("tidak tersedia"))
                {
                    return BadRequest(result); 
                }
            }

            return Ok(results);
        }


        [HttpPost("{id}/return")]
        public ActionResult ReturnBook(int id, int userId)
        {
            var result = _bookService.ReturnBook(id, userId);
            if (result.Contains("tidak ditemukan") || result.Contains("sudah dikembalikan"))
            {
                return BadRequest(result); 
            }
            return Ok(result);
        }

        public class BorrowRequest
        {
            public int UserId { get; set; }
            public List<int> BookIds { get; set; }
        }
    }
}
