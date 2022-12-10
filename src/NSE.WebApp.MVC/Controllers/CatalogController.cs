using Microsoft.AspNetCore.Mvc;
using NSE.WebApp.MVC.Services;

namespace NSE.WebApp.MVC.Controllers;

public class CatalogController : MainController
{
    private readonly ICatalogServiceRefit _catalogService;
    
    public CatalogController(ICatalogServiceRefit catalogService)
    {
        _catalogService = catalogService;
    }
    
    [HttpGet]
    [Route("")]
    [Route("vitrine")]
    public async Task<IActionResult> Index()
    {
        var products = await _catalogService.GetProducts();
        return View(products);
    }
    
    [HttpGet]
    [Route("produto-detalhe/{id}")]
    public async Task<IActionResult> ProductDetails(Guid id)
    {
        var product = await _catalogService.GetProductById(id);
        return View(product);
    }
    
}