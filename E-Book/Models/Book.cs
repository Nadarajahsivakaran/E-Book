namespace E_Book.Models
{
    public class Book
    {
        public int Id { get; set; }

        public required string Title { get; set; }

        public required string Author { get; set; }

        public required DateTime PublishedDate { get; set; }

        public int  NoOfCopies { get; set; } = 1;
    }
}
