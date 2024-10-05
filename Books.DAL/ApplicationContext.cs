
using Books.DataAccessLayer.Configurations;
using Books.DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Books.DataAccessLayer
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<BookAuthor> BookAuthors { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Publisher> Publishers { get; set; }

        public ApplicationContext()
        { 
        }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) 
        {
            //  Database.EnsureCreated();
        }

       //protected override void OnConfiguring(DbContextOptionsBuilder dBContextOptionsBuilder)
       //     => dBContextOptionsBuilder.UseSqlServer("Server=OKSANA_NANGA;Database=BooksDb;Trusted_Connection=True;TrustServerCertificate=True;");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Book>(entity =>
            //{

            //    entity.ToTable("Books");
            //    entity.HasKey(p => p.Id).HasName("Books");

            //    entity.Property(p => p.Id)

            //    .HasColumnName("id")

            //    .HasColumnType("GUID").ValueGeneratedNever();

            //    entity.Property(p => p.Title)

            //    .HasColumnName("title");

            //});
            modelBuilder.Entity<Book>().HasKey("BookId");
            modelBuilder.ApplyConfiguration(new BookConfiguration());
            modelBuilder.ApplyConfiguration(new AuthorConfiguration());
            modelBuilder.ApplyConfiguration(new GenreConfiguration());
            modelBuilder.ApplyConfiguration(new PublisherConfiguration());
            modelBuilder.ApplyConfiguration(new BookAuthorConfiguration());
        }
    }
}
