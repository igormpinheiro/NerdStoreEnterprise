using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NSE.Catalogo.API.Models;

namespace NSE.Catalogo.API.Data.Mappings;

public class ProductMap : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .IsRequired()
            .HasColumnType("varchar(250)");

        builder.Property(p => p.Description)
            .IsRequired()
            .HasColumnType("varchar(1000)");

        builder.Property(p => p.Image)
            .IsRequired()
            .HasColumnType("varchar(250)");

        builder.ToTable("Produtos");
    }
}

