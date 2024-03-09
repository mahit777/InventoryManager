using System;
using InventoryCalculator.Data;

namespace Tests
{
	public class InventoryTestHelper
	{
        // Helper method to create a hierarchical structure with n levels of products
        public Product CreateProductHierarchy(int levels)
        {
            if (levels == 0)
            {
                return new Product
                {
                    Name = "Leaf Product",
                    InventoryCount = 100,
                    QuantityNeeded = 1 // Assuming all leaf products have a quantity needed of 1
                };
            }
            else
            {
                return new Product
                {
                    Name = $"Level {levels} Product",
                    Children = new List<Product> { CreateProductHierarchy(levels - 1) }
                };
            }
        }

        public Product CreateLaptop()
        {
            // Define components of the laptop
            var cpu = new Product
            {
                Name = "CPU",
                InventoryCount = 1, // Assuming we have 1 CPU available
                QuantityNeeded = 1 // One CPU needed per laptop
            };

            var memory = new Product
            {
                Name = "Memory",
                InventoryCount = 2, // Assuming we have 2 memory modules available
                QuantityNeeded = 2 // Two memory modules needed per laptop
            };

            var storage = new Product
            {
                Name = "Storage",
                InventoryCount = 1, // Assuming we have 1 storage unit available
                QuantityNeeded = 1 // One storage unit needed per laptop
            };

            var display = new Product
            {
                Name = "Display",
                InventoryCount = 1, // Assuming we have 1 display available
                QuantityNeeded = 1 // One display needed per laptop
            };

            // Assemble the laptop
            var laptop = new Product
            {
                Name = "Laptop",
                Children = new List<Product> { cpu, memory, memory, storage, display } // Assuming 2 memory modules are needed
            };

            return laptop;
        }

        public Product CreateCar()
        {
            // Define components of the car
            var wheel = new Product
            {
                Name = "Wheel",
                InventoryCount = 4, // Assuming we have 4 wheels available
                QuantityNeeded = 1 // One wheel needed per car
            };

            var engine = new Product
            {
                Name = "Engine",
                InventoryCount = 1, // Assuming we have 1 engine available
                QuantityNeeded = 1 // One engine needed per car
            };

            var body = new Product
            {
                Name = "Body",
                InventoryCount = 1, // Assuming we have 1 car body available
                QuantityNeeded = 1 // One body needed per car
            };

            // Assemble the car
            var car = new Product
            {
                Name = "Car",
                Children = new List<Product> { wheel, wheel, engine, body } // Assuming 2 wheels are needed
            };

            return car;
        }
    }
}

