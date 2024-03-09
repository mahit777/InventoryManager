using System;
using System.Collections.Generic;
using InventoryCalculator.Data;

namespace InventoryCalculator.Services
{
    public class ProductService : IProductService
    {
        public int TotalProducts(Product rootProduct)
        {
           
            if (rootProduct == null)
                return 0;
            Console.WriteLine($"{rootProduct.Name} is being calculated");
            if (rootProduct.Children == null || rootProduct.Children.Count == 0)
            {
                // If the product is a leaf product, calculate the maximum number of root products
                Console.WriteLine($"{rootProduct.Name} is a leaf product");

                var qty = rootProduct.QuantityNeeded == 0 ? 0 : rootProduct.InventoryCount / rootProduct.QuantityNeeded;
                Console.WriteLine($"{rootProduct.Name} qty is {qty}");

                return qty;

            }

            int maxProducts = 0;

            foreach (var child in rootProduct.Children)
            {

                

                // Recursively compute the maximum number of root products for the child
                int childCount = TotalProducts(child);

                child.InventoryCount = childCount;
                // Calculate the available count of child products based on inventory count and quantity needed

                // Update the maximum number of root products based on the child's available count
                maxProducts = maxProducts == 0 ? child.InventoryCount : Math.Min(maxProducts, child.InventoryCount);
            }

            return maxProducts;
        }


    }
}