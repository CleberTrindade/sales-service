﻿namespace SalesService.Catalog.API.Dto;

public class CreateProduct
{
	public string Name { get; set; }
	public string Category { get; set; }
	public string Description { get; set; }
	public string Image { get; set; }
	public string Price { get; set; }
}
