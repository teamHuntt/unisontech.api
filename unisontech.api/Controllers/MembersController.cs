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
    public class MembersController : ControllerBase
    {
        private readonly IMemberService _memberService;

        public MembersController(IMemberService memberService)
        {
            _memberService = memberService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _memberService.GetAllAsync();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var response = await _memberService.GetByIdAsync(id);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MemberRequestDto dto)
        {
            var response = await _memberService.CreateAsync(dto);
            return Created(string.Empty, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] MemberRequestDto dto)
        {
            var response = await _memberService.UpdateAsync(id, dto);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await _memberService.DeleteAsync(id);
            return Ok(response);
        }
    }
}
