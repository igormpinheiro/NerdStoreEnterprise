using Microsoft.Extensions.Options;
using NSE.WebApp.MVC.Extensions;
using NSE.WebApp.MVC.Models;

namespace NSE.WebApp.MVC.Services;

public class AuthService : Services, IAuthService
{
    private readonly HttpClient _httpClient;

    public AuthService(HttpClient httpClient, IOptions<AppSettings> settings)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri($"{settings.Value.AuthenticationUrl}/api/identity/");
    }

    public async Task<UserLoginResponse> Login(UserLogin user)
    {
        var endpoint = "auth";

        var loginContent = GetContent(user);

        var response = await _httpClient.PostAsync(endpoint, loginContent);

        if (!TratarErrosResponse(response))
        {
            return new UserLoginResponse
            {
                ResponseResult = await DeserializeObjectResponse<ResponseResult>(response)
            };
        }

        return await DeserializeObjectResponse<UserLoginResponse>(response);
    }

    public async Task<UserLoginResponse> Register(UserRegister user)
    {
        var endpoint = "register";

        var loginContent = GetContent(user);

        var response = await _httpClient.PostAsync(endpoint, loginContent);

        if (!TratarErrosResponse(response))
        {
            return new UserLoginResponse
            {
                ResponseResult = await DeserializeObjectResponse<ResponseResult>(response)
            };
        }

        return await DeserializeObjectResponse<UserLoginResponse>(response);
    }
}