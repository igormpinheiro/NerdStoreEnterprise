using NSE.WebApp.MVC.Models;
using Refit;

namespace NSE.WebApp.MVC.Services;

public interface ICatalogService
{
    Task<IEnumerable<ProductViewModel>> GetProducts();
    
    Task<ProductViewModel> GetProductById(Guid id);
    
}

public interface ICatalogServiceRefit
{
    [Get("/catalog/products")]
    Task<IEnumerable<ProductViewModel>> GetProducts();
    
    [Get("/catalog/products/{id}")]
    Task<ProductViewModel> GetProductById(Guid id);
}