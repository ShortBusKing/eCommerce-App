using System;
using System.Linq;
using System.Reflection;
using Core.Entities;
using Core.Entities.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace Infrastructure.Data
{
    public class EcommerceContext : DbContext
    {
        public EcommerceContext(DbContextOptions<EcommerceContext> options) : base(options)
        {
            
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductBrand> ProductBrands { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<DeliveryMethod> DeliveryMethods { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            if(Database.ProviderName == "Microsoft.EntityFrameworkCore.SqlServer")
            {
                modelBuilder.Model.SetIdentitySeed(1);


                modelBuilder.Entity<Product>().Property(e => e.Id).ValueGeneratedOnAdd().IsRequired().UseIdentityColumn(1, 1);
                
               //modelBuilder.Entity<Product>().Property(e => e.Id)
                    //.UseIdentityColumn(1,1)
                    //.Metadata.SetBeforeSaveBehavior (Microsoft.EntityFrameworkCore.Metadata.PropertySaveBehavior.Ignore);
               modelBuilder.Entity<ProductBrand>().Property(e => e.Id).UseIdentityColumn(1, 1);
               modelBuilder.Entity<ProductType>().Property(e => e.Id).UseIdentityColumn(1, 1);
               modelBuilder.Entity<Order>().Property(e => e.Id).UseIdentityColumn(1, 1);
               modelBuilder.Entity<OrderItem>().Property(e => e.Id).UseIdentityColumn(1, 1);
               modelBuilder.Entity<DeliveryMethod>().Property(e => e.Id).UseIdentityColumn(1, 1);
           
            } 
            else 


            //Workaround for sqlite as it doesn't support decimal, datetimeoffset.
            if (Database.ProviderName == "Microsoft.EntityFrameworkCore.Sqlite")
            {
                foreach(var item in modelBuilder.Model.GetEntityTypes())
                {
                    var props = item.ClrType.GetProperties().Where(p => p.PropertyType == typeof(decimal));
                    var dateTimeProperties = item.ClrType.GetProperties().Where(p => p.PropertyType == typeof(DateTimeOffset));
                    foreach(var prop in props)
                    {
                        modelBuilder.Entity(item.Name).Property(prop.Name).HasConversion<double>();
                    }
                    foreach (var prop in dateTimeProperties)
                    {
                        modelBuilder.Entity(item.Name).Property(prop.Name).HasConversion(new DateTimeOffsetToBinaryConverter());
                    }
                }
            }
        }
    }
}