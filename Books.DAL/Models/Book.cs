
using Microsoft.Identity.Client;

namespace Books.DataAccessLayer.Models
{
    public class Book
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int Pages { get; set; }
        public Guid GenreId { get; set; }
        public Guid AuthorId { get; set; }
        public Guid PublisherId { get; set; }
        public DateTime ReleaseDate { get; set; }

        public List<Author> Authors { get; set; } = new List<Author>();
        public Genre Genre { get; set; }
    }
}
