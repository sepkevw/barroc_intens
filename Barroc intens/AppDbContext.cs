using Barroc_intens.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barroc_intens
{
    internal class AppDbContext : DbContext
    {

        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerContactPerson> CustomerContactPersons { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<PackageProduct> PackageProducts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<CustomerAppointment> CustomerAppointments { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(
                ConfigurationManager.ConnectionStrings["Database"].ConnectionString, ServerVersion.Parse("8.0.13"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .HasOne(c => c.ContactPerson)
                .WithOne(cp => cp.Customer)
                .HasForeignKey<CustomerContactPerson>(cp => cp.CustomerId); // Specify the foreign key here

            modelBuilder.Entity<Role>().HasData(
               new Role { Id = 1, RoleName = "Admin" },
               new Role { Id = 2, RoleName= "User" },
               new Role { Id = 3, RoleName = "Manager" }
            );

            //products en category seeder
            var products = new List<Product>();
            var categories = new List<Category>();

            categories.AddRange([
            new Category { Id = 1, Name = "Automaten" },
            new Category { Id = 2, Name = "Koffiebonen" }
            ]);

            products.AddRange([
                // Automaten
                new Product
                {
                    Id = 1,
                    name = "Barroc Intens Italian Light",
                    ProductNumber = 23456701,
                    LeaseCost = 499,
                    InstallCost = 289,
                    PricePerKilo = 0, // Niet van toepassing
                    CategoryId = 1
                },
                new Product
                {
                    Id = 2,
                    name = "Barroc Intens Italian",
                    ProductNumber = 23456702,
                    LeaseCost = 599,
                    InstallCost = 289,
                    PricePerKilo = 0, // Niet van toepassing
                    CategoryId = 1
                },
                new Product
                {
                    Id = 3,
                    name = "Barroc Intens Italian Deluxe",
                    ProductNumber = 23456703,
                    LeaseCost = 799,
                    InstallCost = 375,
                    PricePerKilo = 0, // Niet van toepassing
                    CategoryId = 1
                },
                new Product
                {
                    Id = 4,
                    name = "Barroc Intens Italian Deluxe Special",
                    ProductNumber = 23456704,
                    LeaseCost = 999,
                    InstallCost = 375,
                    PricePerKilo = 0, // Niet van toepassing
                    CategoryId = 1
                },

                // Koffiebonen
                new Product
                {
                    Id = 5,
                    name = "Espresso Beneficio",
                    ProductNumber = 23912345,
                    LeaseCost = 0, // Niet van toepassing
                    InstallCost = 0, // Niet van toepassing
                    PricePerKilo = 21.60,
                    CategoryId = 2
                },
                new Product
                {
                    Id = 6,
                    name = "Yellow Bourbon Brasil",
                    ProductNumber = 23912346,
                    LeaseCost = 0, // Niet van toepassing
                    InstallCost = 0, // Niet van toepassing
                    PricePerKilo = 23.20,
                    CategoryId = 2
                },
                new Product
                {
                    Id = 7,
                    name = "Espresso Roma",
                    ProductNumber = 23912347,
                    LeaseCost = 0, // Niet van toepassing
                    InstallCost = 0, // Niet van toepassing
                    PricePerKilo = 20.80,
                    CategoryId = 2
                },
                new Product
                {
                    Id = 8,
                    name = "Red Honey Honduras",
                    ProductNumber = 23912348,
                    LeaseCost = 0, // Niet van toepassing
                    InstallCost = 0, // Niet van toepassing
                    PricePerKilo = 27.80,
                    CategoryId = 2
                }
            ]);

            modelBuilder.Entity<Category>().HasData(categories);
            modelBuilder.Entity<Product>().HasData(products);

            // Seed data for users
            var users = new List<User>();
            var random = new Random();

            for (int i = 1; i <= 30; i++)
            {
                users.Add(new User
                {
                    Id = i,
                    Username = $"User{i}",
                    RoleId = random.Next(1, 4), // Assuming there are 3 roles
                    Created_at = DateTime.Now.AddDays(-random.Next(1, 1000)),
                    Role = null // This will be set automatically by EF Core based on RoleId
                });
            }

            modelBuilder.Entity<User>().HasData(users);
        }
    }
}
