using NSE.WebApp.MVC.Extensions;
using NSE.WebApp.MVC.Services;
using NSE.WebApp.MVC.Services.Handlers;
using Polly;
using Polly.Extensions.Http;

namespace NSE.WebApp.MVC.Configurations;

public static class DependencyInjectionConfig
{
    public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
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
            .AddTypedClient(Refit.RestService.For<ICatalogServiceRefit>)
            //.AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(3, _ => TimeSpan.FromMilliseconds(600)));
            .AddPolicyHandler(PollyExtensions.WaitAndRetryAsync())
            .AddTransientHttpErrorPolicy(p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));

        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        services.AddScoped<IUser, AspNetUser>();
    }
}

public static class PollyExtensions
{
    public static IAsyncPolicy<HttpResponseMessage> WaitAndRetryAsync()
    {
        var retry = HttpPolicyExtensions
            .HandleTransientHttpError()
            .WaitAndRetryAsync(new[]
            {
                TimeSpan.FromSeconds(1),
                TimeSpan.FromSeconds(5),
                TimeSpan.FromSeconds(10)
            }, (exception, timeSpan, retryCount, context) =>
            {
                Console.WriteLine($"Retry {retryCount} of {context.PolicyKey} at {context.OperationKey}, due to: {exception}.");
            });

        return retry;
    }
}