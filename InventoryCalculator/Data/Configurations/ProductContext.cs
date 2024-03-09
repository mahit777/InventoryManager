using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
namespace InventoryCalculator.Data
{


    public class ProductContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Server=localhost;Database=InventoryDb2;Port=5432;User Id=postgres;Password=postgres;");
        }
    }

}

