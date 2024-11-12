using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.EFCore.Repositories
{
    public class SalesDetailRepository : GenericRepository<SalesDetail>, ISalesDetailRepository
    {
        public SalesDetailRepository(ApplicationContext context) : base(context)
        {
        }

    }
}
