using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using POSIMSWebApi.Application.Dtos.Inventory;
using POSIMSWebApi.Application.Interfaces;

namespace POSIMSWebApi.Application.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        public InventoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CurrentInventoryDto> GetCurrentStocks()
        {

            // Current Inventory
            var getCurrentInventory = _unitOfWork.InventoryBeginningDetails
                .GetQueryable()
                .Include(e => e.InventoryBeginningFk)
                .Include(e => e.ProductFK)
                .Where(e => e.InventoryBeginningFk.Status == Domain.Enums.InventoryStatus.Open)
                .GroupBy(e => new
                {
                    e.ProductFK.Id,
                    e.ProductFK.Name,
                    e.ProductFK.CreationTime
                })
                .Select(g => new
                {
                    ProductId = g.Key.Id,
                    ProductName = g.Key.Name,
                    CreationTime = g.Key.CreationTime,
                    TotalQuantity = g.Sum(e => e.Qty)
                });


            // Received Stocks
            var receivedStocks = _unitOfWork.StocksReceiving.GetQueryable()
                .Include(e => e.StocksHeaderFk)
                .ThenInclude(e => e.ProductFK)
                .GroupBy(e => e.StocksHeaderFk.ProductId)
                .Select(group => new
                {
                    ProductId = group.Key,
                    TotalQuantity = group.Sum(e => e.Quantity)
                });

            // Sales Details
            var salesDetails = _unitOfWork.SalesDetail.GetQueryable()
                .GroupBy(e => e.ProductId)
                .Select(g => new
                {
                    ProductId = g.Key,
                    TotalQuantity = g.Sum(e => e.Quantity)
                });

            var join = from currInv in getCurrentInventory
                       join recv in receivedStocks on currInv.ProductId equals recv.ProductId into recvGroup
                       from recv in recvGroup.DefaultIfEmpty()
                       join sales in salesDetails on currInv.ProductId equals sales.ProductId into salesGroup
                       from sales in salesGroup.DefaultIfEmpty()
                       select new CurrentInventoryDto
                       {
                           ProductName = currInv.ProductName,
                           ReceivedQty = recv != null ? recv.TotalQuantity : 0,
                           SalesQty = sales != null ? sales.TotalQuantity : 0,
                           CurrentStocks = (currInv != null ? currInv.TotalQuantity : 0) + (recv != null ? recv.TotalQuantity : 0) - (sales != null ? sales.TotalQuantity : 0)
                       };

            var result = join.FirstOrDefault();
            if (result is null) throw new ArgumentNullException("Error! Current Stocks can't be generated", nameof(result));
            return result!;
        }
    }
}
