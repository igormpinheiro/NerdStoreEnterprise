﻿using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using NSE.Carrinho.API.Models;
using NSE.Core.Messages;

namespace NSE.Carrinho.API.Data;

public class ShoppingCartContext : DbContext
{
    public ShoppingCartContext(DbContextOptions<ShoppingCartContext> options) : base(options)
    {
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        ChangeTracker.AutoDetectChangesEnabled = false;
    }

    public DbSet<CartItem> CartItems { get; set; }
    public DbSet<CustomerShoppingCart> CustomerShoppingCart { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Ignore<ValidationResult>();
        modelBuilder.Ignore<Event>();

        foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
            e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
            property.SetColumnType("varchar(100)");

        modelBuilder.Entity<CustomerShoppingCart>()
            .HasIndex(c => c.CustomerId)
            .HasDatabaseName("IDX_Customer");

        modelBuilder.Entity<CustomerShoppingCart>()
            .Ignore(c => c.Voucher)
            .OwnsOne(c => c.Voucher, v =>
            {
                v.Property(vc => vc.Code)
                    .HasColumnType("varchar(50)");

                v.Property(vc => vc.DiscountType);

                v.Property(vc => vc.Percentage);

                v.Property(vc => vc.Discount);
            });

        modelBuilder.Entity<CustomerShoppingCart>()
            .HasMany(c => c.Items)
            .WithOne(i => i.CustomerShoppingCart)
            .HasForeignKey(c => c.ShoppingCartId);

        foreach (var relationship in modelBuilder.Model.GetEntityTypes()
            .SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.Cascade;
    }
}