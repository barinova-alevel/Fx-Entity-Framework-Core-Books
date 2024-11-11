using System;
using Books.DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace Books.DataAccessLayer.Repositories
{
    public class BookRepository : IRepository<Book>
    {
        private readonly ApplicationContext _context;

        public BookRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Book>> GetAllAsync() => await _context.Books.ToListAsync();

        public async Task<Book> GetByIdAsync(int id) => await _context.Books.FindAsync(id);

        public async Task AddAsync(Book book)
        {
            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
        }

        public async Task AddRangeAsync(IEnumerable<Book> books)
        {
            await _context.Books.AddRangeAsync(books);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Book book)
        {
            _context.Books.Update(book);
            await _context.SaveChangesAsync();
        }

        public void Remove(Book book)
        {
           // _dbSet.Remove(book);
            _context.Remove(book);
            //save changes?
        }

        //public async Task DeleteAsync(int id)
        //{
        //    var book = await _context.Books.FindAsync(id);
        //    if (book != null)
        //    {
        //        _context.Books.Remove(book);
        //        await _context.SaveChangesAsync();
        //    }
        //}
    }
}
