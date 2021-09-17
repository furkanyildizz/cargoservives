using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CustomerAddress.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CustomerAddress.DAL.Context
{
    public class CargoDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("Server=localhost;Database=CargoDb;Uid=root;Pwd=19961999a;ConvertZeroDateTime=True;");
            //optionsBuilder.UseSqlServer("Server=DESKTOP-LVM881V\\SQLEXPRESS;Database=CargoDB;Trusted_Connection=True;");

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Town> Towns { get; set; }
        public DbSet<Street> Streets { get; set; }
        public DbSet<Neighborhood> Neighborhoods { get; set; }


        public DbSet<CompanyBranch> CompanyBranches { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Post> Posts { get; set; }

        public DbSet<Status> Statuses { get; set; }
        public DbSet<RouteMap> RouteMaps { get; set; }
        


    }
}
