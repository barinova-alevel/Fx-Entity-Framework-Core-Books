
using System.Reflection.Emit;
using Books.DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Books.DataAccessLayer.Configurations
{
    public class GenreConfiguration : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder.ToTable("Genres").HasKey(g => g.Id);
            builder.Property(p => p.Id).IsRequired().HasColumnName("GenreId");
            builder.Property(p => p.Name).IsRequired().HasColumnName("Name");
        }
    }
}
