
using System.ComponentModel.DataAnnotations.Schema;

namespace Books.DataAccessLayer.Models
{
    public class Author
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Book> Books { get; set; }
    }
}
