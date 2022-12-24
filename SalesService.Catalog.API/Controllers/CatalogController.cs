using Microsoft.AspNetCore.Mvc;
using SalesService.Catalog.API.Dto;
using SalesService.Catalog.API.Entities;
using SalesService.Catalog.API.Repositories;

namespace SalesService.Catalog.API.Controllers;

[Route("api/v1/[controller]")]

[ApiController]
public class CatalogController : ControllerBase
{
	private readonly IProductRepository _productRepository;

	public CatalogController(IProductRepository productRepository)
	{
		_productRepository = productRepository;
	}

	[HttpGet]
	public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
	{ 
		var products = await _productRepository.GetProducts();
		return Ok(products);
	}

	[HttpGet("{id:length(24)}", Name = "GetProductById")]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Product))]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<ActionResult<Product>> GetProductById(string id)
	{
		var products = await _productRepository.GetProductById(id);
		if (products is null)
		{
			return NotFound();
		}

		return Ok(products);
	}

	[Route("[action]/{category}", Name = "GetProductByCategory")]
	[HttpGet]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Product>))]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	public async Task<ActionResult<IEnumerable<Product>>> GetProductsByCategory(string category)
	{		
		if (category is null)		
			return BadRequest("Invalid category");

		var products = await _productRepository.GetProductByCategory(category);

		return Ok(products);
	}

	[HttpPost]
	[ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	public async Task<ActionResult<Product>> CreateProduct(CreateProduct product)
	{
		if (product is null)
			return BadRequest("Invalid product");

		await _productRepository.CreateProduct(new Product { Name = product.Name, Category = product.Category, Description = product.Description, Image = product.Image, Price = product.Price });

		return Ok();// CreatedAtRoute("GetProductById", new { id = product.Id}, product);
	}

	[HttpPut]
	[ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	public async Task<ActionResult<Product>> UpdateProduct([FromBody] Product product)
	{
		if (product is null)
			return BadRequest("Invalid product");		

		return Ok(await _productRepository.UpdateProduct(product));
	}

	[HttpDelete("{id:length(24)}", Name = "DeleteProduct")]
	public async Task<ActionResult<Product>> DeleteProduct(string id)
	{
		return Ok(await _productRepository.DeleteProduct(id));
	}
}
