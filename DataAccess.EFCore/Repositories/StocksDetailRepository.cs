using Domain.Entities;
using Domain.Interfaces;

namespace DataAccess.EFCore.Repositories
{
    public class StocksDetailRepository : GenericRepository<StocksDetail>, IStocksDetailRepository
    {
        public StocksDetailRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
