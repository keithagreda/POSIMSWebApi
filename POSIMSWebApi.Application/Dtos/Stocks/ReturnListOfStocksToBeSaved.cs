using Domain.Entities;

namespace POSIMSWebApi.Application.Dtos.Stocks
{
    public class ReturnListOfStocksToBeSaved
    {
        public int HeaderId { get; set; }
        public List<StocksDetail> StockDetails { get; set; }
    }
}
