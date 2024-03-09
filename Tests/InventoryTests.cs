using NUnit.Framework;
using InventoryCalculator.Services;
using InventoryCalculator.Data;
using System.Collections.Generic;
using Tests;

namespace InventoryCalculator.Tests
{
    [TestFixture]
    public class ProductServiceTests
    {
        private ProductService productService;
        private InventoryTestHelper testHelper;

        [SetUp]
        public void Setup()
        {
            productService = new ProductService();
            testHelper = new InventoryTestHelper();
        }

        [Test]
        public void MaxRootProducts_LeafProduct_ReturnsExpectedResult()
        {
            // Arrange
            var leafProduct = new Product
            {
                Name = "Leaf Product",
                InventoryCount = 100,
                QuantityNeeded = 10
            };

            // Act
            int result = productService.TotalProducts(leafProduct);

            // Assert
            Assert.AreEqual(10, result);
        }


        [Test]
        public void TotalProducts_NonLeafProduct_ReturnsExpectedResult()
        {
            // Arrange
            var rootProduct = new Product
            {
                Name = "Root Product",
                Children = new List<Product>
            {
                new Product { Name = "Child 1", InventoryCount = 50, QuantityNeeded = 5 },
                new Product { Name = "Child 2", InventoryCount = 60, QuantityNeeded = 6 }
            }
            };

            // Act
            int result = productService.TotalProducts(rootProduct);

            // Assert
            Assert.AreEqual(10, result); // Assuming both children have the same quantity needed
        }

        [Test]
        public void TotalProducts_TwoLevels_ReturnsExpectedResult()
        {
            // Arrange
            var rootProduct = new Product
            {
                Name = "Root Product",
                QuantityNeeded = 0, // Root product does not need a quantity
                Children = new List<Product>()
            };

            var childProduct1 = new Product
            {
                Name = "Child Product 1",
                InventoryCount = 200,
                QuantityNeeded = 20, // 20 Child Product 1s needed for Root Product
                Children = new List<Product>()
            };

            var childProduct2 = new Product
            {
                Name = "Child Product 2",
                InventoryCount = 300,
                QuantityNeeded = 15, // 15 Child Product 2s needed for Root Product
                Children = new List<Product>()
            };

            // Add child products to the root product
            rootProduct.Children.Add(childProduct1);
            rootProduct.Children.Add(childProduct2);

            // Act
            int result = productService.TotalProducts(rootProduct);

            // Assert
            Assert.AreEqual(10, result); // 10 is the minimum of 200/20 and 300/15
        }

        [Test]
        public void TotalProducts_RootProductWithNoChildren_ReturnsZero()
        {
            // Arrange
            var rootProduct = new Product
            {
                Name = "Root Product",
                Children = new List<Product>()
            };

            // Act
            int result = productService.TotalProducts(rootProduct);

            // Assert
            Assert.AreEqual(0, result);
        }

        [Test]
        public void TotalProducts_RootProductIsNull_ReturnsZero()
        {
            // Act
            int result = productService.TotalProducts(null);

            // Assert
            Assert.AreEqual(0, result);
        }

        [Test]
        public void TotalProducts_NonLeafProductWithNoInventory_ReturnsZero()
        {
            // Arrange
            var rootProduct = new Product
            {
                Name = "Root Product",
                Children = new List<Product>
            {
                new Product { Name = "Child 1", InventoryCount = 0, QuantityNeeded = 5 },
                new Product { Name = "Child 2", InventoryCount = 0, QuantityNeeded = 6 }
            }
            };

            // Act
            int result = productService.TotalProducts(rootProduct);

            // Assert
            Assert.AreEqual(0, result); // Both children have zero inventory
        }

        [Test]
        public void TotalProducts_TwentyLevels_ReturnsExpectedResult()
        {
            // Arrange
            var rootProduct = testHelper.CreateProductHierarchy(20);

            // Act
            int result = productService.TotalProducts(rootProduct);

            // Assert
            Assert.AreEqual(100, result); // Assuming all leaf products have a quantity needed of 1
        }

        [Test]
        public void TotalProducts_CarExample_ReturnsExpectedResult()
        {
            // Arrange
            var car = testHelper.CreateCar();

            // Act
            int result = productService.TotalProducts(car);

            // Assert
            Assert.AreEqual(1, result); // Assuming one car can be assembled
        }

        public void TotalProducts_LaptopExample_ReturnsExpectedResult()
        {
            // Arrange
            var laptop = testHelper.CreateLaptop();

            // Act
            int result = productService.TotalProducts(laptop);

            // Assert
            Assert.AreEqual(1, result); // Assuming one laptop can be assembled
        }


    }
}
