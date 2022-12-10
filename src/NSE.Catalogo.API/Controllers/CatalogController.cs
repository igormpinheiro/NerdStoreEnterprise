using Microsoft.AspNetCore.Mvc;
using NSE.Catalogo.API.Models;

namespace NSE.Catalogo.API.Controllers;

[ApiController]
public class CatalogController : Controller
{
    private readonly IProductRepository _productRepository;

    public CatalogController(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    [HttpGet("catalog/products")]
    public async Task<IEnumerable<Product>> Index()
    {
        return await _productRepository.GetAll();
    }

    [HttpGet("catalog/products/{id}")]
    public async Task<Product> Details(Guid id)
    {
        return await _productRepository.GetById(id);
    }

    [HttpPost("catalog/products")]
    public async Task<ActionResult<Product>> Create(Product product)
    {
        _productRepository.Add(product);
        //await _productRepository.UnitOfWork.Commit();

        return CreatedAtAction(nameof(Details), new { id = product.Id }, product);
    }

    [HttpPut("catalog/products/{id}")]
    public async Task<ActionResult<Product>> Edit(Guid id, Product product)
    {
        if (id != product.Id)
        {
            return BadRequest();
        }

        _productRepository.Update(product);
        //await _productRepository.UnitOfWork.Commit();

        return product;
    }
}