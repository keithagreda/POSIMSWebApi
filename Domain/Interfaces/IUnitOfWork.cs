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
        IStocksDetailRepository StocksDetail { get; }
        IStocksHeaderRepository StocksHeader { get; }
        IStocksReceivingRepository StocksReceiving { get; }
        IInventoryBeginningRepository InventoryBeginning { get; }
        IInventoryBeginningDetailsRepository InventoryBeginningDetails { get; }
        int Complete();
    }
}
