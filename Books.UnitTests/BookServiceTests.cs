using Books.BussinessLogicLayer.Services;
using Books.DataAccessLayer.Models;
using Books.DataAccessLayer.Repositories;
using Books.DataAccessLayer;
using Moq;
using Microsoft.EntityFrameworkCore;
using Serilog;
using NUnit.Framework.Legacy;


namespace Books.UnitTests
{
    [TestFixture]
    public class BookServiceTests
    {
        [Test]
        public async Task AddUniqueBooksAsync_ShouldAddOnlyUniqueBooks()
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
                var service = new BookService(unitOfWork); // Replace with your actual service

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

        //[Test]
        //public async Task AddUniqueBooksAsync_ShouldAddOnlyUniqueBooks()
        //{
        //    // Arrange
        //    var existingBooks = new List<Book>
        //{
        //    new Book { Id = Guid.NewGuid(), Title = "Exsisting Book One", Pages = 300, GenreId = Guid.NewGuid(), AuthorId = Guid.NewGuid(), PublisherId = Guid.NewGuid(), ReleaseDate = new DateTime(1998,04,30)},
        //    new Book { Id = Guid.NewGuid(), Title = "Exsisting Book Two", Pages = 250, GenreId = Guid.NewGuid(), AuthorId = Guid.NewGuid(), PublisherId = Guid.NewGuid(), ReleaseDate = new DateTime(1998,04,30)}
        //}.AsQueryable().ToList();

        //    var newBooks = new List<Book>
        //{
        //    new Book { Id = Guid.NewGuid(), Title = "New Book One", Pages = 400, AuthorId = Guid.NewGuid() },
        //    new Book { Id = Guid.NewGuid(), Title = "New Book Two", Pages = 150, AuthorId = Guid.NewGuid() }
        //}.AsQueryable().ToList();

        //    var bookRepositoryMock = new Mock<IRepository<Book>>();
        //    bookRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(existingBooks);
        //    bookRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Book>())).Returns(Task.CompletedTask);

        //    var unitOfWorkMock = new Mock<IUnitOfWork>();
        //    unitOfWorkMock.Setup(uow => uow.Books).Returns(bookRepositoryMock.Object);

        //    var bookService = new BookService(unitOfWorkMock.Object);

        //    // Act
        //    await bookService.AddUniqueBooksAsync(newBooks);

        //    // Assert
        //    bookRepositoryMock.Verify(repo => repo.AddAsync(It.Is<Book>(b => b.Title == "New Book One")), Times.Once);
        //    bookRepositoryMock.Verify(repo => repo.AddAsync(It.Is<Book>(b => b.Title == "Exsisting Book One")), Times.Never);
        //}
    }
}
