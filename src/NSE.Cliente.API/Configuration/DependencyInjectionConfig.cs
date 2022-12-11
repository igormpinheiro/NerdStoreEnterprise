using NSE.Cliente.API.Data;
//using NSE.Cliente.API.Data.Repositories;
using NSE.Cliente.API.Models;

namespace NSE.Cliente.API.Configurations;

public static class DependencyInjectionConfig
{
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        //services.AddScoped<IAspNetUser, AspNetUser>();

        //services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<CustomerContext>();
    }
}
