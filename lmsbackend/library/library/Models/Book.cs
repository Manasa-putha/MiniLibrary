namespace library.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string BookName { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public float Price { get; set; }
        public bool Ordered { get; set; }
        public int BookCategoryId { get; set; }
        public int userId { get; set; } // ID of the user who owns the book
        public int Rating { get; set; }
        public string Description { get; set; } = string.Empty;
        public BookCategory? BookCategory { get; set; }
    }

}
