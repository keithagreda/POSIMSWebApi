using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using POSIMSWebApi.Interceptors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.EFCore
{
    public class ApplicationContext : DbContext
    {
        private readonly SoftDeleteInterceptor _softDeleteInterceptor;
        public ApplicationContext(DbContextOptions<ApplicationContext> options, SoftDeleteInterceptor softDeleteInterceptor) : base(options)
        {
            _softDeleteInterceptor = softDeleteInterceptor;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(_softDeleteInterceptor);
        }
        public DbSet<SalesHeader> SalesHeaders { get; set; }
        public DbSet<SalesDetail> SalesDetails { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<StocksReceiving> StocksReceivings { get; set; }
        public DbSet<StockDamageHeader> StockDamageHeaders { get; set; }
        public DbSet<StockDamageDetail> StockDamageDetails { get; set; }
        public DbSet<SalesReturn> SalesReturns { get; set; }
        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                // Check if the entity has an IsDeleted property
                var isDeletedProperty = entityType.FindProperty("IsDeleted");
                if (isDeletedProperty != null && isDeletedProperty.ClrType == typeof(bool))
                {
                    // Get the entity type
                    var parameter = Expression.Parameter(entityType.ClrType, "e");

                    // Create expression: e => e.IsDeleted == false
                    var filter = Expression.Lambda(
                        Expression.Equal(
                            Expression.Property(parameter, "IsDeleted"),
                            Expression.Constant(false)
                        ),
                        parameter
                    );

                    // Apply filter to entity
                    modelBuilder.Entity(entityType.ClrType).HasQueryFilter(filter);
                }
            }
        }
    }
}
