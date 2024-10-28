
using CsvHelper.Configuration;

namespace Books.DataAccessLayer
{
    public class RecordMap : ClassMap<Record>
    {
        public RecordMap()
        {
            Map(m => m.Title).Name("Title");  
            Map(m => m.Pages).Name("Pages");
            Map(m => m.Genre).Name("Genre");
            //Map(m => m.ReleaseDate).Name("ReleaseDate").TypeConverterOption.Format("M/d/yyyy");
            Map(m => m.Author).Name("Author");
            Map(m => m.Publisher).Name("Publisher");
        }
    }
}
