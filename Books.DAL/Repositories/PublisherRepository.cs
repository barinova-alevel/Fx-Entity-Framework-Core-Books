
using Books.DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace Books.DataAccessLayer.Repositories
{
    //public class PublisherRepository : IRepository<Publisher>
    //{
    //    private readonly ApplicationContext _context;

    //    public PublisherRepository(ApplicationContext context)
    //    {
    //        _context = context;
    //    }

    //    public async Task<IEnumerable<Publisher>> GetAllAsync() => await _context.Publishers.ToListAsync();

    //    public async Task<Publisher> GetByIdAsync(int id) => await _context.Publishers.FindAsync(id);

    //    public async Task AddAsync(Publisher publisher)
    //    {
    //        await _context.Publishers.AddAsync(publisher);
    //        await _context.SaveChangesAsync();
    //    }

    //    public async Task UpdateAsync(Publisher publisher)
    //    {
    //        _context.Publishers.Update(publisher);
    //        await _context.SaveChangesAsync();
    //    }

    //    public void Remove(Publisher publisher)
    //    {
    //        _context.Remove(publisher);
    //    }
    //}
}
