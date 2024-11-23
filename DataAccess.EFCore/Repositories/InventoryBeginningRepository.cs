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

        public async Task CloseInventory()
        {
            var openInventory = await _context.InventoryBeginnings.FirstOrDefaultAsync(e => e.Status == InventoryStatus.Open);
            if(openInventory is null)
            {
                throw new Exception("Invalid Action! Can't find an open inventory.");
            }

            var stocksReceived = _context.StocksReceivings.AsQueryable();
            //var sales
        }
    }
}
