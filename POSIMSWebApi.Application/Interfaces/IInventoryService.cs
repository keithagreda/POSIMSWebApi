using POSIMSWebApi.Application.Dtos.Inventory;

namespace POSIMSWebApi.Application.Interfaces
{
    public interface IInventoryService
    {
        Task<CurrentInventoryDto> GetCurrentStocks();
    }
}