using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSIMSWebApi.Application.Dtos.Stocks
{
    public class CreateStocks
    {
        public int Id { get; set; }
        public decimal Quantity { get; set; }
        public string TransNum { get; set; }
        public int ProductId { get; set; }
    }
}
