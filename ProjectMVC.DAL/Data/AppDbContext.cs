using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjectMVC.DAL.Data.Migrations;
using ProjectMVC.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMVC.DAL.Data
{
    public class AppDbContext : IdentityDbContext
    {
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }


        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server = .; Database = ProjectMVC; Trusted_Connection = True");
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(System.Reflection.Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<IdentityRole>().ToTable("Roles");
            modelBuilder.Entity<IdentityUser>().ToTable("Users");

        }
    }
}
