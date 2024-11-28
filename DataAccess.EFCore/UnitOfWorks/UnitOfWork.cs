using DataAccess.EFCore.Repositories;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.EFCore.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _context;
        public UnitOfWork(ApplicationContext context)
        {
            _context = context;
            SalesHeader = new SalesHeaderRepository(_context);
            SalesDetail = new SalesDetailRepository(_context);
            ProductCategory = new ProductCategoryRepository(_context);
            Product = new ProductRepository(_context);
            StocksDetail = new StocksDetailRepository(_context);
            StocksReceiving = new StocksReceivingRepository(_context);
            StocksHeader= new StocksHeaderRepository(_context);
            InventoryBeginning = new InventoryBeginningRepository(_context);
            InventoryBeginningDetails = new InventoryBeginningDetailsRepository(_context);
            Customer = new CustomerRepository(_context);
        }
        public ISalesHeaderRepository SalesHeader { get; private set; }
        public ISalesDetailRepository SalesDetail { get; private set; }
        public IProductCategoryRepository ProductCategory { get; private set; }
        public IProductRepository Product { get; private set; }
        public IStocksReceivingRepository StocksReceiving { get; private set; }
        public IStocksDetailRepository StocksDetail { get; private set; }
        public IStocksHeaderRepository StocksHeader { get; private set; }
        public IInventoryBeginningRepository InventoryBeginning { get; private set; }
        public IInventoryBeginningDetailsRepository InventoryBeginningDetails { get; private set; }
        public ICustomerRepository Customer { get; private set; }
        public int Complete()
        {
            return _context.SaveChanges();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
