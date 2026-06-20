namespace unisontech.api.Models.DTOs
{
    public class LoanRequestDto
    {
        public Guid BookId { get; set; }
        public Guid MemberId { get; set; }
        public DateTime DueDate { get; set; }
    }
}
