using Books.BussinessLogicLayer.Services;
using Books.DataAccessLayer.Models;
using Books.DataAccessLayer.Repositories;
using Books.DataAccessLayer;
using Moq;
using Microsoft.EntityFrameworkCore;
using Serilog;
using NUnit.Framework.Legacy;
using Microsoft.Data.Sqlite;


namespace Books.UnitTests
{
    [TestFixture]
    public class BookServiceTests
    {
        [Test]
        public async Task AddUniqueBooksAsync_UseInMemory_ShouldAddOnlyUniqueBooks()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            IUnitOfWork unitOfWork = new UnitOfWork(new ApplicationContext()); // New app context?
            var existingBooks = new List<Book>
            {
        new Book { Title = "Existing Book 1", Author = new Author { Name = "Author 1" } },
        new Book { Title = "Existing Book 2", Author = new Author { Name = "Author 2" } }
             };

            var newBooks = new List<Book>
    {
        new Book { Title = "Existing Book 1", Author = new Author { Name = "Author 1" } }, // Duplicate
        new Book { Title = "New Book 1", Author = new Author { Name = "Author 3" } }, // Unique
        new Book { Title = "New Book 2", Author = new Author { Name = "Author 4" } } // Unique
    };

            using (var context = new ApplicationContext(options))
            {
                context.Books.AddRange(existingBooks);
                await context.SaveChangesAsync();
            }

            // Act
            using (var context = new ApplicationContext(options))
            {
                var bookRepository = new BookRepository(context);
                var logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();
                var service = new BookService(unitOfWork);
                await service.AddUniqueBooksAsync(newBooks);
            }

            // Assert
            using (var context = new ApplicationContext(options))
            {
                var allBooks = await context.Books.Include(b => b.Author).ToListAsync();
                //ClassicAssert.AreEqual(4, allBooks.Count); // 2 existing + 2 unique
                ClassicAssert.IsTrue(allBooks.Any(b => b.Title == "New Book 1" && b.Author.Name == "Author 3"));
                ClassicAssert.IsTrue(allBooks.Any(b => b.Title == "New Book 2" && b.Author.Name == "Author 4"));
            }
        }

        [Test]
        public async Task AddUniqueBooksAsync_UseSQLite_ShouldAddOnlyUniqueBooksAsync()
        {
            // Arrange
            var connectionStringBuilder =
                new SqliteConnectionStringBuilder { DataSource = ":memory:" };
            var connection = new SqliteConnection(connectionStringBuilder.ToString());

            var options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseSqlite(connection)
                .Options;

            IUnitOfWork unitOfWork = new UnitOfWork(new ApplicationContext());
            var service = new BookService(unitOfWork);

            var existingBooks = new List<Book>
            {
                new Book { Title = "Existing Book 1", Author = new Author { Name = "Author 1" } },
                new Book { Title = "Existing Book 2", Author = new Author { Name = "Author 2" } }
             };

            var newBooks = new List<Book>
            {
                new Book { Title = "Existing Book 1", Author = new Author { Name = "Author 1" } }, // Duplicate
                new Book { Title = "New Book 1", Author = new Author { Name = "Author 3" } }, // Unique
                new Book { Title = "New Book 2", Author = new Author { Name = "Author 4" } } // Unique
            };

            using (var context = new ApplicationContext(options))
            {
                context.Database.OpenConnection();
                context.Database.EnsureCreated();

                context.Books.AddRange(existingBooks);
                context.SaveChanges();

                //Act
                await service.AddUniqueBooksAsync(newBooks);
                var allBooks = await context.Books.Include(b => b.Author).ToListAsync();

                //Assert
                ClassicAssert.IsTrue(allBooks.Any(b => b.Title == "New Book 1" && b.Author.Name == "Author 3"));
                ClassicAssert.IsTrue(allBooks.Any(b => b.Title == "New Book 2" && b.Author.Name == "Author 4"));
                ClassicAssert.AreEqual(4, allBooks.Count); // 2 existing + 2 unique
            }
        }
    }
}
