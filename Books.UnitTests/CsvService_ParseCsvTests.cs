using Books.BussinessLogicLayer.Services;
using Books.BussinessLogicLayer;
using Moq;
using Books.DataAccessLayer;
using NUnit.Framework.Legacy;

namespace Books.UnitTests
{
    [TestFixture]
    public class CsvService_ParseCsvTests
    {
        private Mock<ICsvReader> _csvReaderMock;
        private CsvService _csvService;

        [SetUp]
        public void SetUp()
        {
            _csvReaderMock = new Mock<ICsvReader>();
            _csvService = new CsvService(_csvReaderMock.Object);
        }

        [Test]
        public void ParseCsv_ShouldReturnRecords_WhenFileIsValid()
        {
            // Arrange
            var filePath = "valid.csv";
            var expectedRecords = new List<Record>
        {
            new Record { Title = "Book1", Pages = 100, Genre = "Fiction", ReleaseDate = new DateTime(2024, 11, 20), Author = "Author1" },
            new Record { Title = "Book2", Pages = 200, Genre = "Classics", ReleaseDate = new DateTime(2023, 5, 15), Author = "Author2" }
        };
            _csvReaderMock.Setup(reader => reader.ReadCsv(filePath)).Returns(expectedRecords);

            // Act
            var result = _csvService.ParseCsv(filePath);
            var records = result.ToList();

            // Assert
            ClassicAssert.NotNull(result);
            Assert.That(records.Count, Is.EqualTo(2));
            Assert.That(records[0].Title, Is.EqualTo("Book1"));
            Assert.That(records[0].Pages, Is.EqualTo(100));
        }

        [Test]
        public void ParseCsv_ShouldThrowArgumentException_WhenFilePathIsNull()
        {
            // Arrange
            string invalidFilePath = null;

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => _csvService.ParseCsv(invalidFilePath));
        }

        [Test]
        public void ParseCsv_ShouldThrowFormatException_WhenCsvFormatIsInvalid()
        {
            // Arrange
            var filePath = "invalid.csv";
            _csvReaderMock.Setup(reader => reader.ReadCsv(filePath)).Throws<FormatException>();

            // Act & Assert
            Assert.Throws<FormatException>(() => _csvService.ParseCsv(filePath));
        }

        //ParseCsv > Get correct records; Get incorrect records; Case sensitivity ; White space in start of entity; file is used; Title is missing; TypeConverterException (dateTime in diff format); Not csv format file; null passinп, missing file, empty lines
    }
}
