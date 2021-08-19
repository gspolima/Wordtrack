namespace Wordtrack.Api.Dtos
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int YearPublished { get; set; }
        public int Pages { get; set; }
        public bool isRead { get; set; }
    }
}
