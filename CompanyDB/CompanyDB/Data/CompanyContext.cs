using CompanyDB.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyDB.Data
{
    public class CompanyContext : DbContext
    {
        public CompanyContext()
        {
            Database.EnsureCreated();
        }
        public DbSet<Employee> Employees { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Integrated Security=true;Database=EmployeeDB");
            }
        }
    }
}
