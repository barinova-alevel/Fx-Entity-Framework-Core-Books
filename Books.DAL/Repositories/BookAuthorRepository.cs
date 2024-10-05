using System;
using Books.DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace Books.DataAccessLayer.Repositories
{
    public class BookAuthorRepository : IRepository<BookAuthor>
    {
        private readonly ApplicationContext _context;

        public BookAuthorRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BookAuthor>> GetAllAsync() => await _context.BookAuthors.ToListAsync();

        public async Task<BookAuthor> GetByIdAsync(int id) => await _context.BookAuthors.FindAsync(id);

        public async Task AddAsync(BookAuthor bookAuthor)
        {
            await _context.BookAuthors.AddAsync(bookAuthor);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(BookAuthor bookAuthor)
        {
            _context.BookAuthors.Update(bookAuthor);
            await _context.SaveChangesAsync();
        }

        public void Remove(BookAuthor bookAuthor)
        {
            _context.Remove(bookAuthor);
        }
    }
}
