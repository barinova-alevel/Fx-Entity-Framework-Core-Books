using System.Globalization;
using Books.DataAccessLayer;
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
                //DisplayList(result);
                return result;
            }
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
