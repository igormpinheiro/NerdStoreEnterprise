using NSE.WebApp.MVC.Models;
using System.Text;
using System.Text.Json;

namespace NSE.WebApp.MVC.Services;

public class AuthService : Services, IAuthService
{
	private readonly HttpClient _httpClient;
	private readonly string url = "https://localhost:7102/api/identity/";

	public AuthService(HttpClient httpClient)
	{
		_httpClient = httpClient;
	}

	public async Task<UserLoginResponse> Login(UserLogin user)
	{
		var endpoint = "auth";

		var loginContent = new StringContent(
			JsonSerializer.Serialize(user),
			Encoding.UTF8,
			"application/json");

		var response = await _httpClient.PostAsync(url + endpoint, loginContent);

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
		var endpoint = "register";

		var loginContent = new StringContent(
			JsonSerializer.Serialize(user),
			Encoding.UTF8,
			"application/json");

		var response = await _httpClient.PostAsync(url + endpoint, loginContent);

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