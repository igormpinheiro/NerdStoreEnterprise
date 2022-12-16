using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authentication;
using NSE.WebAPI.Core.User;
using NSE.WebApp.MVC.Extensions;

namespace NSE.WebApp.MVC.Services.Handlers;

public class HttpClientAuthorizationDelegatingHandler : DelegatingHandler
{
    private IAspNetUser _user;
    
    public HttpClientAuthorizationDelegatingHandler(IAspNetUser user)
    {
        _user = user;
    }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var authorizationHeader = _user.GetHttpContext().Request.Headers["Authorization"];
        
        if(!string.IsNullOrEmpty(authorizationHeader))
        {
            request.Headers.Add("Authorization", new List<string>() { authorizationHeader });
        }
        
        var token = _user.GetUserToken();

        if (token is not null)
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
        
        return base.SendAsync(request, cancellationToken);
    }
}