using Books.DataAccessLayer.Models;
using Books.DataAccessLayer;

namespace Books.BussinessLogicLayer.Services
{
    public class AuthorService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AuthorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddAuthorAsync(Author author)
        {
            await _unitOfWork.Authors.AddAsync(author);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<Author> GetAuthorByIdAsync(int id)
        {
            return await _unitOfWork.Authors.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Author>> GetAllAuthorAsync()
        {
            return await _unitOfWork.Authors.GetAllAsync();
        }

        public async Task RemoveAuthorAsync(int id)
        {
            var author = await _unitOfWork.Authors.GetByIdAsync(id);
            if (author != null)
            {
                _unitOfWork.Authors.Remove(author);
                await _unitOfWork.CompleteAsync();
            }
        }
    }
}
