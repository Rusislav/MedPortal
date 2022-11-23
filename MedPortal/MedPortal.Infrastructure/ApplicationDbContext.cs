using MedPortal.Infrastructure.Configuration;
using MedPortal.Infrastructure.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Reflection.Emit;

namespace MedPortal.Infrastructure
{
    public class ApplicationDbContext : IdentityDbContext
    {
        

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            
        }
        public DbSet<Product> Products { get; set; } = null!;

        public DbSet<Category> Categories { get; set; } = null!;

        public DbSet<Manufacturer> Manufacturers { get; set; } = null!;

        public DbSet<Pharmacy> Pharmacies { get; set; } = null!;

        public DbSet<Cart> Carts { get; set; } = null!;


       

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<PharamcyProduct>()
         .HasKey(k => new { k.PharmacyId, k.ProductId });

            //seed admin role
            builder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Name = "Admin",
                NormalizedName = "ADMIN",
                Id = "1f091cd0-b589-4b77-b951-480ed62fa46f",
                ConcurrencyStamp = "5fce0a67-70da-4e40-af4e-42ef38d13348"
            });

            //set user role to admin
            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = "1f091cd0-b589-4b77-b951-480ed62fa46f",
                UserId = "dea12856-c198-4129-b3f3-b893d8395082"
            });

            builder.ApplyConfiguration(new ManufacturerConfiguration());

            builder.ApplyConfiguration(new CategoryConfiguration());

            builder.ApplyConfiguration(new ProductConfiguration());

            builder.ApplyConfiguration(new PharmacyConfiguration());

            builder.ApplyConfiguration(new UserConfiguration());

        

            base.OnModelCreating(builder);
        }
    }
}