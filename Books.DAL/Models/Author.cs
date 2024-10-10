
namespace Books.DataAccessLayer.Models
{
    public class Author
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid BookId { get; set; }
        //public List<BookAuthor> BookAuthors { get; set; } = new List<BookAuthor>();
        public List<Book> Books { get; set; } = new List<Book>();
    }
}
