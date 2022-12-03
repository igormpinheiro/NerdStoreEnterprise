using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace NSE.WebApp.Blazor.Configurations;

public static class IdentityConfig
{
    public static void AddIdentityConfiguration(this IServiceCollection services)
    {
        services.AddOptions();
        services.AddAuthorizationCore();
        
    }
    
    public static void UseIdentityConfiguration(this WebAssemblyHostBuilder app)
    {
        // app.UseAuthentication();
        // app.UseAuthorization();
    }
    
}