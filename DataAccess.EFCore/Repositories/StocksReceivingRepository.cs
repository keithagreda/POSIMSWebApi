using Domain.Entities;
using Domain.Interfaces;

namespace DataAccess.EFCore.Repositories
{
    public class StocksReceivingRepository : GenericRepository<StocksReceiving>, IStocksReceivingRepository
    {
        public StocksReceivingRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
