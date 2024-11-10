using System.Globalization;
using Books.DataAccessLayer;
using Books.DataAccessLayer.Models;
using CsvHelper;
using Serilog;

namespace Books.BussinessLogicLayer.Services
{
    public class CsvService
    {
        public IEnumerable<Record> ParseCsv(string filePath)
        {
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Context.RegisterClassMap<RecordMap>();
                var result = csv.GetRecords<Record>().ToList();
                Log.Information("List of records has been created.");
                DisplayList(result);
                return result;
            }
        }

        public List<Book> GetBooksFromFile(IEnumerable<Record> records)
        {
            var authors = GetListUniqueAuthors(records);
            var authorsMap = authors.ToDictionary(_ => _.Name);
            var genres = GetListUniqueGenres(records);
            var genresMap = genres.ToDictionary(_ => _.Name);
            var publishers = GetListUniquePublishers(records);
            var publishersMap = publishers.ToDictionary(_ => _.Name);
            var books = records
                .Select(_ => new Book()
                {
                    Title = _.Title,
                    ReleaseDate = _.ReleaseDate,
                    Pages = _.Pages,
                    Author = authorsMap[_.Author],
                    Genre = genresMap[_.Genre],
                    Publisher = publishersMap[_.Publisher]
                })
                .ToList();
            return books;
        }

        public List<Author> GetListUniqueAuthors(IEnumerable<Record> records)
        {
           var authors = records
            .Where(r => !string.IsNullOrEmpty(r.Author))
            .Select(r => r.Author)
            .Distinct()
            .Select(_=> new Author() { Name = _ })
            .ToList();
            return authors;
        }

        public List<Genre> GetListUniqueGenres(IEnumerable<Record> records)
        {
            var genres = records
             .Where(r => !string.IsNullOrEmpty(r.Genre))
             .Select(r => r.Genre)
             .Distinct()
             .Select(_ => new Genre() { Name = _ })
             .ToList();
            return genres;
        }

        public List<Publisher> GetListUniquePublishers(IEnumerable<Record> records)
        {
            var publishers = records
             .Where(r => !string.IsNullOrEmpty(r.Publisher))
             .Select(r => r.Publisher)
             .Distinct()
             .Select(_ => new Publisher() { Name = _ })
             .ToList();
            return publishers;
        }

        private void DisplayList(List<Record> records)
        {
            foreach (var record in records)
            {
                Log.Information($"Title: {record.Title}, Pages: {record.Pages}, Genre: {record.Genre}, " +
                                  $"Release Date: {record.ReleaseDate}, Author: {record.Author}, Publisher: {record.Publisher}");
            }
        }
    }
}
