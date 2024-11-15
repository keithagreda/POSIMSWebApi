using Domain.Entities;

namespace DataAccess.EFCore.Repositories
{
    public class StocksReceivingRepository : GenericRepository<StocksReceiving>, IStocksReceivingRepository
    {
        public StocksReceivingRepository(ApplicationContext context) : base(context)
        {
        }

    }
}
