using Microsoft.EntityFrameworkCore;
using unisontech.api.Data;
using unisontech.api.Models;

namespace unisontech.api.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly AppDbContext _context;

        public BookRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            return await _context.Books.ToListAsync();
        }

        public async Task<Book?> GetByIdAsync(Guid id)
        {
            return await _context.Books.FindAsync(id);
        }

        public async Task<int> CreateAsync(Book book)
        {
            _context.Books.Add(book);
            return await _context.SaveChangesAsync();
        }

        public async Task<Book> UpdateAsync(Book book)
        {
            _context.Books.Update(book);
            await _context.SaveChangesAsync();
            return book;
        }

        public async Task<int> DeleteAsync(Book book)
        {
            _context.Books.Remove(book);
            return await _context.SaveChangesAsync();
        }

        public Task<bool> IsISBNExistsAsync(string iSBN)
        {
            return _context.Books.AnyAsync(x => x.ISBN == iSBN);
        }
    }
}
