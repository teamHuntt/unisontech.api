using unisontech.api.Common;
using unisontech.api.Models.DTOs;

namespace unisontech.api.Services
{
    public interface IBookService
    {
        Task<ApiResponse<List<BookResponseDto>>> GetAllAsync();
        Task<ApiResponse<BookResponseDto>> GetByIdAsync(Guid id);
        Task<ApiResponse<BookResponseDto>> CreateAsync(BookRequestDto dto);
        Task<ApiResponse<BookResponseDto>> UpdateAsync(Guid id, BookRequestDto dto);
        Task<ApiResponse<bool>> DeleteAsync(Guid id);
    }
}