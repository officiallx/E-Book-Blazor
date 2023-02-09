using ebook.Models.Books;

namespace ebook.Models
{
    public class MenuRoleModel
    {
        public int Id { get; set; }
        public int book_id { get; set; }
        public BooksModel Book { get; set; }
        public List<MenuList>? menu { get; set; } = new();
    }
    public class MenuList
    {
        public int subcategory_id { get; set; }
        public string subcategory_name { get; set; }
        public bool IsActive { get; set; }
    }
}
