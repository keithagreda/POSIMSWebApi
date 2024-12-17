using Domain.ApiResponse;
using LanguageExt.Common;
using POSIMSWebApi.Application.Dtos.Sales;

namespace POSIMSWebApi.Application.Interfaces
{
    public interface ISalesService
    {
        Task<ApiResponse<string>> CreateSalesFromTransNum(CreateOrEditSalesDto input);
        Task<ApiResponse<string>> CreateSales(CreateOrEditSalesV1Dto input);

    }
}