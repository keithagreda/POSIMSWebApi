using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSIMSWebApi.Application.Dtos.Inventory
{
    public class CurrentInventoryDto
    {
        public string ProductName { get; set; }
        public decimal ReceivedQty { get; set; }
        public decimal SalesQty { get; set; }
        public decimal BegQty { get; set; }
        public decimal CurrentStocks { get; set; }
    }

    public class CreateBeginningEntryDto
    {
        public int ProductId { get; set; }
        //public string ProductName { get; set;}
        public decimal ReceivedQty { get; set;}
    }
}
