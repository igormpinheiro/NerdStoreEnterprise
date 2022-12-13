using Microsoft.Extensions.DependencyInjection;

namespace NSE.MessageBus;

public static class DepedencyInjectionExtensions
{
    public static IServiceCollection AddMessageBus(this IServiceCollection services, string connectionString)
    {
        if (services == null) throw new ArgumentNullException(nameof(services));

        if (string.IsNullOrEmpty(connectionString)) throw new ArgumentNullException(nameof(connectionString));

        services.AddSingleton<IMessageBus, MessageBus>(sp => new MessageBus(connectionString));
        return services;
    }
}