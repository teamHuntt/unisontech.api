namespace unisontech.api.Models
{
    public class Loan
    {
        public Guid Id { get; set; }

        public Guid BookId { get; set; }
        public Book Book { get; set; } = null!;

        public Guid MemberId { get; set; }
        public Member Member { get; set; } = null!;

        public DateTime LoanDate { get; set; } = DateTime.UtcNow;
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }
    }
}
