using Asp.Versioning;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using unisontech.api.Models.DTOs;
using unisontech.api.Services;

namespace unisontech.api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class LoansController : ControllerBase
    {
        private readonly ILoanService _loanService;

        public LoansController(ILoanService loanService)
        {
            _loanService = loanService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _loanService.GetAllAsync();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var response = await _loanService.GetByIdAsync(id);
            return Ok(response);
        }

        [HttpGet("member/{memberId}")]
        public async Task<IActionResult> GetByMemberId(Guid memberId)
        {
            var response = await _loanService.GetByMemberIdAsync(memberId);
            return Ok(response);
        }

        [HttpGet("book/{bookId}")]
        public async Task<IActionResult> GetByBookId(Guid bookId)
        {
            var response = await _loanService.GetByBookIdAsync(bookId);
            return Ok(response);
        }

        // Borrow a book
        [HttpPost("borrow")]
        public async Task<IActionResult> Borrow([FromBody] LoanRequestDto dto)
        {
            var response = await _loanService.BorrowBookAsync(dto);
            return Created(string.Empty, response);
        }

        // Return a book
        [HttpPut("return/{loanId}")]
        public async Task<IActionResult> Return(Guid loanId)
        {
            var response = await _loanService.ReturnBookAsync(loanId);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await _loanService.DeleteAsync(id);
            return Ok(response);
        }
    }
}
