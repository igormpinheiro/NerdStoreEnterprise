using MediatR;
using Microsoft.EntityFrameworkCore;
using NSE.Cliente.API.Models;
using NSE.Core.Data;
using NSE.Core.DomainObjects;
using NSE.Core.Mediator;
using NSE.Core.Messages;
using System.ComponentModel.DataAnnotations;

namespace NSE.Cliente.API.Data;

public sealed class CustomerContext : DbContext, IUnitOfWork
{
    private readonly IMediatorHandler _mediatorHandler;
    private readonly IMediator _mediator;

    public CustomerContext(DbContextOptions<CustomerContext> options, IMediator mediator)
        : base(options)
    {
        _mediator = mediator;
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        ChangeTracker.AutoDetectChangesEnabled = false;
    }

    public DbSet<Customer> Customers { get; set; }
    public DbSet<Address> Addresses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Ignore<ValidationResult>();
        modelBuilder.Ignore<Event>();

        foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
            e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
            property.SetColumnType("varchar(100)");

        foreach (var relationship in modelBuilder.Model.GetEntityTypes()
            .SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CustomerContext).Assembly);
    }

    public async Task<bool> Commit()
    {
        var success = await base.SaveChangesAsync() > 0;
        if (success) await _mediator.PublishEvents(this);

        return success;
    }
}

public static class MediatorExtension
{
    public static async Task PublishEvents<T>(this IMediator mediator, T ctx) where T : DbContext
    {

        var domainEntities = ctx.ChangeTracker
            .Entries<Entity>()
            .Where(x => x.Entity.Notifications != null && x.Entity.Notifications.Any());
        
        var domainEvents = domainEntities
            .SelectMany(x => x.Entity.Notifications)
            .ToList();

        domainEntities.ToList()
            .ForEach(entity => entity.Entity.ClearEvents());

        foreach (var task in domainEvents)
        {
            await mediator.Publish(task);
        }
    }
}