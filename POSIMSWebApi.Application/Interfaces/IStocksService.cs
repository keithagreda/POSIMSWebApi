using POSIMSWebApi.Application.Dtos.Stocks;

namespace POSIMSWebApi.Application.Interfaces
{
    public interface IStocksService
    {
        Task<string> AutoCreateStocks(CreateStocks input);
    }
}