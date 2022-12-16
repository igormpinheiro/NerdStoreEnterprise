﻿using NSE.Carrinho.API.Data;

namespace NSE.Carrinho.API.Configurations;

public static class DependencyInjectionConfig
{
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        //services.AddScoped<IAspNetUser, AspNetUser>();
        services.AddScoped<ShoppingCartContext>();
        //services.AddScoped<ShoppingCart>();
    }
}