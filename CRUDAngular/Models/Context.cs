using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace CRUDAngular.Models
{
    public class Context : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=localhost\SQLEXPRESS;Database=CRUDAngular;Integrated Security=True");
        }
        public DbSet<Product> Products { get; set; }
    }
}
