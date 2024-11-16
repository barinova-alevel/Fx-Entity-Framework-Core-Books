using System;
using Books.DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using Serilog;

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

        //public async Task AddUniqueBooksAsync(List<Book> fileBooks)
        //{
        //    List<Book> uniqueBooks = new List<Book>();
        //    using var context = new ApplicationContext();
        //    Log.Information("Checking presence of books that are not previously added to db.");

        //    foreach (var book in fileBooks)
        //    {
        //        bool exists = await context.Books
        //            .Include(b => b.Author)
        //            .AnyAsync(b => b.Title.ToLower().Trim() == book.Title.ToLower().Trim()
        //                && b.Author.Name.ToLower().Trim() == book.Author.Name.ToLower().Trim());

        //        if (!exists)
        //        {
        //            Log.Information($"{book.Title}, {book.Author.Name}");
        //            uniqueBooks.Add(book);
        //        }
        //    }
        //    Log.Information($"{uniqueBooks.Count} books is adding.");
        //    await AddRangeAsync(uniqueBooks);
        //}
        public void Remove(Book book)
        {
            _context.Remove(book);
        }
    }
}
