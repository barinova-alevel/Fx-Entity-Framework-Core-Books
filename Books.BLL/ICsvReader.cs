
using Books.DataAccessLayer;

namespace Books.BussinessLogicLayer
{
    public interface ICsvReader
    {
        IEnumerable<Record> ReadCsv(string filePath);
    }
}
