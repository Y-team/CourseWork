using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models.DB;
using Models.Enams;

namespace DAL
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {
			Database.EnsureCreated();
		}
        public DbSet<Commodity> Commodities { get; set; }
        public DbSet<CommodityImage> CommodityImages { get; set; }
        public DbSet<BasketCommodity> BasketCommodities { get; set; }
        public DbSet<Basket> Baskets { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            builder.Entity<Commodity>().HasKey(i => i.Id);
            builder.Entity<CommodityImage>().HasKey(i => i.Id);

            #region FK

            builder.Entity<Commodity>()
                .HasMany(ag => ag.BasketCommodities)
                .WithOne(au => au.Commodity)
                .HasForeignKey(au => au.CommodityId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Basket>()
                .HasMany(ag => ag.BasketCommodities)
                .WithOne(au => au.Basket)
                .HasForeignKey(au => au.BasketId)
                .OnDelete(DeleteBehavior.Cascade);

            #endregion

            builder.Entity<BasketCommodity>()
                .HasKey(bc => new { bc.BasketId, bc.CommodityId });


            #region Required fields

            builder.Entity<Commodity>()
                .Property(c => c.Number)
                .IsRequired();
            builder.Entity<Commodity>()
                .Property(c => c.Price)
                .IsRequired();
            builder.Entity<Commodity>()
                .Property(c => c.Name)
                .IsRequired();

            #endregion

            #region  Unique indexes

            builder.Entity<Commodity>()
                .HasIndex(w => w.Name)
                .IsUnique();

            #endregion

            
            // Default values
            builder.Entity<Commodity>()
                .Property(r => r.TypeCommodity)
                .HasDefaultValue(TypeCommodity.NotSelected);
        }
    }
}
