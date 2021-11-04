using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CrudApp1.Data.Models;

namespace CrudApp1.Data
{
    public class AutoContext : DbContext
    {
        public AutoContext()
        {
            Database.EnsureCreated();
        }
        public DbSet<Model> Models { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.; Integrated Security=true; Database=Auto");
            }
        }

    }
}
