
using Books.DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Books.DataAccessLayer.Configurations
{
    public class BookAuthorConfiguration : IEntityTypeConfiguration<BookAuthor>
    {
        public void Configure(EntityTypeBuilder<BookAuthor> builder)
        {
            builder.ToTable("BookAuthor").HasKey(ba => ba.Id);
            builder.Property(p => p.Id).HasColumnName("BookAuthorId");
            builder.Property(p => p.BookId); //skip HasColumnName to see if BookId has added automatically
            builder.Property(p => p.AuthorId).HasColumnName("AuthorId");

            builder.HasOne(ba => ba.Book)
                .WithMany(b => b.BookAuthors)
                .HasForeignKey(ba => ba.BookId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ba => ba.Author)
                .WithMany(a => a.BookAuthors)
                .HasForeignKey(ba => ba.AuthorId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
