namespace ebook.Models.Books
{
    public class BooksModel
    {
        public int book_id { get; set; }
        public string title { get; set; }
        public string isbn { get; set; }
        public int num_pages { get; set; }
        public string book_cover { get; set; }
        public DateTime? publication_date { get; set; }
        public String? publicationDate { get; set; }
        public int author_id { get; set; }
    }
}
