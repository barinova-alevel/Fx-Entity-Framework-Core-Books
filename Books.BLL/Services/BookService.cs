
using Books.DataAccessLayer;
using Books.DataAccessLayer.Models;

namespace Books.BussinessLogicLayer.Services
{
    public class BookService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BookService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
