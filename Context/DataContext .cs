using Microsoft.EntityFrameworkCore;
using Human_Resource_Management_App.Entities;
using System.Diagnostics;

namespace Human_Resource_Management_App.Context
{
    public class DataContext : DbContext
    {
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Employee> Employees { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}

