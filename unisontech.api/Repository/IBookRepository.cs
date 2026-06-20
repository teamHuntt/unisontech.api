using unisontech.api.Models;

namespace unisontech.api.Repository
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAllAsync();
        Task<Book?> GetByIdAsync(Guid id);
        Task<int> CreateAsync(Book book);
        Task<Book> UpdateAsync(Book book);
        Task<int> DeleteAsync(Book book);
        Task<bool> IsISBNExistsAsync(string iSBN);
    }
}
