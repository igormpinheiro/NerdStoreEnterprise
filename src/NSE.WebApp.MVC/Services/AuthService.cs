using System.Text;
using System.Text.Json;
using NSE.WebApp.MVC.Models;

namespace NSE.WebApp.MVC.Services;

public class AuthService : Services, IAuthService
{
    private readonly HttpClient _httpClient;


    public AuthService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<UserLoginResponse> Login(UserLogin user)
    {
        var url = "https://localhost:44333/api/identidade/autenticar";
        var loginContent = new StringContent(
            JsonSerializer.Serialize(user),
            Encoding.UTF8,
            "application/json");
        
        var response = await _httpClient.PostAsync(url,loginContent);

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        
        if (!TratarErrosResponse(response))
        {
            return new UserLoginResponse()
            {
                ResponseResult =
                    JsonSerializer.Deserialize<ResponseResult>(await response.Content.ReadAsStringAsync(), options)
            };
        }
        
        return JsonSerializer.Deserialize<UserLoginResponse>(await response.Content.ReadAsStringAsync(), options);
    }

    public async Task<UserLoginResponse> Register(UserRegister user)
    {
        var url = "https://localhost:44333/api/identidade/nova-conta";
        
        var loginContent = new StringContent(
            JsonSerializer.Serialize(user),
            Encoding.UTF8,
            "application/json");
        
        var response = await _httpClient.PostAsync(url,loginContent);
        
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        
        if (!TratarErrosResponse(response))
        {
            return new UserLoginResponse()
            {
                ResponseResult =
                    JsonSerializer.Deserialize<ResponseResult>(await response.Content.ReadAsStringAsync(), options)
            };
        }
        
        return JsonSerializer.Deserialize<UserLoginResponse>(await response.Content.ReadAsStringAsync(), options);
    }
}