using LanguageExt.Common;
using POSIMSWebApi.Application.Dtos.Sales;

namespace POSIMSWebApi.Application.Interfaces
{
    public interface ISalesService
    {
        Task<Result<string>> CreateSalesFromTransNum(CreateOrEditSalesDto input);
        Task<Result<string>> CreateSales(CreateOrEditSalesDto input);

    }
}