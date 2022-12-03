using Microsoft.AspNetCore.Authentication.Cookies;

namespace NSE.WebApp.MVC.Configurations;

public static class IdentityConfig
{
    public static void AddIdentityConfiguration(this IServiceCollection services)
    {
        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.LoginPath = "/login";
                options.AccessDeniedPath = "/acesso-negado";
            });
        
        // if (services == null) throw new ArgumentNullException(nameof(services));
        //
        // services.AddIdentity<IdentityUser, IdentityRole>()
        //     .AddEntityFrameworkStores<ApplicationDbContext>()
        //     .AddDefaultTokenProviders();
        //
        // // Cookie
        // services.ConfigureApplicationCookie(options =>
        // {
        //     options.LoginPath = "/login";
        //     options.LogoutPath = "/logout";
        //     options.AccessDeniedPath = "/acesso-negado";
        //     options.SlidingExpiration = true;
        //     options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
        //     options.Cookie = new CookieBuilder
        //     {
        //         HttpOnly = true,
        //         Name = ".AspNetCore.Identity.Application",
        //         SameSite = SameSiteMode.Strict
        //     };
        // });
    }
    
    public static void UseIdentityConfiguration(this IApplicationBuilder app)
    {
        app.UseAuthentication();
        app.UseAuthorization();
    }
    
}