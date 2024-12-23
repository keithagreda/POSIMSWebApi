using DataAccess.EFCore.UnitOfWorks;
using Domain.ApiResponse;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.EFCore.Repositories
{
    public class InventoryBeginningRepository : GenericRepository<InventoryBeginning>, IInventoryBeginningRepository
    {
        public InventoryBeginningRepository(ApplicationContext context) : base(context)
        {
        }

        public async Task<Guid> CreateOrGetInventoryBeginning()
        {
            var checkIfInventoryIsOpen = await _context.InventoryBeginnings.FirstOrDefaultAsync(e => e.Status == InventoryStatus.Open);
            if(checkIfInventoryIsOpen is not null)
            {
                return checkIfInventoryIsOpen.Id;
            }
            var newInventory = new InventoryBeginning
            {
                Status = InventoryStatus.Open,
                Id = Guid.NewGuid(),
            };

            await _context.InventoryBeginnings.AddAsync(newInventory);
            await _context.SaveChangesAsync();
            return newInventory.Id;
        }

        public async Task<ApiResponse<string>> CloseInventory()
        {
            var getCurrentOpenedInventory = await _context.InventoryBeginnings.FirstOrDefaultAsync(e => e.Status == InventoryStatus.Open);
            if (getCurrentOpenedInventory is null) return ApiResponse<string>.Fail("Error! Inventory can't be closed because there are no open records.");
            var currentOpenedInvDate = getCurrentOpenedInventory.CreationTime.DateTime;
            var currentOpenedInvId = getCurrentOpenedInventory.Id;

            var received = await _context.StocksReceivings.Include(e => e.StocksHeaderFk).Where(e => e.InventoryBeginningId == currentOpenedInvId).GroupBy(e => new
            {
                e.StocksHeaderFk.ProductId
            }).Select(e => new
            {
                ProductId = e.Key.ProductId,
                TotalQuantity = e.Sum(e => e.Quantity)
            }).ToListAsync();

            var sales = await _context.SalesHeaders.Include(e => e.SalesDetails).Include(e => e.InventoryBeginningFk)
                .Where(e => e.InventoryBeginningFk.Status == InventoryStatus.Open).GroupBy(e => e.SalesDetails)

            //get sales and receiving
        }
    }
}
