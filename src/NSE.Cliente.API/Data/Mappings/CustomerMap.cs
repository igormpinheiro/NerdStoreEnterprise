using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NSE.Cliente.API.Models;
using NSE.Core.DomainObjects;

namespace NSE.Cliente.API.Data.Mappings;

public class CustomerMap : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name)
            .IsRequired()
            .HasColumnType("varchar(200)");

        builder.OwnsOne(c => c.Cpf, tf =>
        {
            tf.Property(c => c.Number)
                .IsRequired()
                .HasMaxLength(CPF.CpfMaxLength)
                .HasColumnName("Cpf")
                .HasColumnType($"varchar({CPF.CpfMaxLength})");
        });

        //Property(c => c.Cpf).IsRequired()
        //    .HasColumnType($"varchar(50)");

        builder.OwnsOne(c => c.Email, tf =>
        {
            tf.Property(c => c.Address)
                .IsRequired()
                .HasColumnName("Email")
                .HasColumnType($"varchar({Email.AddressMaxLength})");
        });

        // 1 : 1 => Customer : Address
        builder.HasOne(c => c.Address)
            .WithOne(c => c.Customer);

        builder.ToTable("Customer");
    }
}