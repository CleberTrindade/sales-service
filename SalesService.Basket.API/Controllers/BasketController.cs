using Microsoft.AspNetCore.Mvc;
using SalesService.Basket.API.Entities;
using SalesService.Basket.API.Repositories;

namespace SalesService.Basket.API.Controllers;


[Route("api/v1/[controller]")]
[ApiController]
public class BasketController : ControllerBase
{
	private readonly IBasketRepository _repository;

	public BasketController(IBasketRepository repository)
	{
		_repository = repository;
	}

	[HttpGet("{userName}", Name = "GetBasket")]
	public async Task<ActionResult<ShoppingCart>> GetBasket(string userName)
	{
		var basket = await _repository.GetBasket(userName);

		return Ok(basket ?? new ShoppingCart(userName));
	}

	[HttpPut]
	public async Task<ActionResult<ShoppingCart>> UpdateBasket([FromBody] ShoppingCart basket)
	{
		return Ok(await _repository.UpdateBasket(basket));
	}

	[HttpDelete("{userName}", Name = "DeleteBasket")]
	public async Task<ActionResult> DeleteBasket(string userName)
	{
		await _repository.DeleteBasket(userName);
		return Ok();
	}

}
