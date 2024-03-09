using System;
using System.Collections.Generic;
namespace InventoryCalculator.Data
{


    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int InventoryCount { get; set; }
        public int QuantityNeeded { get; set; }
        // Parent relationship
        public Guid? ParentId { get; set; }
        public Product? Parent { get; set; }

        public List<Product>? Children { get; set; }
    }

}

