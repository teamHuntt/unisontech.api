using Asp.Versioning;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using unisontech.api.Common;
using unisontech.api.Models.DTOs;
using unisontech.api.Services;

namespace unisontech.api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var books = await _bookService.GetAllAsync();
            if (books.Success)
            {
                return Ok(books);
            }
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var book = await _bookService.GetByIdAsync(id);
            return Ok(book);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BookRequestDto dto)
        {
            var book = await _bookService.CreateAsync(dto);
            if (book.Success)
            {
                return Ok(book);
            }
            return BadRequest(book);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] BookRequestDto dto)
        {
            var book = await _bookService.UpdateAsync(id, dto);
            return Ok(book);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var res = await _bookService.DeleteAsync(id);
            if (res.Success)
            {
                return Ok(res);
            }
            return BadRequest(res);
        }
    }
}
