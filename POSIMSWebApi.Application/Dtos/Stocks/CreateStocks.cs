using Domain.Entities;
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
        public decimal Quantity { get; set; }
        public int ProductId { get; set; }
        public int StorageLocationId { get; set; }
    }

    public class GetStockDetailsDto
    {
        public int ProductId { get; set; }
        public int StorageLocationId { get; set; }
        public int OverallStock { get; set; }
        public List<StocksDetail> StocksDetails { get; set; }
    }
}
