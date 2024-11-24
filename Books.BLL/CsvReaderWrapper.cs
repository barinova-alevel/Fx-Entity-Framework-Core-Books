using System.Globalization;
using Books.DataAccessLayer;
using CsvHelper;
using Serilog;

namespace Books.BussinessLogicLayer
{
    public class CsvReaderWrapper : ICsvReader
    {
        public IEnumerable<Record> ReadCsv(string filePath)
        {
            using var reader = new StreamReader(filePath);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            {
                csv.Context.RegisterClassMap<RecordMap>();
                var result = csv.GetRecords<Record>().ToList();
                Log.Information("List of records has been created.");
                return result;
            }
        }
    }
}
