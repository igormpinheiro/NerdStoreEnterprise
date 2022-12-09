﻿using System.Security.Claims;

namespace NSE.WebApp.MVC.Extensions;

public interface IUser
{
    string Name { get; }
    
    Guid GetUserId();
    
    string GetUserEmail();
    
    string GetUserToken();
    
    bool IsAuthenticated();
    
    bool HasRole(string role);
    
    IEnumerable<Claim> GetClaims();
    
    HttpContext GetHttpContext();
}

public class AspNetUser : IUser
{
    private readonly IHttpContextAccessor _accessor;
    
    public AspNetUser(IHttpContextAccessor accessor)
    {
        _accessor = accessor;
    }
    
    public string Name => _accessor.HttpContext.User.Identity.Name;
    
    public Guid GetUserId()
    {
        return IsAuthenticated() ? Guid.Parse(_accessor.HttpContext.User.GetUserEmail()) : Guid.Empty;
    }

    public string GetUserEmail()
    {
        return IsAuthenticated() ? _accessor.HttpContext.User.GetUserEmail() : String.Empty;
    }

    public string GetUserToken()
    {
        return IsAuthenticated() ? _accessor.HttpContext.User.GetUserToken() : String.Empty;
    }

    public bool IsAuthenticated()
    {
        return _accessor.HttpContext.User.Identity.IsAuthenticated;
    }

    public bool HasRole(string role)
    {
        return _accessor.HttpContext.User.IsInRole(role);
    }

    public IEnumerable<Claim> GetClaims()
    {
        return _accessor.HttpContext.User.Claims;
    }

    public HttpContext GetHttpContext()
    {
        return _accessor.HttpContext;
    }
}

public static class ClaimsPrincipalExtensions
{
    public static string GetUserId(this ClaimsPrincipal principal)
    {
        if(principal is null) throw new ArgumentException(nameof(principal));
        
        return principal.FindFirstValue("sub");
    }
    public static string GetUserEmail(this ClaimsPrincipal principal)
    {
        if(principal is null) throw new ArgumentException(nameof(principal));
        
        return principal.FindFirstValue("email");
    }

    public static string GetUserToken(this ClaimsPrincipal principal)
    {
        if(principal is null) throw new ArgumentException(nameof(principal));
        
        return principal.FindFirstValue("JWT");
    }
}