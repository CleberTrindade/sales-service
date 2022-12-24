using MongoDB.Driver;
using SalesService.Catalog.API.Entities;

namespace SalesService.Catalog.API.Data;

public class CatalogContextSeed
{
	public static void SeedData(IMongoCollection<Product> productCollection)
	{
		bool existProduct = productCollection.Find(p => true).Any();

		if (!existProduct) {
			productCollection.InsertManyAsync(GetMyProducts());
		}
	}

	private static IEnumerable<Product> GetMyProducts()
	{
		throw new NotImplementedException();
	}
}
