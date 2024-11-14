using Barroc_intens.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;

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
           new Role { Id = 3, RoleName = "Manager" });

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
            }   modelBuilder.Entity<User>().HasData(users);

            // Seed data for Categories
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Category1" },
                new Category { Id = 2, Name = "Category2" },
                new Category { Id = 3, Name = "Category3" },
                new Category { Id = 4, Name = "Category4" },
                new Category { Id = 5, Name = "Category5" }
            );

            // Seed data for Customers
            modelBuilder.Entity<Customer>().HasData(
                new Customer { Id = 1, CompanyName = "Customer A", Address = "123 Elm St", ContactPersonId = 1, Description = "Primary Customer" },
                new Customer { Id = 2, CompanyName = "Customer B", Address = "456 Oak St", ContactPersonId = 2, Description = "Secondary Customer" },
                new Customer { Id = 3, CompanyName = "Customer C", Address = "789 Pine St", ContactPersonId = 3, Description = "Tertiary Customer" },
                new Customer { Id = 4, CompanyName = "Customer D", Address = "101 Maple St", ContactPersonId = 4, Description = "Quarterly Customer" },
                new Customer { Id = 5, CompanyName = "Customer E", Address = "102 Maple St", ContactPersonId = 5, Description = "Quinary Customer" },
                new Customer { Id = 6, CompanyName = "Customer F", Address = "103 Maple St", ContactPersonId = 6, Description = "Senary Customer" },
                new Customer { Id = 7, CompanyName = "Customer G", Address = "104 Maple St", ContactPersonId = 7, Description = "Septenary Customer" },
                new Customer { Id = 8, CompanyName = "Customer H", Address = "105 Maple St", ContactPersonId = 8, Description = "Octonary Customer" },
                new Customer { Id = 9, CompanyName = "Customer I", Address = "106 Maple St", ContactPersonId = 9, Description = "Nonary Customer" },
                new Customer { Id = 10, CompanyName = "Customer J", Address = "107 Maple St", ContactPersonId = 10, Description = "Denary Customer" }
            );

            // Seed data for CustomerContactPersons
            modelBuilder.Entity<CustomerContactPerson>().HasData(
                new CustomerContactPerson { Id = 1, Name = "Contact1", Email = "contact1@example.com", CustomerId = 1 },
                new CustomerContactPerson { Id = 2, Name = "Contact2", Email = "contact2@example.com", CustomerId = 2 },
                new CustomerContactPerson { Id = 3, Name = "Contact3", Email = "contact3@example.com", CustomerId = 3 },
                new CustomerContactPerson { Id = 4, Name = "Contact4", Email = "contact4@example.com", CustomerId = 4 },
                new CustomerContactPerson { Id = 5, Name = "Contact5", Email = "contact5@example.com", CustomerId = 5 },
                new CustomerContactPerson { Id = 6, Name = "Contact6", Email = "contact6@example.com", CustomerId = 6 },
                new CustomerContactPerson { Id = 7, Name = "Contact7", Email = "contact7@example.com", CustomerId = 7 },
                new CustomerContactPerson { Id = 8, Name = "Contact8", Email = "contact8@example.com", CustomerId = 8 },
                new CustomerContactPerson { Id = 9, Name = "Contact9", Email = "contact9@example.com", CustomerId = 9 },
                new CustomerContactPerson { Id = 10, Name = "Contact10", Email = "contact10@example.com", CustomerId = 10 }
            );

            // Seed data for Appointments
            modelBuilder.Entity<Appointment>().HasData(
                new Appointment { Id = 1, CustomerId = 1, UserId = 1, Date = DateTime.Now.AddDays(-10), Duration = 60, Description = "Initial Meeting", CreatedAt = DateTime.Now.AddDays(-10), UpdatedAt = DateTime.Now, Location = "Office A" },
                new Appointment { Id = 2, CustomerId = 2, UserId = 2, Date = DateTime.Now.AddDays(-9), Duration = 45, Description = "Follow-up Meeting", CreatedAt = DateTime.Now.AddDays(-9), UpdatedAt = DateTime.Now, Location = "Office B" },
                new Appointment { Id = 3, CustomerId = 3, UserId = 3, Date = DateTime.Now.AddDays(-8), Duration = 30, Description = "Consultation", CreatedAt = DateTime.Now.AddDays(-8), UpdatedAt = DateTime.Now, Location = "Office C" },
                new Appointment { Id = 4, CustomerId = 4, UserId = 1, Date = DateTime.Now.AddDays(-7), Duration = 90, Description = "Project Review", CreatedAt = DateTime.Now.AddDays(-7), UpdatedAt = DateTime.Now, Location = "Office D" },
                new Appointment { Id = 5, CustomerId = 5, UserId = 2, Date = DateTime.Now.AddDays(-6), Duration = 120, Description = "Annual Meeting", CreatedAt = DateTime.Now.AddDays(-6), UpdatedAt = DateTime.Now, Location = "Office E" },
                new Appointment { Id = 6, CustomerId = 6, UserId = 3, Date = DateTime.Now.AddDays(-5), Duration = 60, Description = "Strategy Discussion", CreatedAt = DateTime.Now.AddDays(-5), UpdatedAt = DateTime.Now, Location = "Office F" },
                new Appointment { Id = 7, CustomerId = 7, UserId = 1, Date = DateTime.Now.AddDays(-4), Duration = 45, Description = "Budget Review", CreatedAt = DateTime.Now.AddDays(-4), UpdatedAt = DateTime.Now, Location = "Office G" },
                new Appointment { Id = 8, CustomerId = 8, UserId = 2, Date = DateTime.Now.AddDays(-3), Duration = 60, Description = "New Project", CreatedAt = DateTime.Now.AddDays(-3), UpdatedAt = DateTime.Now, Location = "Office H" },
                new Appointment { Id = 9, CustomerId = 9, UserId = 3, Date = DateTime.Now.AddDays(-2), Duration = 75, Description = "Closing Meeting", CreatedAt = DateTime.Now.AddDays(-2), UpdatedAt = DateTime.Now, Location = "Office I" },
                new Appointment { Id = 10, CustomerId = 10, UserId = 1, Date = DateTime.Now.AddDays(-1), Duration = 30, Description = "Catch-up", CreatedAt = DateTime.Now.AddDays(-1), UpdatedAt = DateTime.Now, Location = "Office J" }
            );

            // Seed data for CustomerAppointments (Associative Table)
            modelBuilder.Entity<CustomerAppointment>().HasData(
                new CustomerAppointment { Id = 1, CustomerId = 1, AppointmentId = 1, Notes = "Follow-up needed", CreatedAt = DateTime.Now.AddDays(-10) },
                new CustomerAppointment { Id = 2, CustomerId = 2, AppointmentId = 2, Notes = "Scheduled follow-up", CreatedAt = DateTime.Now.AddDays(-9) },
                new CustomerAppointment { Id = 3, CustomerId = 3, AppointmentId = 3, Notes = "Discuss further", CreatedAt = DateTime.Now.AddDays(-8) },
                new CustomerAppointment { Id = 4, CustomerId = 4, AppointmentId = 4, Notes = "Requires review", CreatedAt = DateTime.Now.AddDays(-7) },
                new CustomerAppointment { Id = 5, CustomerId = 5, AppointmentId = 5, Notes = "Annual check", CreatedAt = DateTime.Now.AddDays(-6) },
                new CustomerAppointment { Id = 6, CustomerId = 6, AppointmentId = 6, Notes = "Strategy planning", CreatedAt = DateTime.Now.AddDays(-5) },
                new CustomerAppointment { Id = 7, CustomerId = 7, AppointmentId = 7, Notes = "Budget discussion", CreatedAt = DateTime.Now.AddDays(-4) },
                new CustomerAppointment { Id = 8, CustomerId = 8, AppointmentId = 8, Notes = "New project outline", CreatedAt = DateTime.Now.AddDays(-3) },
                new CustomerAppointment { Id = 9, CustomerId = 9, AppointmentId = 9, Notes = "Finalize terms", CreatedAt = DateTime.Now.AddDays(-2) },
                new CustomerAppointment { Id = 10, CustomerId = 10, AppointmentId = 10, Notes = "General update", CreatedAt = DateTime.Now.AddDays(-1) }
            );
        }
    }
}
