using unisontech.api.Common;
using unisontech.api.Models.DTOs;

namespace unisontech.api.Services
{
    public interface IMemberService
    {
        Task<ApiResponse<List<MemberResponseDto>>> GetAllAsync();
        Task<ApiResponse<MemberResponseDto>> GetByIdAsync(Guid id);
        Task<ApiResponse<MemberResponseDto>> CreateAsync(MemberRequestDto dto);
        Task<ApiResponse<MemberResponseDto>> UpdateAsync(Guid id, MemberRequestDto dto);
        Task<ApiResponse<bool>> DeleteAsync(Guid id);
    }
}