
using Books.DataAccessLayer.Models;
using Books.DataAccessLayer.Repositories;

namespace Books.DataAccessLayer
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Book> Books { get; }
        IRepository<Genre> Genres { get; }
        IRepository<Author> Authors { get; }
        IRepository<Publisher> Publishers { get; }

        Task<int> CompleteAsync();
    }
}
