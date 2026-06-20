using System.Reflection.Metadata.Ecma335;
using unisontech.api.Common;
using unisontech.api.Models;
using unisontech.api.Models.DTOs;
using unisontech.api.Repository;

namespace unisontech.api.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }
        public async Task<ApiResponse<List<BookResponseDto>>> GetAllAsync()
        {
            var books = await _bookRepository.GetAllAsync();
            if (books.Count() > 0)
            {
                var result = books.Select(b => new BookResponseDto
                {
                    Id = b.Id,
                    Title = b.Title,
                    Author = b.Author,
                    ISBN = b.ISBN,
                    Genre = b.Genre,
                    Quantity = b.Quantity
                }).ToList();

                return ApiResponse<List<BookResponseDto>>.Ok(result);
            }
            return ApiResponse<List<BookResponseDto>>.Fail("No record found");

        }

        public async Task<ApiResponse<BookResponseDto>> GetByIdAsync(Guid id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book is null)
                throw new NotFoundException($"Book with ID {id} not found.");

            var result = new BookResponseDto
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                ISBN = book.ISBN,
                Genre = book.Genre,
                Quantity = book.Quantity
            };

            return ApiResponse<BookResponseDto>.Ok(result);
        }

        public async Task<ApiResponse<BookResponseDto>> CreateAsync(BookRequestDto dto)
        {
            bool isExists = await _bookRepository.IsISBNExistsAsync(dto.ISBN);
            if (isExists)
                return ApiResponse<BookResponseDto>.Fail($"Book with ISBN {dto.ISBN} already exists.");
          
            var book = new Book
            {
                Id = Guid.NewGuid(),
                Title = dto.Title,
                Author = dto.Author,
                ISBN = dto.ISBN,
                Genre = dto.Genre,
                Quantity = dto.Quantity
            };

            int created = await _bookRepository.CreateAsync(book);
            if (created > 0)
            {
                var result = new BookResponseDto
                {
                    Id = book.Id,
                    Title = book.Title,
                    Author = book.Author,
                    ISBN = book.ISBN,
                    Genre = book.Genre,
                    Quantity = book.Quantity
                };
                return ApiResponse<BookResponseDto>.Ok(result, "Created successfully");
            }
            return ApiResponse<BookResponseDto>.Fail("Failed to create book.");
        }

        public async Task<ApiResponse<BookResponseDto>> UpdateAsync(Guid id, BookRequestDto dto)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book is null)
                throw new NotFoundException($"Book with ID {id} not found.");

            book.Title = dto.Title;
            book.Author = dto.Author;
            book.ISBN = dto.ISBN;
            book.Genre = dto.Genre;
            book.Quantity = dto.Quantity;

            var updated = await _bookRepository.UpdateAsync(book);

            var result = new BookResponseDto
            {
                Id = updated.Id,
                Title = updated.Title,
                Author = updated.Author,
                ISBN = updated.ISBN,
                Genre = updated.Genre,
                Quantity = updated.Quantity
            };

            return ApiResponse<BookResponseDto>.Ok(result, "Book updated successfully.");
        }

        public async Task<ApiResponse<bool>> DeleteAsync(Guid id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book is null)
                throw new NotFoundException($"Book with ID {id} not found.");

            int res = await _bookRepository.DeleteAsync(book);
            if (res > 0)
            {
                return ApiResponse<bool>.Ok(true, "Book deleted successfully.");
            }
            return ApiResponse<bool>.Fail("Failed to delete book.");
        }
    }
}
