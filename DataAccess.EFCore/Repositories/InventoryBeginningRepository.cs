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
            try
            {
                var getCurrentOpenedInventory = await _context.InventoryBeginnings
                    .FirstOrDefaultAsync(e => e.Status == InventoryStatus.Open);

                if (getCurrentOpenedInventory is null)
                    return ApiResponse<string>.Fail("Error! Inventory can't be closed because there are no open records.");

                var currentOpenedInvDate = getCurrentOpenedInventory.CreationTime;
                var currentDateTime = DateTimeOffset.UtcNow;
                var currentOpenedInvId = getCurrentOpenedInventory.Id;

                var received = await _context.StocksReceivings
                    .Include(e => e.StocksHeaderFk)
                    .Where(e => e.InventoryBeginningId == currentOpenedInvId &&
                                e.CreationTime >= currentOpenedInvDate && 
                                e.CreationTime <= currentDateTime)
                    .GroupBy(e => new
                    {
                        e.StocksHeaderFk.ProductId
                    })
                    .Select(e => new
                    {
                        ProductId = e.Key.ProductId,
                        TotalQuantity = e.Sum(x => x.Quantity)
                    })
                    .ToListAsync();

                //var sales = await _context.SalesHeaders.Include(e => e.SalesDetails).Include(e => e.InventoryBeginningFk)
                //    .Where(e => e.InventoryBeginningFk.Status == InventoryStatus.Open).GroupBy(e => e.SalesDetails)

                var salesDetails = await _context.SalesDetails.Include(e => e.SalesHeaderFk.InventoryBeginningFk)
                    .Where(e => e.SalesHeaderFk.InventoryBeginningFk.Status == InventoryStatus.Open
                    && e.CreationTime >= currentOpenedInvDate
                    && e.CreationTime <= currentDateTime
                    )
                    .GroupBy(e => e.ProductId)
                    .Select(e => new
                    {
                        ProductId = e.Key,
                        TotalQuantity = e.Sum(e => e.Quantity)
                    }).ToListAsync();

                //get inventory beginning details
                var inventoryDetails = await _context.InventoryBeginningDetails.Include(e => e.InventoryBeginningFk)
                    .Where(e => e.InventoryBeginningFk.Status == InventoryStatus.Open
                    && e.CreationTime >= currentOpenedInvDate && e.CreationTime <= currentDateTime).Select(e => new
                    {
                        ProductId = e.ProductId,
                        Quantity = e.Qty
                    }).ToListAsync();
                //close existing inventory
                getCurrentOpenedInventory.Status = InventoryStatus.Closed;
                getCurrentOpenedInventory.TimeClosed = DateTime.UtcNow;

                //create new inventory beginning
                InventoryBeginning newInventory = new InventoryBeginning
                {
                    Id = Guid.NewGuid(),
                    Status = 0,
                };

                //join 3 tables
                var join = (from i in inventoryDetails
                            join r in received
                            on i.ProductId equals r.ProductId
                            join s in salesDetails
                            on i.ProductId equals s.ProductId
                            select new InventoryBeginningDetails
                            {

                                ProductId = i.ProductId,
                                Qty = (i.Quantity + r.TotalQuantity) - s.TotalQuantity,
                                InventoryBeginningId = newInventory.Id
                            }).ToList();



                //insert to inventory beginning details
                await _context.InventoryBeginnings.AddAsync(newInventory);
                await _context.InventoryBeginningDetails.AddRangeAsync(join);
                
                await _context.SaveChangesAsync();

                return ApiResponse<string>.Success("Success!");
            }
            catch (Exception ex)
            {
 
                throw ex;
            }

            //get sales and receiving
        }
    }
}
