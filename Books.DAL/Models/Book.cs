
namespace Books.DataAccessLayer.Models
{
    public class Book
    {
        Guid _id { get; set; }
        string _title { get; set; }
        int _pages { get; set; }
        Guid _genreId { get; set; }
        Guid _authorId { get; set; }
        Guid _publisherId { get; set; }
        DateTime _releaseDate { get; set; }
    }
}
