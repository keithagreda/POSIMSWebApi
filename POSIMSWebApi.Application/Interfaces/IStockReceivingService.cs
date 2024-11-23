using POSIMSWebApi.Application.Dtos.StocksReceiving;

namespace POSIMSWebApi.Application.Interfaces
{
    public interface IStockReceivingService
    {
        Task<string> ReceiveStocks(CreateStocksReceivingDto input);
    }
}