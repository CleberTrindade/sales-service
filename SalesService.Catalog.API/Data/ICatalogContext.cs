using MongoDB.Driver;
using SalesService.Catalog.API.Entities;

namespace SalesService.Catalog.API.Data
{
	public interface ICatalogContext
	{
		IMongoCollection<Product> Products { get; }
	}
}
