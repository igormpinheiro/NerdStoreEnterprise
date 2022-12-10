using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSE.Catalogo.API.Models;
using NSE.WebAPI.Core.Identity;

namespace NSE.Catalogo.API.Controllers;

[ApiController]
[Authorize]
public class CatalogController : Controller
{
    private readonly IProductRepository _productRepository;

    public CatalogController(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    [AllowAnonymous]
    [HttpGet("catalog/products")]
    public async Task<IEnumerable<Product>> Index()
    {
        return await _productRepository.GetAll();
    }

    [ClaimAuthorize("catalog", "read")]
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