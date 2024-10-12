
using Books.DataAccessLayer.Models;
using Books.DataAccessLayer;

namespace Books.BussinessLogicLayer.Services
{
    public class PublisherService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PublisherService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddGenreAsync(Publisher publisher)
        {
            await _unitOfWork.Publishers.AddAsync(publisher);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<Publisher> GetPublisherByIdAsync(int id)
        {
            return await _unitOfWork.Publishers.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Publisher>> GetAllGenresAsync()
        {
            return await _unitOfWork.Publishers.GetAllAsync();
        }

        public async Task RemovePublisherAsync(int id)
        {
            var publisher = await _unitOfWork.Publishers.GetByIdAsync(id);
            if (publisher != null)
            {
                _unitOfWork.Publishers.Remove(publisher);
                await _unitOfWork.CompleteAsync();
            }
        }
    }
}
