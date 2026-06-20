using unisontech.api.Common;
using unisontech.api.Models;
using unisontech.api.Models.DTOs;
using unisontech.api.Repository;

namespace unisontech.api.Services
{
    public class LoanService : ILoanService
    {
        private readonly ILoanRepository _loanRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IMemberRepository _memberRepository;

        public LoanService(
            ILoanRepository loanRepository,
            IBookRepository bookRepository,
            IMemberRepository memberRepository)
        {
            _loanRepository = loanRepository;
            _bookRepository = bookRepository;
            _memberRepository = memberRepository;
        }

        public async Task<ApiResponse<List<LoanResponseDto>>> GetAllAsync()
        {
            var loans = await _loanRepository.GetAllAsync();
            var result = loans.Select(l => ToDto(l)).ToList();
            return ApiResponse<List<LoanResponseDto>>.Ok(result);
        }

        public async Task<ApiResponse<LoanResponseDto>> GetByIdAsync(Guid id)
        {
            var loan = await _loanRepository.GetByIdAsync(id);
            if (loan is null)
                return ApiResponse<LoanResponseDto>.Fail($"Loan with ID {id} not found.");

            return ApiResponse<LoanResponseDto>.Ok(ToDto(loan));
        }

        public async Task<ApiResponse<List<LoanResponseDto>>> GetByMemberIdAsync(Guid memberId)
        {
            var member = await _memberRepository.GetByIdAsync(memberId);
            if (member is null)
                return ApiResponse<List<LoanResponseDto>>.Fail($"Member with ID {memberId} not found.");

            var loans = await _loanRepository.GetByMemberIdAsync(memberId);
            var result = loans.Select(l => ToDto(l)).ToList();
            return ApiResponse<List<LoanResponseDto>>.Ok(result);
        }

        public async Task<ApiResponse<List<LoanResponseDto>>> GetByBookIdAsync(Guid bookId)
        {
            var book = await _bookRepository.GetByIdAsync(bookId);
            if (book is null)
                return ApiResponse<List<LoanResponseDto>>.Fail($"Book with ID {bookId} not found.");

            var loans = await _loanRepository.GetByBookIdAsync(bookId);
            var result = loans.Select(l => ToDto(l)).ToList();
            return ApiResponse<List<LoanResponseDto>>.Ok(result);
        }

        public async Task<ApiResponse<LoanResponseDto>> BorrowBookAsync(LoanRequestDto dto)
        {
            //Check if book exists
            var book = await _bookRepository.GetByIdAsync(dto.BookId);
            if (book is null)
                return ApiResponse<LoanResponseDto>.Fail("Book not found.");

            // check member exists if
            var member = await _memberRepository.GetByIdAsync(dto.MemberId);
            if (member is null)
                return ApiResponse<LoanResponseDto>.Fail("Member not found.");

            // make sure memeber is acrtive or not
            if (!member.IsActive)
                return ApiResponse<LoanResponseDto>.Fail("Member account is inactive.");

            // check if book availabe or not
            if (book.Quantity <= 0)
                return ApiResponse<LoanResponseDto>.Fail($"Book '{book.Title}' is out of stock.");

            // make sure this member already take this book or not
            bool hasActiveLoan = await _loanRepository.HasActiveLoanAsync(dto.MemberId, dto.BookId);
            if (hasActiveLoan)
                return ApiResponse<LoanResponseDto>.Fail($"Member already has an active loan for '{book.Title}'.");

            // checking again if the due date is futured or not
            if (dto.DueDate <= DateTime.UtcNow)
                return ApiResponse<LoanResponseDto>.Fail("Due date must be in the future.");

            // Create loan
            var loan = new Loan
            {
                Id = Guid.NewGuid(),
                BookId = dto.BookId,
                MemberId = dto.MemberId,
                LoanDate = DateTime.UtcNow,
                DueDate = dto.DueDate,
                ReturnDate = null
            };

            // Reduce book quantity
            book.Quantity--;
            await _bookRepository.UpdateAsync(book);

            int created = await _loanRepository.CreateAsync(loan);
            if (created > 0)
            {
                // reload with includes
                var created_loan = await _loanRepository.GetByIdAsync(loan.Id);
                return ApiResponse<LoanResponseDto>.Ok(ToDto(created_loan!), "Book borrowed successfully.");
            }

            return ApiResponse<LoanResponseDto>.Fail("Failed to borrow book.");
        }

        public async Task<ApiResponse<LoanResponseDto>> ReturnBookAsync(Guid loanId)
        {
            var loan = await _loanRepository.GetByIdAsync(loanId);
            if (loan is null)
                return ApiResponse<LoanResponseDto>.Fail("Loan not found.");

            //Check not already returned
            if (loan.ReturnDate != null)
                return ApiResponse<LoanResponseDto>.Fail("Book has already been returned.");

            // Mark as returned
            loan.ReturnDate = DateTime.UtcNow;

            // Increase book quantity back
            var book = await _bookRepository.GetByIdAsync(loan.BookId);
            if (book is not null)
            {
                book.Quantity++;
                await _bookRepository.UpdateAsync(book);
            }

            int updated = await _loanRepository.UpdateAsync(loan);
            if (updated > 0)
                return ApiResponse<LoanResponseDto>.Ok(ToDto(loan), "Book returned successfully.");

            return ApiResponse<LoanResponseDto>.Fail("Failed to return book.");
        }

        public async Task<ApiResponse<bool>> DeleteAsync(Guid id)
        {
            var loan = await _loanRepository.GetByIdAsync(id);
            if (loan is null)
                return ApiResponse<bool>.Fail("Loan not found.");

            int deleted = await _loanRepository.DeleteAsync(loan);
            if (deleted > 0)
                return ApiResponse<bool>.Ok(true, "Loan deleted successfully.");

            return ApiResponse<bool>.Fail("Failed to delete loan.");
        }

        // inline select — no mapper needed
        private static LoanResponseDto ToDto(Loan l) => new()
        {
            Id = l.Id,
            BookId = l.BookId,
            BookTitle = l.Book?.Title ?? string.Empty,
            BookISBN = l.Book?.ISBN ?? string.Empty,
            MemberId = l.MemberId,
            MemberName = l.Member?.Name ?? string.Empty,
            MemberEmail = l.Member?.Email ?? string.Empty,
            LoanDate = l.LoanDate,
            DueDate = l.DueDate,
            ReturnDate = l.ReturnDate
        };
    }
}
