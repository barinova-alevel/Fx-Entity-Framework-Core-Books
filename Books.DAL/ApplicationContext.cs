
using Books.DataAccessLayer.Configurations;
using Books.DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace Books.DataAccessLayer
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Publisher> Publishers { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) 
        {
            //  Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BookConfiguration());
            modelBuilder.ApplyConfiguration(new AuthorConfiguration());
            modelBuilder.ApplyConfiguration(new GenreConfiguration());
            modelBuilder.ApplyConfiguration(new PublisherConfiguration());
        }
    }
}
