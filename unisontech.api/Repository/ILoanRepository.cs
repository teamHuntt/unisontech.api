using unisontech.api.Models;

namespace unisontech.api.Repository
{
    public interface ILoanRepository
    {
        Task<List<Loan>> GetAllAsync();
        Task<Loan?> GetByIdAsync(Guid id);
        Task<List<Loan>> GetByMemberIdAsync(Guid memberId);
        Task<List<Loan>> GetByBookIdAsync(Guid bookId);
        Task<bool> HasActiveLoanAsync(Guid memberId, Guid bookId);
        Task<int> CreateAsync(Loan loan);
        Task<int> UpdateAsync(Loan loan);
        Task<int> DeleteAsync(Loan loan);
    }
}