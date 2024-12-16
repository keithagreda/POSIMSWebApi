using Domain.ApiResponse;
using LanguageExt.Common;
using POSIMSWebApi.Application.Dtos.StocksReceiving;

namespace POSIMSWebApi.Application.Interfaces
{
    public interface IStockReceivingService
    {
        Task<ApiResponse<string>> ReceiveStocks(CreateStocksReceivingDto input);
    }
}