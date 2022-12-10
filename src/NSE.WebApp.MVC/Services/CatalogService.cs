using Microsoft.Extensions.Options;
using NSE.WebApp.MVC.Extensions;
using NSE.WebApp.MVC.Models;

namespace NSE.WebApp.MVC.Services;

public class CatalogService : Services, ICatalogService
{
    private readonly HttpClient _httpClient;

    public CatalogService(HttpClient httpClient, IOptions<AppSettings> settings)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(settings.Value.CatalogUrl);
    }

    public async Task<IEnumerable<ProductViewModel>> GetProducts()
    {
        var response = await _httpClient.GetAsync("/catalog/products/");
        
        TratarErrosResponse(response);
        
        return await DeserializeObjectResponse<IEnumerable<ProductViewModel>>(response);
    }

    public async Task<ProductViewModel> GetProductById(Guid id)
    {
        var response = await _httpClient.GetAsync($"/catalog/products/{id}");
        
        TratarErrosResponse(response);
        
        return await DeserializeObjectResponse<ProductViewModel>(response);
    }
}