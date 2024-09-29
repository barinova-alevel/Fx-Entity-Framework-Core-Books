
namespace Books.DataAccessLayer.Models
{
    public class Author
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid BookId { get; set; }
    }
}
