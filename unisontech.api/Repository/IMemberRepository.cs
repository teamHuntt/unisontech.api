using unisontech.api.Models;

namespace unisontech.api.Repository
{
    public interface IMemberRepository
    {
        Task<List<Member>> GetAllAsync();
        Task<Member?> GetByIdAsync(Guid id);
        Task<bool> IsEmailExistsAsync(string email);
        Task<int> CreateAsync(Member member);
        Task<int> UpdateAsync(Member member);
        Task<int> DeleteAsync(Member member);
    }
}