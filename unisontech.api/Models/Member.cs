namespace unisontech.api.Models
{
    public class Member
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public DateTime MembershipDate { get; set; } = DateTime.UtcNow;
        public ICollection<Loan> Loans { get; set; } = new List<Loan>();
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true;
        public bool IsEmailVerified { get; set; } = false;
    }
}
