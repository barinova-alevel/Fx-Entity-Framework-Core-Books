using Books.DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace Books.DataAccessLayer.Repositories
{
    public class AuthorRepository : IRepository<Author>
    {
        private readonly ApplicationContext _context;

        public AuthorRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Author>> GetAllAsync() => await _context.Authors.ToListAsync();

        public async Task<Author> GetByIdAsync(int id) => await _context.Authors.FindAsync(id);

        public async Task AddAsync(Author author)
        {
            await _context.Authors.AddAsync(author);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Author author)
        {
            _context.Authors.Update(author);
            await _context.SaveChangesAsync();
        }

        public void Remove(Author author)
        {
            _context.Remove(author);
        }
    }
}
