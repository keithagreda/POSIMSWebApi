using DataAccess.EFCore.Repositories;
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
            SalesDetail = new SalesDetailRepository(_context);
            ProductCategory = new ProductCategoryRepository(_context);
            Product = new ProductRepository(_context);
        }
        public ISalesDetailRepository SalesDetail { get; private set; }
        public IProductCategoryRepository ProductCategory { get; private set; }
        public IProductRepository Product { get; private set; }
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
