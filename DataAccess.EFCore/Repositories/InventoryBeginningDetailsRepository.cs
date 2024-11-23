using Domain.Entities;
using Domain.Interfaces;

namespace DataAccess.EFCore.Repositories
{
    public class InventoryBeginningDetailsRepository : GenericRepository<InventoryBeginningDetails>, IInventoryBeginningDetailsRepository
    {
        public InventoryBeginningDetailsRepository(ApplicationContext context) : base(context)
        {

        }

        public async Task CloseInventoryBeginningDetails()
        {
        }
    }
}
