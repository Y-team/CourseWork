using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Rewrite.Internal.UrlActions;
using Microsoft.EntityFrameworkCore;
using WebCustomerApp.Models;

namespace WebCustomerApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

       
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketCommodities> BasketCommoditieses { get; set; }
        public DbSet<LongDescription> LongDescriptions { get; set; }
        public DbSet<Commodity> Commodities{ get; set; }
        public DbSet<Moderator> Moderators { get; set; }
        public DbSet<Photo> Photoes{ get; set; }
        public DbSet<BlockedUser> BlokedUsers{ get; set; }
        public DbSet<OrderUser> OrderUsers { get; set; }
        public  DbSet<OrderCommodities> OrderCommoditieses { get; set; }
        public DbSet<Receipt> Receipts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            // Explicitly setting PK:

         

            builder.Entity<Basket>().HasKey(i => i.Id);
            builder.Entity<LongDescription>().HasKey(i => i.Id);
            builder.Entity<Commodity>().HasKey(i => i.Id);
            builder.Entity<Moderator>().HasKey(i => i.Id);
            builder.Entity<Photo>().HasKey(i => i.Id);
            builder.Entity<BlockedUser>().HasKey(i => i.Id);
            builder.Entity<OrderUser>().HasKey(i => i.Id);
            builder.Entity<Receipt>().HasKey(i => i.Id);

            // Compound key for Many-To-Many joining table

            // Setting FK
            #region FK
            builder.Entity<ApplicationUser>()
                .HasOne(ag => ag.Basket)
                .WithOne(au => au.ApplicationUser)
                .HasForeignKey<Basket>(au => au.UserId)
                .OnDelete(DeleteBehavior.Cascade);

           // builder.Entity<Commodity>()
              //  .HasOne(s => s.LongDescription)
               // .WithOne(c => c.Commodity)
                //.HasForeignKey<LongDescription>(c => c.CommodityId);

            builder.Entity<Moderator>()
                .HasMany(c => c.Commodities)
                .WithOne(m => m.Moderator)
                .HasForeignKey(oc => oc.ModeratorId);

            builder.Entity<ApplicationUser>()
                .HasOne(m=>m.Moderator)
                .WithOne(m => m.ApplicationUser)
                .HasForeignKey<Moderator>(m => m.UserId);

            builder.Entity<ApplicationUser>()
                .HasMany(o=>o.Orders)
                .WithOne(m => m.User)
                .HasForeignKey(m => m.UserId);


           

            #endregion

            // Optional FK

            // Configuring Many-To-Many relationship  and compound index
            builder.Entity<Commodity>()
                .HasMany(bc => bc.BasketCommoditieses)
                .WithOne(c => c.Commodity)
                .HasForeignKey(bc => bc.CommodityId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Basket>()
                .HasMany(bc => bc.BasketCommoditieses)
                .WithOne(b => b.Basket)
                .HasForeignKey(bc => bc.BasketId)
                .OnDelete(DeleteBehavior.Cascade);


            builder.Entity<Commodity>()
                .HasMany(oc => oc.OrderCommoditieses)
                .WithOne(c => c.Commodity)
                .HasForeignKey(oc => oc.CommodityId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<OrderUser>()
                .HasMany(oc => oc.OrderCommoditieses)
                .WithOne(o => o.OrderUser)
                .HasForeignKey(oc => oc.OrderId)
                .OnDelete(DeleteBehavior.Cascade);


            builder.Entity<BasketCommodities>().HasKey(r => new { r.BasketId, r.CommodityId });
            builder.Entity<OrderCommodities>().HasKey(r => new { r.OrderId, r.CommodityId });
            
        }
    }
}
