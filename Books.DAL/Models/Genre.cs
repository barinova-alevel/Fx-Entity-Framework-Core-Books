
namespace Books.DataAccessLayer.Models
{
    public class Genre
    {
        public Guid Id {  get; set; }
        public string Name { get; set; }
        public Guid BookId { get; set; }
        public List<Book> Books { get; set; } = new List<Book>();
    }
}
