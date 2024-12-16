using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSIMSWebApi.Application.Dtos.Pagination
{
    public class PaginationParams
    {
        public int? PageNumber { get; set; } = 1; // Default to the first page
        public int? PageSize { get; set; } = 10;  // Default page size
    }
}
