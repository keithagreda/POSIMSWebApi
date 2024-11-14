using Domain.Entities;
using Domain.Interfaces;

namespace DataAccess.EFCore.Repositories
{
    public class SalesHeaderRepository : GenericRepository<SalesHeader>, ISalesHeaderRepository
    {
        public SalesHeaderRepository(ApplicationContext context) : base(context)
        {
        }

    }
}
