using System;
using InventoryCalculator.Data;

namespace InventoryCalculator.Services
{
	public interface IProductService
	{

        int TotalProducts(Product rootProduct);

    }
}

