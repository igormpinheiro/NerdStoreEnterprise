﻿namespace NSE.Carrinho.API.Configurations;

public static class MessageBusConfig
{
    public static void AddMessageBusConfiguration(this IServiceCollection services,
            IConfiguration configuration)
    {
        //services.AddMessageBus(configuration.GetMessageQueueConnection("MessageBus"))
        //    .AddHostedService<ShoppingCartIntegrationHandler>();
    }
}