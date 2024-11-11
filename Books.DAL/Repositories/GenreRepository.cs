
using System;
using Books.DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace Books.DataAccessLayer.Repositories
{
    public class GenreRepository : IRepository<Genre>
    {
        private readonly ApplicationContext _context;

        public GenreRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Genre>> GetAllAsync() => await _context.Genres.ToListAsync();

        public async Task<Genre> GetByIdAsync(int id) => await _context.Genres.FindAsync(id);

        public async Task AddAsync(Genre genre)
        {
            await _context.Genres.AddAsync(genre);
            await _context.SaveChangesAsync();
        }

        public async Task AddRangeAsync(IEnumerable<Genre> genres)
        {
            await _context.Genres.AddRangeAsync(genres);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Genre genre)
        {
            _context.Genres.Update(genre);
            await _context.SaveChangesAsync();
        }

        public void Remove(Genre genre)
        {
            // _dbSet.Remove(book);
            _context.Remove(genre);
            //save changes?
        }
    }
}
