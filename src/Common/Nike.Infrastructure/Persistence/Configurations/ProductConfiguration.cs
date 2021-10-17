using Nike.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Nike.Infrastructure.Persistence.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(t => t.Name)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(t => t.ProductCategoryId)
                .IsRequired();
        }
    }
}
