using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineBookStore.Interfaces;
using OnlineBookStore.Models.DTOs;
using OnlineBookStore.Exceptions;
using Microsoft.AspNetCore.Cors;

namespace OnlineBookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("reactApp")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult AddBook([FromBody] BookDTO bookDTO)
        {
            try
            {
                var isSuccess = _bookService.Add(bookDTO);
                if (isSuccess)
                    return Ok("Book added successfully.");
                else
                    return BadRequest("Failed to add Book.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public IActionResult GetBook(int id)
        {
            try
            {
                var bookDTO = _bookService.Get(id);
                if (bookDTO != null)
                    return Ok(bookDTO);
                else
                    return NotFound("Book not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [Authorize(Roles = "User,Admin")]
        [HttpGet]
        public IActionResult GetAllBooks()
        {
            try
            {
                var bookDTOs = _bookService.GetAll();
                return Ok(bookDTOs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public IActionResult RemoveBook(int id)
        {
            try
            {
                var isSuccess = _bookService.Remove(id);
                if (isSuccess)
                    return Ok("Book removed successfully.");
                else
                    return NotFound("Book not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpPut]
        public IActionResult UpdateBook([FromBody] BookDTO bookDTO)
        {
            try
            {
                var updatedBook = _bookService.Update(bookDTO);
                if (updatedBook != null)
                    return Ok(updatedBook);
                else
                    return NotFound("Book not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "No events available");
            }
        }
        [Authorize(Roles = "User,Admin")]
        [HttpGet("title/{title}")]
        public IActionResult GetBookByTitle(string title)
        {
            try
            {
                var book = _bookService.GetBookByTitle(title);
                if (book != null)
                    return Ok(book);
                return NotFound();
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
        [Authorize(Roles = "User")]
        [HttpGet("genere/{genere}")]
        public IActionResult GetBooksByGenre(string genere)
        {
            try
            {
                var book = _bookService.GetBooksByGenre(genere);
                if (book != null)
                    return Ok(book);
                return NotFound();
            }
            catch (BookNotFoundException ex)
            {
                
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

    }
}
