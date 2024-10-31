using Books.DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Books.DataAccessLayer.Configurations
{
    public class PublisherConfiguration : IEntityTypeConfiguration<Publisher>
    {
        public void Configure(EntityTypeBuilder<Publisher> builder)
        {
            builder.ToTable("publishers").HasKey(p => p.Id);
            builder.Property(a => a.Id).IsRequired().HasColumnName("PublisherId"); 
            builder.Property(p => p.Name).IsRequired().HasColumnName("Name");
        }
    }
}
