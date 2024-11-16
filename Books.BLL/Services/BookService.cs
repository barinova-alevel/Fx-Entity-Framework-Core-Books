
using Books.DataAccessLayer;
using Books.DataAccessLayer.Models;
using Books.DataAccessLayer.Repositories;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Books.BussinessLogicLayer.Services
{
    public class BookService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BookService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddUniqueBooksAsync(List<Book> fileBooks)
        {
            List<Book> uniqueBooks = new List<Book>();
            using var context = new ApplicationContext();
            var bookRepository = new BookRepository(context);
            Log.Information("Checking presence of books that are not previously added to db.");

            foreach (var book in fileBooks)
            {
                bool exists = await context.Books
                    .Include(b => b.Author)
                    .AnyAsync(b => b.Title.ToLower().Trim() == book.Title.ToLower().Trim()
                        && b.Author.Name.ToLower().Trim() == book.Author.Name.ToLower().Trim());

                if (!exists)
                {
                    Log.Information($"{book.Title}, {book.Author.Name}");
                    uniqueBooks.Add(book);
                }
            }
            Log.Information($"{uniqueBooks.Count} books is adding.");
            await bookRepository.AddRangeAsync(uniqueBooks);
        }

        public async Task AddBookAsync(Book book)
        { 
            await _unitOfWork.Books.AddAsync(book);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<Book> GetBookByIdAsync(int id)
        {
            return await _unitOfWork.Books.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            return await _unitOfWork.Books.GetAllAsync();
        }

        public async Task RemoveBookAsync(int id)
        {
            var book = await _unitOfWork.Books.GetByIdAsync(id);
            if (book != null)
            {
                _unitOfWork.Books.Remove(book);
                await _unitOfWork.CompleteAsync();
            }
        }
    }
}
