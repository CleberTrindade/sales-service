using MongoDB.Driver;
using SalesService.Catalog.API.Entities;

namespace SalesService.Catalog.API.Data;

public class CatalogContext : ICatalogContext
{
	public IMongoCollection<Product> Products { get; }

	public CatalogContext(IConfiguration configuration)
	{
		var _com = configuration.GetValue<string>("DatabaseSettings:ConnectionString");
		var _db = configuration.GetValue<string>("DatabaseSettings:DatabaseName");
		var _co = configuration.GetValue<string>("DatabaseSettings:CollectionName");

		var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
		var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));
		Products = database.GetCollection<Product>(configuration.GetValue<string>("DatabaseSettings:CollectionName"));
	}

	

	
}
