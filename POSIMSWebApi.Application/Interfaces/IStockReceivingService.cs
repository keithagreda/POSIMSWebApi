using Domain.ApiResponse;
using LanguageExt.Common;
using POSIMSWebApi.Application.Dtos.Stocks;
using POSIMSWebApi.Application.Dtos.StocksReceiving;

namespace POSIMSWebApi.Application.Interfaces
{
    public interface IStockReceivingService
    {
        Task<ApiResponse<string>> ReceiveStocks(CreateStocksReceivingDto input);
        Task<ApiResponse<List<GetStocksGenerationDto>>> GetStocksGeneration(GetStocksGenerationInput input);
    }
}