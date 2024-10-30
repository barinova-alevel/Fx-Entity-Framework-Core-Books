
namespace Books.DataAccessLayer
{
    public class Record
    {
        public string Title { get; set; }
        public int Pages { get; set; }
        public string Genre { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
    }
}
