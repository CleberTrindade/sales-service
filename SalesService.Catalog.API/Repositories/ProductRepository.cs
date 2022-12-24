using Microsoft.Extensions.Hosting;
using MongoDB.Bson;
using MongoDB.Driver;
using SalesService.Catalog.API.Data;
using SalesService.Catalog.API.Entities;

namespace SalesService.Catalog.API.Repositories;

public class ProductRepository : IProductRepository
{

	private readonly ICatalogContext _catalogContext;
	public ProductRepository(ICatalogContext catalogContext)
	{
		_catalogContext = catalogContext;
	}

	public async Task CreateProduct(Product product)
	{
		await _catalogContext.Products.InsertOneAsync(product);
	}

	public async Task<bool> DeleteProduct(string id)
	{
		FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Id, id);

		DeleteResult deleteResult = await _catalogContext.Products.DeleteOneAsync(filter);

		return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
	}

	public async Task<IEnumerable<Product>> GetProductByCategory(string categoryName)
	{

		/*var query = Query.In("Titulo", new BsonArray {
						FindAll no MongoDB", "Um terceiro post sobre MongoDB"}
						);
		var todosPosts = posts.Find(query);*/

		var results =
			from prod in _catalogContext.Products.AsQueryable()
			where prod.Category.Contains(categoryName)
			select new { prod.Id, prod.Name, prod.Description, prod.Category, prod.Price, prod.Image };

		List<Product> Product = new List<Product>();

		foreach (var p in results)
		{
			Product.Add(new Product() { Id = p.Id, Name = p.Name, Description = p.Description, 
									    Category = p.Category, Price = p.Price, Image = p.Image
			});

		}

		return Product;

		//FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Category, categoryName);
		//return await _catalogContext.Products.Find(filter).ToListAsync();
	}

	public async Task<Product> GetProductById(string id)
	{
		return await _catalogContext.Products.Find(p => p.Id == id).FirstOrDefaultAsync();
	}

	public async Task<IEnumerable<Product>> GetProductByName(string name)
	{
		FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Name, name);

		return await _catalogContext.Products.Find(filter).ToListAsync();
	}

	public async Task<IEnumerable<Product>> GetProducts()
	{
		return await _catalogContext.Products.Find(p => true).ToListAsync();
	}

	public async Task<bool> UpdateProduct(Product product)
	{
		var updateProduct = await _catalogContext.Products.ReplaceOneAsync(
						filter: p => p.Id == product.Id, replacement: product);

		return updateProduct.IsAcknowledged && updateProduct.ModifiedCount > 0;
	}
}
