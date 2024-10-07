
using Books.DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Books.DataAccessLayer.Configurations
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.ToTable("Books").HasKey(b => b.Id);
            builder.Property(p => p.Id).IsRequired().HasColumnName("id"); //ValueGeneratedOnAdd();
            builder.Property(p => p.Title).IsRequired().HasColumnName("title").HasMaxLength(255);
            builder.Property(p => p.Pages).IsRequired().HasColumnName("pages");
            builder.Property(p => p.GenreId).IsRequired().HasColumnName("genreId");
            builder.Property(p => p.AuthorId).IsRequired().HasColumnName("authorId");
            builder.Property(p => p.PublisherId).IsRequired().HasColumnName("publisherId");
            builder.Property(p => p.ReleaseDate).IsRequired().HasColumnName("releaseDate");

            builder.HasOne(b => b.Genre)
                .WithMany(g => g.Books)
                .HasForeignKey(b => b.GenreId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(b => b.Publisher)
                .WithMany(p => p.Books)
                .HasForeignKey(b => b.PublisherId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
