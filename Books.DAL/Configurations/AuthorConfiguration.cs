
using Books.DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Books.DataAccessLayer.Configurations
{
    public class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.ToTable("Authors").HasKey(a => a.Id);
            builder.Property(a => a.Id).IsRequired().HasColumnName("AuthorId");
            builder.Property(a => a.Name).IsRequired().HasColumnName("Name");
        }

    }
}