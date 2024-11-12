using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.EFCore
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
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
    }
}
