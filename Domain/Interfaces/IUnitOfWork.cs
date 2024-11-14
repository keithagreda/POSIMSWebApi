using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ISalesHeaderRepository SalesHeader { get; }
        ISalesDetailRepository SalesDetail {  get; }
        IProductCategoryRepository ProductCategory { get; }
        IProductRepository Product { get; }
        int Complete();
    }
}
