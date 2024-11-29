using POSIMSWebApi.Application.Dtos.Sales;

namespace POSIMSWebApi.Application.Interfaces
{
    public interface ISalesService
    {
        Task CreateSales(CreateOrEditSalesDto input);
    }
}