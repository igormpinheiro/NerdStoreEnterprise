using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace NSE.WebAPI.Core.User;

public interface IAspNetUser
{
    string Name { get; }

    Guid GetUserId();

    string GetUserEmail();

    string GetUserToken();

    string GetUserRefreshToken();

    bool IsAuthenticated();

    bool IsInRole(string role);

    IEnumerable<Claim> GetClaims();

    HttpContext GetHttpContext();
}