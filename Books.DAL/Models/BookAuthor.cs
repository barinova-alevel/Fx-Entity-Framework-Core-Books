
namespace Books.DataAccessLayer.Models
{
    public class BookAuthor
    {
        public Guid Id { get; set; }
        public Guid BookId { get; set; }    
        public Guid AuthorId { get; set; }
        public Book Book { get; set; }
        public Author Author { get; set; }
    }
}
