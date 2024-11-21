using Books.BussinessLogicLayer.Services;
using Books.DataAccessLayer.Models;
using Books.DataAccessLayer.Repositories;
using Books.DataAccessLayer;
using Moq;
using NUnit.Framework.Legacy;
using Microsoft.EntityFrameworkCore;

namespace Books.UnitTests
{
    [TestFixture]
    public class BookServiceTests
    {
        [Test]
        public async Task AddUniqueBooksAsync_ShouldAddOnlyUniqueBooks()
        {
            // Arrange
            var existingBooks = new List<Book>
        {
            new Book { Id = Guid.NewGuid(), Title = "Exsisting Book One", Pages = 300, GenreId = Guid.NewGuid(), AuthorId = Guid.NewGuid(), PublisherId = Guid.NewGuid(), ReleaseDate = new DateTime(1998,04,30)},
            new Book { Id = Guid.NewGuid(), Title = "Exsisting Book Two", Pages = 250, GenreId = Guid.NewGuid(), AuthorId = Guid.NewGuid(), PublisherId = Guid.NewGuid(), ReleaseDate = new DateTime(1998,04,30)}
        }.AsQueryable().ToList();

            var newBooks = new List<Book>
        {
            new Book { Id = Guid.NewGuid(), Title = "New Book One", Pages = 400, AuthorId = Guid.NewGuid() },
            new Book { Id = Guid.NewGuid(), Title = "New Book Two", Pages = 150, AuthorId = Guid.NewGuid() }
        }.AsQueryable().ToList();

            var bookRepositoryMock = new Mock<IRepository<Book>>();
            bookRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(existingBooks);
            bookRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Book>())).Returns(Task.CompletedTask);

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(uow => uow.Books).Returns(bookRepositoryMock.Object);

            var bookService = new BookService(unitOfWorkMock.Object);

            // Act
            await bookService.AddUniqueBooksAsync(newBooks);

            // Assert
            bookRepositoryMock.Verify(repo => repo.AddAsync(It.Is<Book>(b => b.Title == "New Book One")), Times.Once);
            bookRepositoryMock.Verify(repo => repo.AddAsync(It.Is<Book>(b => b.Title == "Exsisting Book One")), Times.Never);
        }
    }
}
