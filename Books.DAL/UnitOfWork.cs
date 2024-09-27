
using Books.DataAccessLayer.Models;
using Books.DataAccessLayer.Repositories;
using System;

namespace Books.DataAccessLayer
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _context;

        public IRepository<Book> Books { get; private set; }
        public IRepository<Author> Authors { get; private set; }
        public IRepository<Genre> Genres { get; private set; }
        public IRepository<Publisher> Publishers { get; private set; }

        public UnitOfWork(ApplicationContext context)
        {
            _context = context;
            Books = new BookRepository(_context);
            Authors = new AuthorRepository(_context);
            Genres = new GenreRepository(_context);
            Publishers = new PublisherRepository(_context);
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
