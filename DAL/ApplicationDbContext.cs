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

        public DbSet<Code> Codes { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Operator> Operators { get; set; }
        public DbSet<Phone> Phones { get; set; }
        public DbSet<Recipient> Recipients { get; set; }
        public DbSet<StopWord> StopWords { get; set; }
        public DbSet<Tariff> Tariffs { get; set; }
        public DbSet<ApplicationGroup> Groups { get; set; }


        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketCommodities> BasketCommoditieses { get; set; }
        public DbSet<LongDescription> LongDescriptions { get; set; }
        public DbSet<Commodity> Commodities{ get; set; }
        public DbSet<Moderator> Moderators { get; set; }
        public DbSet<Photo> Photoes{ get; set; }
        public DbSet<BlokedUser> BlokedUsers{ get; set; }
        public DbSet<Order> Orders { get; set; }
        public  DbSet<OrderCommodities> OrderCommoditieses { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            // Explicitly setting PK:

            builder.Entity<Code>().HasKey(i => i.Id);
            builder.Entity<Company>().HasKey(i => i.Id);
            builder.Entity<Contact>().HasKey(i => i.Id);
            builder.Entity<Operator>().HasKey(i => i.Id);
            builder.Entity<Recipient>().HasKey(i => i.Id);
            builder.Entity<Phone>().HasKey(i => i.Id);
            builder.Entity<Tariff>().HasKey(i => i.Id);
            builder.Entity<StopWord>().HasKey(i => i.Id);
            builder.Entity<ApplicationGroup>().HasKey(i => i.Id);

            builder.Entity<Basket>().HasKey(i => i.Id);
            builder.Entity<LongDescription>().HasKey(i => i.Id);
            builder.Entity<Commodity>().HasKey(i => i.Id);
            builder.Entity<Moderator>().HasKey(i => i.Id);
            builder.Entity<Photo>().HasKey(i => i.Id);
            builder.Entity<BlokedUser>().HasKey(i => i.Id);
            builder.Entity<Order>().HasKey(i => i.Id);

            // Compound key for Many-To-Many joining table

            // Setting FK
            #region FK
            builder.Entity<ApplicationUser>()
                .HasOne(ag => ag.Basket)
                .WithOne(au => au.ApplicationUser)
                .HasForeignKey<Basket>(au => au.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Commodity>()
                .HasMany(p => p.Photo)
                .WithOne(c=> c.Commodity)
                .HasForeignKey(p => p.CommodityId);

          

            builder.Entity<Commodity>()
                .HasOne(s => s.LongDescription)
                .WithOne(c => c.Commodity)
                .HasForeignKey<LongDescription>(c => c.CommodityId);

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


            //======================
            builder.Entity<ApplicationGroup>()
              .HasMany(ag => ag.ApplicationUsers)
              .WithOne(au => au.ApplicationGroup)
              .HasForeignKey(au => au.ApplicationGroupId)
              .OnDelete(DeleteBehavior.Cascade);//Delete

            builder.Entity<ApplicationGroup>()
              .HasMany(ag => ag.Companies)
              .WithOne(c => c.ApplicationGroup)
              .HasForeignKey(c => c.ApplicationGroupId)
              .OnDelete(DeleteBehavior.Cascade);//Delete

            builder.Entity<ApplicationGroup>()
              .HasMany(ag => ag.Contacts)
              .WithOne(c => c.ApplicationGroup)
              .HasForeignKey(c => c.ApplicationGroupId)
              .OnDelete(DeleteBehavior.Cascade);//Delete

            builder.Entity<Operator>()
                .HasMany(o => o.Codes)
                .WithOne(c => c.Operator)
                .HasForeignKey(c => c.OperatorId)
                .OnDelete(DeleteBehavior.Cascade);//Delete

            builder.Entity<Operator>()
                .HasMany(o => o.Tariffs)
                .WithOne(t => t.Operator)
                .HasForeignKey(t => t.OperatorId)
                .OnDelete(DeleteBehavior.Cascade);//Delete

            builder.Entity<Phone>()
                .HasMany(p => p.Contacts)
                .WithOne(c => c.Phone)
                .HasForeignKey(c => c.PhoneId)
                .OnDelete(DeleteBehavior.ClientSetNull);//Delete

            builder.Entity<Tariff>()
                .HasMany(t => t.Companies)
                .WithOne(com => com.Tariff)
                .HasForeignKey(com => com.TariffId)
                .OnDelete(DeleteBehavior.ClientSetNull);//Delete

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

            builder.Entity<Order>()
                .HasMany(oc => oc.OrderCommoditieses)
                .WithOne(o => o.Order)
                .HasForeignKey(oc => oc.OrderId)
                .OnDelete(DeleteBehavior.Cascade);


            builder.Entity<BasketCommodities>().HasKey(r => new { r.BasketId, r.CommodityId });
            builder.Entity<OrderCommodities>().HasKey(r => new { r.OrderId, r.CommodityId });

            builder.Entity<Company>()
                .HasMany(c => c.Recipients)
                .WithOne(r => r.Company)
                .HasForeignKey(r => r.CompanyId)
                .OnDelete(DeleteBehavior.Cascade);//Delete

            builder.Entity<Phone>()
                .HasMany(p => p.Recipients)
                .WithOne(r => r.Phone)
                .HasForeignKey(r => r.PhoneId)
                .OnDelete(DeleteBehavior.Cascade);//Delete

            builder.Entity<Recipient>()
                .HasIndex(r => new { r.PhoneId, r.CompanyId })
                .IsUnique();//Delete

            // Required fields
            #region Required fields
            builder.Entity<Code>()
                .Property(c => c.OperatorCode)
                .IsRequired();//Delete

            builder.Entity<Company>()
                .Property(com => com.Name)
                .IsRequired();//Delete

            builder.Entity<Company>()
                .Property(com => com.Message)
                .IsRequired();//Delete

            builder.Entity<Phone>()
                .Property(p => p.PhoneNumber)
                .IsRequired();//Delete

            builder.Entity<Tariff>()
                .Property(t => t.Name)
                .IsRequired();//Delete

            builder.Entity<Tariff>()
                .Property(t => t.Price)
                .IsRequired();//Delete

            builder.Entity<Tariff>()
                .Property(t => t.Limit)
                .IsRequired();//Delete

            builder.Entity<Operator>()
                .Property(o => o.Name)
                .IsRequired();//Delete

            builder.Entity<StopWord>()
                .Property(sw => sw.Word)
                .IsRequired();//Delete

            #endregion

            // Unique indexes

            builder.Entity<Operator>()
                .HasIndex(o => o.Name)
                .IsUnique();//Delete

            builder.Entity<Code>()
                .HasIndex(i => i.OperatorCode)
                .IsUnique();//Delete

            // Default values

            builder.Entity<Recipient>()
                .Property(r => r.BeenSent)
                .HasDefaultValue(false);//Delete



        }
    }
}
