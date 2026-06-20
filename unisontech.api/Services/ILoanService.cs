using unisontech.api.Common;
using unisontech.api.Models.DTOs;

namespace unisontech.api.Services
{
    public interface ILoanService
    {
        Task<ApiResponse<List<LoanResponseDto>>> GetAllAsync();
        Task<ApiResponse<LoanResponseDto>> GetByIdAsync(Guid id);
        Task<ApiResponse<List<LoanResponseDto>>> GetByMemberIdAsync(Guid memberId);
        Task<ApiResponse<List<LoanResponseDto>>> GetByBookIdAsync(Guid bookId);
        Task<ApiResponse<LoanResponseDto>> BorrowBookAsync(LoanRequestDto dto);
        Task<ApiResponse<LoanResponseDto>> ReturnBookAsync(Guid loanId);
        Task<ApiResponse<bool>> DeleteAsync(Guid id);
    }
}