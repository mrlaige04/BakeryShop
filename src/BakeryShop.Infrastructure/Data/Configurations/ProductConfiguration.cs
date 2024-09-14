using BakeryShop.Domain.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BakeryShop.Infrastructure.Data.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.Id);


        builder.HasMany(p => p.Information)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);

        builder.ToTable("Products");
    }
}