namespace unisontech.api.Models
{
    public class Book
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string ISBN { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public int Quantity { get; set; }

        public ICollection<Loan> Loans { get; set; } = new List<Loan>();
    }
}
