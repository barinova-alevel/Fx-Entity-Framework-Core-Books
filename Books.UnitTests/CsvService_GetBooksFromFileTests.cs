using Books.BussinessLogicLayer;
using Books.BussinessLogicLayer.Services;
using Books.DataAccessLayer;
using NUnit.Framework.Legacy;

namespace Books.UnitTests
{
    [TestFixture]
    public class CsvService_GetBooksFromFileTests
    {
        ICsvReader _csvReader = new CsvReaderWrapper();

        [Test]
        public void GetBooksFromFile_ShouldReturnBooks_WhenValidRecordsAreProvided()
        {
            // Arrange
            CsvService _csvService = new CsvService(_csvReader);
            IEnumerable<Record> records = new[] {
                new Record { Title = "BookTitle1", Pages = 15, Genre = "Fiction", ReleaseDate = new DateTime(2024, 11, 21), Author = "Author1", Publisher = "Publisher1" },
                new Record { Title = "BookTitle2", Pages = 10, Genre = "Fiction", ReleaseDate = new DateTime(2010, 11, 21), Author = "Author2", Publisher = "Publisher2" }
            };

            // Act
            var result = _csvService.GetBooksFromFile(records);

            // Assert
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result[0].Title, Is.EqualTo("BookTitle1"));
            Assert.That(result[1].Title, Is.EqualTo("BookTitle2"));
        }

        [Test]
        public void GetBooksFromFile_ShouldReturnEmptyList_WhenRecordsAreEmpty()
        {
            // Arrange
            CsvService _csvService = new CsvService(_csvReader);
            IEnumerable<Record> records = Enumerable.Empty<Record>();

            // Act
            var result = _csvService.GetBooksFromFile(records);

            // Assert
            Assert.That(result.Count, Is.EqualTo(0));
            ClassicAssert.IsEmpty(result);
            ClassicAssert.NotNull(result);
        }

        [Test]
        public void GetBooksFromFile_ShouldThrowArgumentNullException_WhenRecordsIsNull()
        {
            // Arrange
            CsvService _csvService = new CsvService(_csvReader);
            IEnumerable<Record> records = null;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => _csvService.GetBooksFromFile(records));
        }

        [Test]
        public void GetBooksFromFile_ShouldHandleInvalidData()
        {
            // Arrange
            CsvService _csvService = new CsvService(_csvReader);
            IEnumerable<Record> records = new[] {
                new Record { Title = "BookTitle1", Pages = 15, Genre = "Fiction", ReleaseDate = new DateTime(2024, 11, 21), Author = "Author1", Publisher = "Publisher1" },
                new Record { Title = null, Pages = 15, Genre = "Fiction", ReleaseDate = new DateTime(2024, 11, 21), Author = "Author1", Publisher = "Publisher1" },
                new Record { Title = "BookTitle2", Pages = 10, Genre = "Fiction", ReleaseDate = new DateTime(2010, 11, 21), Author = "Author2", Publisher = "Publisher2" }
            };

            // Act
            var result = _csvService.GetBooksFromFile(records);

            // Assert
            ClassicAssert.NotNull(result);
            Assert.That(result.Count, Is.EqualTo(3));
            Assert.That(result[2].Title, Is.EqualTo("BookTitle2"));
        }
    }
}

