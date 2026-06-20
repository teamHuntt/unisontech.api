using Microsoft.EntityFrameworkCore;
using unisontech.api.Data;
using unisontech.api.Models;

namespace unisontech.api.Repository
{
    public class MemberRepository :IMemberRepository
    {
        private readonly AppDbContext _context;

        public MemberRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Member>> GetAllAsync()
        {
            return await _context.Members.ToListAsync();
        }

        public async Task<Member?> GetByIdAsync(Guid id)
        {
            return await _context.Members.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<bool> IsEmailExistsAsync(string email)
        {
            return await _context.Members.AnyAsync(m => m.Email == email);
        }

        public async Task<int> CreateAsync(Member member)
        {
            _context.Members.Add(member);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateAsync(Member member)
        {
            _context.Members.Update(member);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(Member member)
        {
            _context.Members.Remove(member);
            return await _context.SaveChangesAsync();
        }
    }
}
