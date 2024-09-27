
using Books.DataAccessLayer.Models;
using Books.DataAccessLayer;

namespace Books.BussinessLogicLayer.Services
{
    public class GenreService
    {
        private readonly IUnitOfWork _unitOfWork;

        public GenreService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddGenreAsync(Genre genre)
        {
            await _unitOfWork.Genres.AddAsync(genre);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<Genre> GetGenreByIdAsync(int id)
        {
            return await _unitOfWork.Genres.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Genre>> GetAllGenresAsync()
        {
            return await _unitOfWork.Genres.GetAllAsync();
        }

        public async Task RemoveGenreAsync(int id)
        {
            var genre = await _unitOfWork.Genres.GetByIdAsync(id);
            if (genre != null)
            {
                _unitOfWork.Genres.Remove(genre);
                await _unitOfWork.CompleteAsync();
            }
        }
    }
}
