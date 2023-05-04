using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Apahida.Models;

namespace Apahida.Data
{
    public class ApahidaContext : DbContext
    {
        public DbSet<Users> Users { get; set; } = default!;
        public DbSet<Order> Order { get; set; } = default!;
        public DbSet<Food> Food { get; set; } = default!;
        public DbSet<OrderFood> OrderFood { get; set; } = default!;
        public ApahidaContext (DbContextOptions<ApahidaContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderFood>()
                .HasKey(obj => new {obj.Id});
            modelBuilder.Entity<OrderFood>()
                .Property(of => of.Id)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<OrderFood>()
                .HasOne(obj => obj.Food)
                .WithMany(b => b.Orders)
                .HasForeignKey(obj => obj.FoodId);
            modelBuilder.Entity<OrderFood>()
                .HasOne(obj => obj.Order)
                .WithMany(c => c.Foods)
                .HasForeignKey(obj => obj.OrderId);
        }

    }
}
