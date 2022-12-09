using NSE.WebApp.MVC.Extensions;
using NSE.WebApp.MVC.Models;
using System.Text;
using System.Text.Json;

namespace NSE.WebApp.MVC.Services;

public abstract class Services
{
    protected bool TratarErrosResponse(HttpResponseMessage response)
    {
        switch ((int)response.StatusCode)
        {
            case 401:
            case 403:
            case 404:
            case 500:
                throw new CustomHttpRequestException(response.StatusCode);
            case 400:
                return false;
            default:
                response.EnsureSuccessStatusCode();
                return true;
        }
    }

    protected StringContent GetContent(object data)
    {
        return new StringContent(
            JsonSerializer.Serialize(data),
            Encoding.UTF8,
            "application/json");
    }

    protected async Task<T> DeserializeObjectResponse<T>(HttpResponseMessage response)
    {
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var json = await response.Content.ReadAsStringAsync();
        
        return JsonSerializer.Deserialize<T>(json, options);

    }

}