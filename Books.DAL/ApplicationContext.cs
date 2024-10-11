
using Books.DataAccessLayer.Configurations;
using Books.DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;
using conf = Microsoft.Extensions.Configuration;

namespace Books.DataAccessLayer
{
    public class ApplicationContext : DbContext
    {

        public DbSet<Book> Books { get; set; }
        //public DbSet<Author> Authors { get; set; }
        public DbSet<Genre> Genres { get; set; }
        //public DbSet<Publisher> Publishers { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                const string connectionString = "Server=OKSANA_NANGA;Database=BooksDb1;Trusted_Connection=True;TrustServerCertificate=True;";
                optionsBuilder.UseSqlServer(connectionString);
                optionsBuilder.LogTo(message => System.Diagnostics.Debug.WriteLine(message));
            }
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BookConfiguration());
           // modelBuilder.ApplyConfiguration(new AuthorConfiguration());
            modelBuilder.ApplyConfiguration(new GenreConfiguration());
            //modelBuilder.ApplyConfiguration(new PublisherConfiguration());
        }
    }
}
