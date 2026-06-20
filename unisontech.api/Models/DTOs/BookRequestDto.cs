namespace unisontech.api.Models.DTOs
{
    public class BookRequestDto
    {
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string ISBN { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public int Quantity { get; set; }
    }
}
