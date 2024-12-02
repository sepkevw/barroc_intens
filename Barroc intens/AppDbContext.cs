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

        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .HasOne(c => c.ContactPerson)
                .WithOne(cp => cp.Customer)
                .HasForeignKey<CustomerContactPerson>(cp => cp.CustomerId); // Specify the foreign key here

            modelBuilder.Entity<Role>().HasData(
           new Role { Id = 1, RoleName = "Admin" },
           new Role { Id = 2, RoleName = "CEO"},
           new Role { Id = 2, RoleName= "HeadFinance" },
           new Role { Id = 3, RoleName = "AdminFinance" },
           new Role { Id = 4, RoleName = "HeadSales"},
           new Role { Id = 5, RoleName = "Consultant"},
           new Role { Id = 6, RoleName = "headInkoop"},
           new Role { Id = 7, RoleName = "Inkoper"},
           new Role { Id = 8, RoleName = "MedewerkerMagazijn"},
           new Role { Id = 9, RoleName = "HeadMaintenance"},
           new Role { Id = 11, RoleName = "TechnicalService"},
           new Role { Id = 12, RoleName = "Planner"}
       );

            // Seed data for users
            var users = new List<User>();
            var random = new Random();

            for (int i = 1; i <= 30; i++)
            {
                users.Add(new User
                {
                    Id = i,
                    Username = $"User{i}",
                    Password = SecureHasher.Hash("test"),
                    RoleId = random.Next(1, 4), // Assuming there are 3 roles
                    Created_at = DateTime.Now.AddDays(-random.Next(1, 1000)),
                    Role = null
                });
            }

            modelBuilder.Entity<User>().HasData(users);
        }
    }
}
