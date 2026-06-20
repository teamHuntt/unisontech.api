using Microsoft.EntityFrameworkCore;
using unisontech.api.Data;
using unisontech.api.Models;

namespace unisontech.api.Repository
{
    public class LoanRepository : ILoanRepository
    {
        private readonly AppDbContext _context;

        public LoanRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Loan>> GetAllAsync()
        {
            return await _context.Loans
                .Include(l => l.Book)
                .Include(l => l.Member)
                .ToListAsync();
        }

        public async Task<Loan?> GetByIdAsync(Guid id)
        {
            return await _context.Loans
                .Include(l => l.Book)
                .Include(l => l.Member)
                .FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task<List<Loan>> GetByMemberIdAsync(Guid memberId)
        {
            return await _context.Loans
                .Include(l => l.Book)
                .Include(l => l.Member)
                .Where(l => l.MemberId == memberId)
                .ToListAsync();
        }

        public async Task<List<Loan>> GetByBookIdAsync(Guid bookId)
        {
            return await _context.Loans
                .Include(l => l.Book)
                .Include(l => l.Member)
                .Where(l => l.BookId == bookId)
                .ToListAsync();
        }

        public async Task<bool> HasActiveLoanAsync(Guid memberId, Guid bookId)
        {
            return await _context.Loans
                .AnyAsync(l => l.MemberId == memberId
                            && l.BookId == bookId
                            && l.ReturnDate == null); // null = still borrowed
        }

        public async Task<int> CreateAsync(Loan loan)
        {
            _context.Loans.Add(loan);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateAsync(Loan loan)
        {
            _context.Loans.Update(loan);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(Loan loan)
        {
            _context.Loans.Remove(loan);
            return await _context.SaveChangesAsync();
        }
    }
}
