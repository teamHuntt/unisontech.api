namespace unisontech.api.Models.DTOs
{
    public class LoanResponseDto
    {
        public Guid Id { get; set; }

        public Guid BookId { get; set; }
        public string BookTitle { get; set; } = string.Empty;
        public string BookISBN { get; set; } = string.Empty;

        public Guid MemberId { get; set; }
        public string MemberName { get; set; } = string.Empty;
        public string MemberEmail { get; set; } = string.Empty;

        public DateTime LoanDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }

        public bool IsReturned => ReturnDate.HasValue;
        public bool IsOverdue => !IsReturned && DateTime.UtcNow > DueDate;
    }
}
