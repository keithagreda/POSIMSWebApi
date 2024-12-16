using Domain.ApiResponse;
using LanguageExt.Common;
using POSIMSWebApi.Application.Dtos.Sales;
using POSIMSWebApi.Application.Dtos.Stocks;

namespace POSIMSWebApi.Application.Interfaces
{
    public interface IStockDetailService
    {
        Task<ApiResponse<int>> AutoCreateStocks(CreateStocks input, string transNum);
        Task<Result<string>> StockDetailSalesHandler(CreateSalesDetailDto input);
    }
}