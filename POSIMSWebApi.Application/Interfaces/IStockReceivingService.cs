using LanguageExt.Common;
using POSIMSWebApi.Application.Dtos.StocksReceiving;

namespace POSIMSWebApi.Application.Interfaces
{
    public interface IStockReceivingService
    {
        Task<Result<string>> ReceiveStocks(CreateStocksReceivingDto input);
    }
}