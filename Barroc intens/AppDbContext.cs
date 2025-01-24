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
            var appointments = new List<Appointment>();
            var customers = new List<Customer>();
            var contactPerson = new List<CustomerContactPerson>();

            categories.AddRange([
            new Category { Id = 1, Name = "Automaten" },
            new Category { Id = 2, Name = "Koffiebonen" }
            ]);

            products.AddRange([
                // Automaten
                new Product
                {
                    Id = 1,
                    Name = "Barroc Intens Italian Light",
                    ProductNumber = 23456701,
                    LeaseCost = 499,
                    InstallCost = 289,
                    PricePerKilo = 0, // Niet van toepassing
                    CategoryId = 1
                },
                new Product
                {
                    Id = 2,
                    Name = "Barroc Intens Italian",
                    ProductNumber = 23456702,
                    UnitsInStock = 0,
                    LeaseCost = 599,
                    InstallCost = 289,
                    PricePerKilo = 0, // Niet van toepassing
                    CategoryId = 1
                },
                new Product
                {
                    Id = 3,
                    Name = "Barroc Intens Italian Deluxe",
                    ProductNumber = 23456703,
                    UnitsInStock = 2,
                    LeaseCost = 799,
                    InstallCost = 375,
                    PricePerKilo = 0, // Niet van toepassing
                    CategoryId = 1
                },
                new Product
                {
                    Id = 4,
                    Name = "Barroc Intens Italian Deluxe Special",
                    ProductNumber = 23456704,
                    UnitsInStock = 7,
                    LeaseCost = 999,
                    InstallCost = 375,
                    PricePerKilo = 0, // Niet van toepassing
                    CategoryId = 1
                },
                new Product
                {
                    Id = 13,
                    Name = "Barroc Intens Brew Pro",
                    ProductNumber = 23456701,
                    LeaseCost = 499,
                    InstallCost = 289,
                    PricePerKilo = 0, // Niet van toepassing
                    CategoryId = 1
                },
                new Product
                {
                    Id = 14,
                    Name = "Barroc Intens Brew Pro max plus",
                    ProductNumber = 23456701,
                    UnitsInStock = 1,
                    LeaseCost = 499,
                    InstallCost = 289,
                    PricePerKilo = 0, // Niet van toepassing
                    CategoryId = 1
                },
                new Product
                {
                    Id = 15,
                    Name = "Barroc Intens Brew Pro max plus ultra",
                    ProductNumber = 23456701,
                    UnitsInStock = 2,
                    LeaseCost = 499,
                    InstallCost = 289,
                    PricePerKilo = 0, // Niet van toepassing
                    CategoryId = 1
                },
                new Product
                {
                    Id = 16,
                    Name = "Barroc Intens Brew",
                    ProductNumber = 23456701,
                    UnitsInStock = 3,
                    LeaseCost = 499,
                    InstallCost = 289,
                    PricePerKilo = 0, // Niet van toepassing
                    CategoryId = 1
                },
                new Product
                {
                    Id = 17,
                    Name = "Barroc Intens Brew ii",
                    ProductNumber = 23456701,
                    UnitsInStock = 4,
                    LeaseCost = 499,
                    InstallCost = 289,
                    PricePerKilo = 0, // Niet van toepassing
                    CategoryId = 1
                },
                new Product
                {
                    Id = 18,
                    Name = "Barroc Intens Brew Pro iii",
                    ProductNumber = 23456701,
                    UnitsInStock = 2,
                    LeaseCost = 499,
                    InstallCost = 289,
                    PricePerKilo = 0, // Niet van toepassing
                    CategoryId = 1
                },
                new Product
                {
                    Id = 19,
                    Name = "Barroc Intens Brew iv",
                    ProductNumber = 23456701,
                    UnitsInStock = 1,
                    LeaseCost = 499,
                    InstallCost = 289,
                    PricePerKilo = 0, // Niet van toepassing
                    CategoryId = 1
                },
                // Koffiebonen
                new Product
                {
                    Id = 5,
                    Name = "Espresso Beneficio",
                    ProductNumber = 23912345,
                    UnitsInStock = 273,
                    LeaseCost = 0, // Niet van toepassing
                    InstallCost = 0, // Niet van toepassing
                    PricePerKilo = 21.60,
                    CategoryId = 2
                },
                new Product
                {
                    Id = 6,
                    Name = "Yellow Bourbon Brasil",
                    ProductNumber = 23912346,
                    UnitsInStock = 320,
                    LeaseCost = 0, // Niet van toepassing
                    InstallCost = 0, // Niet van toepassing
                    PricePerKilo = 23.20,
                    CategoryId = 2
                },
                new Product
                {
                    Id = 7,
                    Name = "Espresso Roma",
                    ProductNumber = 23912347,
                    UnitsInStock = 74,
                    LeaseCost = 0, // Niet van toepassing
                    InstallCost = 0, // Niet van toepassing
                    PricePerKilo = 20.80,
                    CategoryId = 2
                },
                new Product
                {
                    Id = 8,
                    Name = "Red Honey Honduras",
                    ProductNumber = 23912348,
                    UnitsInStock = 37,
                    LeaseCost = 0, // Niet van toepassing
                    InstallCost = 0, // Niet van toepassing
                    PricePerKilo = 27.80,
                    CategoryId = 2
                },
                new Product
                {
                    Id = 9,
                    Name = "Panama Joe's Blend",
                    ProductNumber = 23912348,
                    UnitsInStock = 32,
                    LeaseCost = 0, // Niet van toepassing
                    InstallCost = 0, // Niet van toepassing
                    PricePerKilo = 27.80,
                    CategoryId = 2
                },
                new Product
                {
                    Id = 10,
                    Name = "Kentucky Straight Single Origin",
                    ProductNumber = 23912348,
                    UnitsInStock = 46,
                    LeaseCost = 0, // Niet van toepassing
                    InstallCost = 0, // Niet van toepassing
                    PricePerKilo = 27.80,
                    CategoryId = 2
                },
                new Product
                {
                    Id = 11,
                    Name = "Ipanema Beach Blonde Roast",
                    ProductNumber = 23912348,
                    UnitsInStock = 42,
                    LeaseCost = 0, // Niet van toepassing
                    InstallCost = 0, // Niet van toepassing
                    PricePerKilo = 27.80,
                    CategoryId = 2
                },
                new Product
                {
                    Id = 12,
                    Name = "Cup'a'Joe Special",
                    ProductNumber = 23912348,
                    UnitsInStock = 13,
                    LeaseCost = 0, // Niet van toepassing
                    InstallCost = 0, // Niet van toepassing
                    PricePerKilo = 27.80,
                    CategoryId = 2
                },
                new Product
                {
                    Id = 20,
                    Name = "Hot Beans! blend",
                    ProductNumber = 23912348,
                    UnitsInStock = 46,
                    LeaseCost = 0, // Niet van toepassing
                    InstallCost = 0, // Niet van toepassing
                    PricePerKilo = 27.80,
                    CategoryId = 2
                },
                new Product
                {
                    Id = 21,
                    Name = "Black Cow blend",
                    ProductNumber = 23912348,
                    UnitsInStock = 42,
                    LeaseCost = 0, // Niet van toepassing
                    InstallCost = 0, // Niet van toepassing
                    PricePerKilo = 27.80,
                    CategoryId = 2
                },
                new Product
                {
                    Id = 22,
                    Name = "Stawberry Fields Blend",
                    ProductNumber = 23912348,
                    UnitsInStock = 46,
                    LeaseCost = 0, // Niet van toepassing
                    InstallCost = 0, // Niet van toepassing
                    PricePerKilo = 27.80,
                    CategoryId = 2
                },
                new Product
                {
                    Id = 23,
                    Name = "Samba Triste Blend",
                    ProductNumber = 23912348,
                    UnitsInStock = 42,
                    LeaseCost = 0, // Niet van toepassing
                    InstallCost = 0, // Niet van toepassing
                    PricePerKilo = 27.80,
                    CategoryId = 2
                },
                new Product
                {
                    Id = 24,
                    Name = "Katy Lied Blend",
                    ProductNumber = 23912348,
                    UnitsInStock = 46,
                    LeaseCost = 0, // Niet van toepassing
                    InstallCost = 0, // Niet van toepassing
                    PricePerKilo = 27.80,
                    CategoryId = 2
                },
                new Product
                {
                    Id = 25,
                    Name = "Aja Blend",
                    ProductNumber = 23912348,
                    UnitsInStock = 42,
                    LeaseCost = 0, // Niet van toepassing
                    InstallCost = 0, // Niet van toepassing
                    PricePerKilo = 27.80,
                    CategoryId = 2
                },
            ]);

            appointments.AddRange([
                 new Appointment
                    {
                        Id = 1,
                        Date = DateTime.Now.AddDays(1),
                        Duration = 60,
                        Description = "Initial Consultation",
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now,
                        UserId = 1,
                        Location = "Office 1"
                    },
                    new Appointment
                    {
                        Id = 2,
                        Date = DateTime.Now.AddDays(2),
                        Duration = 90,
                        Description = "Follow-up",
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now,
                        UserId = 2,
                        Location = "Office 2"
                    },
                     new Appointment
                    {
                        Id = 3,
                        Date = DateTime.Now.AddDays(2),
                        Duration = 100,
                        Description = "Follow-up",
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now,
                        UserId = 3,
                        Location = "Office 3"
                    }
                ]);

            customers.AddRange([
                new Customer
                {
                    Id = 1,
                    CompanyName = "Tech Solutions Inc.",
                    Address = "123 Tech Street, Innovation City",
                    Description = "Leading provider of tech solutions."
                },
                new Customer
                {
                    Id = 2,
                    CompanyName = "Green Energy Co.",
                    Address = "456 Renewable Lane, EcoTown",
                    Description = "Specializes in renewable energy projects."
                }
            ]);

            contactPerson.AddRange([
                new CustomerContactPerson
                {
                    Id = 1,
                    CustomerId = 1
                },
                new CustomerContactPerson
                {
                    Id = 2,
                    CustomerId = 1
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
            modelBuilder.Entity<Category>().HasData(categories);
            modelBuilder.Entity<Product>().HasData(products);
            modelBuilder.Entity<Appointment>().HasData(appointments);
            modelBuilder.Entity<Customer>().HasData(customers);
            modelBuilder.Entity<CustomerContactPerson>().HasData(contactPerson);
        }
    }
}
