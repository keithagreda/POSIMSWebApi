using Domain.Entities;
using Domain.Interfaces;

namespace DataAccess.EFCore.Repositories
{
    public class StocksRepository : GenericRepository<Stocks>, IStocksRepository
    {
        public StocksRepository(ApplicationContext context) : base(context)
        {
        }

        
    }
}
