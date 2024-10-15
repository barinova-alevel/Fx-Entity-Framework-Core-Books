
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
            builder.Property(p => p.Id).IsRequired().HasColumnName("Id");
            builder.Property(p => p.Title).IsRequired().HasColumnName("Title").HasMaxLength(255);
            builder.Property(p => p.Pages).IsRequired().HasColumnName("Pages");
            builder.Property(p => p.GenreId).IsRequired().HasColumnName("GenreId");
            builder.Property(p => p.PublisherId).IsRequired().HasColumnName("PublisherId");
            builder.Property(p => p.ReleaseDate).IsRequired().HasColumnName("ReleaseDate");

            builder.HasOne(b => b.Genre)
                .WithMany(p => p.Books)
                .HasForeignKey(b => b.GenreId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(b => b.Author)
                .WithMany(p => p.Books)
                .HasForeignKey(b => b.AuthorId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(b => b.Publisher)
                .WithMany(p => p.Books)
                .HasForeignKey(b => b.PublisherId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
