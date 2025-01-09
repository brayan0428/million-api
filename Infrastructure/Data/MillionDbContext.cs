using Core.Entities;
using Infrastructure.Data.Configurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class MillionDbContext : DbContext
    {
        public MillionDbContext(DbContextOptions<MillionDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new OwnerConfiguration());
            modelBuilder.ApplyConfiguration(new PropertyConfiguration());
            modelBuilder.ApplyConfiguration(new PropertyImageConfiguration());
            modelBuilder.ApplyConfiguration(new PropertyTraceConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());

            // Seed para Owners
            modelBuilder.Entity<Owner>().HasData(
                new Owner { IdOwner = 1, Name = "Juan Pérez", Address = "Calle 123, Bogotá", Photo = null, Birthday = new DateTime(1980, 5, 14) },
                new Owner { IdOwner = 2, Name = "María Rodríguez", Address = "Avenida Central 45, Medellín", Photo = null, Birthday = new DateTime(1992, 9, 23) },
                new Owner { IdOwner = 3, Name = "Carlos Gómez", Address = "Carrera 10 #15-20, Cali", Photo = null, Birthday = new DateTime(1975, 11, 30) },
                new Owner { IdOwner = 4, Name = "Ana Martínez", Address = "Diagonal 25 #33-45, Barranquilla", Photo = null, Birthday = new DateTime(1988, 7, 12) }
            );

            // Seed para Properties
            modelBuilder.Entity<Property>().HasData(
                new Property { IdProperty = 1, Name = "Casa en la Playa", Address = "Sector Punta Arena, Cartagena", Price = 350000000, CodeInternal = "PROP001", Year = 2015, IdOwner = 1 },
                new Property { IdProperty = 2, Name = "Apartamento en el Centro", Address = "Carrera 7 #12-34, Bogotá", Price = 500000000, CodeInternal = "PROP002", Year = 2020, IdOwner = 2 },
                new Property { IdProperty = 3, Name = "Finca Campestre", Address = "Kilómetro 15 vía al Llano, Villavicencio", Price = 800000000, CodeInternal = "PROP003", Year = 2010, IdOwner = 3 },
                new Property { IdProperty = 4, Name = "Local Comercial", Address = "Zona Rosa, Medellín", Price = 950000000, CodeInternal = "PROP004", Year = 2018, IdOwner = 4 },
                new Property { IdProperty = 5, Name = "Penthouse de Lujo", Address = "Edificio Ocean View, Santa Marta", Price = 1200000000, CodeInternal = "PROP005", Year = 2019, IdOwner = 1 }
            );

            //Seed para Users
            var adminPassword = BCrypt.Net.BCrypt.HashPassword("Admin123");
            var userPassword = BCrypt.Net.BCrypt.HashPassword("User123");

            // Insertar datos iniciales
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Nombre = "Admin",
                    Email = "admin@example.com",
                    PasswordHash = adminPassword
                },
                new User
                {
                    Id = 2,
                    Nombre = "User",
                    Email = "user@example.com",
                    PasswordHash = userPassword
                }
            );
        }
    }
}
