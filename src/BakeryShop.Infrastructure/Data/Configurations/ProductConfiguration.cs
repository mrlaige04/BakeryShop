using BakeryShop.Domain.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BakeryShop.Infrastructure.Data.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.Id);

        builder.HasOne(p => p.Currency)
            .WithMany()
            .HasForeignKey(p => p.CurrencyId);

        builder.ToTable("Products");
    }
}