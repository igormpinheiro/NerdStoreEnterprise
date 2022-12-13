using FluentValidation.Results;
using MediatR;
using NSE.Cliente.API.Application.Commands;
using NSE.Cliente.API.Application.Events;
using NSE.Cliente.API.Data;
using NSE.Cliente.API.Data.Repositories;
using NSE.Cliente.API.Models;
using NSE.Cliente.API.Services;
using NSE.Core.Mediator;

namespace NSE.Cliente.API.Configurations;

public static class DependencyInjectionConfig
{
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IMediatorHandler, MediatorHandler>();
        services.AddScoped<IRequestHandler<NewCustomerCommand, ValidationResult>, CustomerCommandHandler>();
        services.AddScoped<INotificationHandler<NewCustomerAddedEvent>, CustomerEventHandler>();

        services.AddHostedService<UserRegisteredIntegrationHandler>();

        //services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        //services.AddScoped<IAspNetUser, AspNetUser>();

        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<CustomerContext>();
    }
}
