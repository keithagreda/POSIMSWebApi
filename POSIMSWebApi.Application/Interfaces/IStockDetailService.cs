using POSIMSWebApi.Application.Dtos.Stocks;

namespace POSIMSWebApi.Application.Interfaces
{
    public interface IStockDetailService
    {
        Task<int> AutoCreateStocks(CreateStocks input, string transNum);
    }
}