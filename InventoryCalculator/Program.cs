using System;
using InventoryCalculator.Data;
using InventoryCalculator.Services;
using Microsoft.EntityFrameworkCore;


using (var context = new ProductContext())
        {

    // traditionally we would inject it for now we are just creating an object
    IProductService productService = new ProductService();
        // Ensure the database is created
        context.Database.EnsureCreated();
    ClearDatabase(context);
    // Initialize the database with test values
    InitializeBikeDatabase(context);

    // Load the root product  from the database
    var bike = context.Products        
        .FirstOrDefault(p => p.ParentId == null);
    LoadAllLevels(bike, context);

    if (bike != null)
            {
                // Compute the maximum number of root products that can be built using spare parts
                int maxRoots = productService.TotalProducts(bike);
                Console.WriteLine("Maximum number of root products: " + maxRoots);
            }
            else
            {
                Console.WriteLine("Bike not found in the database.");
            }
        }


 static void LoadAllLevels(Product product, ProductContext context)
{
    if (product == null)
        return;

    context.Entry(product)
        .Collection(p => p.Children)
        .Load();

    if (product.Children != null)
    {
        foreach (var child in product.Children)
        {
            LoadAllLevels(child,context);
        }
    }
}

// Method to clear the database
 void ClearDatabase(ProductContext context)
{
    // Get all products from the database
    var allProducts = context.Products.ToList();

    // Remove each product from the database
    foreach (var product in allProducts)
    {
        context.Products.Remove(product);
    }

    // Save changes to the database
    context.SaveChanges();
}

static void InitializeBikeDatabase(ProductContext context)
{
    // Check if products already exist in the database
    if (context.Products.Any())
    {
        Console.WriteLine("Database already initialized.");
        return;
    }

    // Create the leaf products
    var seat = new Product
    {
        Id = Guid.NewGuid(),
        Name = "Seat",
        InventoryCount = 100,
        QuantityNeeded = 1, // One seat needed per parent product
        Children = new List<Product>()
    };

    var tube = new Product
    {
        Id = Guid.NewGuid(),
        Name = "Tube",
        InventoryCount = 100,
        QuantityNeeded = 1, // One tube needed per parent product
        Children = new List<Product>()
    };

    var frame = new Product
    {
        Id = Guid.NewGuid(),
        Name = "Frame",
        InventoryCount = 60,
        QuantityNeeded = 1, // One frame needed per parent product
        Children = new List<Product>()
    };

    var wheel = new Product
    {
        Id = Guid.NewGuid(),
        Name = "Wheel",
        InventoryCount = 60,
        QuantityNeeded = 2, // Two wheels needed per parent product
        Children = new List<Product> { tube, frame }
    };

    var pedal = new Product
    {
        Id = Guid.NewGuid(),
        Name = "Pedal",
        InventoryCount = 1000,
        QuantityNeeded = 2, // Two pedals needed per parent product
        Children = new List<Product>()
    };

    // Create the bundle (bike) and assemble the required parts
    var bike = new Product
    {
        Id = Guid.NewGuid(),
        Name = "Bike",
        InventoryCount = 0,
        QuantityNeeded = 0, // One bike
        Children = new List<Product> { wheel, pedal, seat }
    };

 

    // Add all products to the context
    context.Products.AddRange(bike, wheel, frame, tube, pedal, seat);

    // Save changes to the database
    context.SaveChanges();
    
    Console.WriteLine("Database initialized with dummy values.for bikes");

}


