
using EntityLayer.Concretes;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Context
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Employee - AppUser ilişkisi
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.AppUser)
                .WithMany() 
                .HasForeignKey(e => e.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);

            // Employee - Hairdresser ilişkisi
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Hairdresser)
                .WithMany(h => h.Employees) 
                .HasForeignKey(e => e.HairdresserId)
                .OnDelete(DeleteBehavior.NoAction);

            // EmployeeService - Employee ilişki konfigürasyonu
            modelBuilder.Entity<EmployeeService>()
                .HasOne(es => es.Employee)
                .WithMany(e => e.EmployeeServices)
                .HasForeignKey(es => es.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);

            // EmployeeService - Service ilişki konfigürasyonu
            modelBuilder.Entity<EmployeeService>()
                .HasOne(es => es.Service)
                .WithMany(s => s.EmployeeServices)
                .HasForeignKey(es => es.ServiceId)
                .OnDelete(DeleteBehavior.NoAction); 

        }
        public DbSet<Hairdresser> Hairdressers { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeService> EmployeeServices { get; set; }
    }
}
