using NSE.WebApp.MVC.Extensions;
using NSE.WebApp.MVC.Services;
using NSE.WebApp.MVC.Services.Handlers;

namespace NSE.WebApp.MVC.Configurations;

public static class DependencyInjectionConfig
{
    public static void RegisterServices(this IServiceCollection services, IConfiguration configuration )
    {
        services.AddTransient<HttpClientAuthorizationDelegatingHandler>();
        services.AddHttpClient<IAuthService, AuthService>();
        // services.AddHttpClient<ICatalogService, CatalogService>()
        //     .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();
        services.AddHttpClient("Refit", options =>
            {
                options.BaseAddress = new Uri(configuration.GetSection("CatalogUrl").Value);
            })
            .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
            .AddTypedClient(Refit.RestService.For<ICatalogServiceRefit>);
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddScoped<IUser, AspNetUser>();
    }
}