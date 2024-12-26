using Domain.ApiResponse;
using POSIMSWebApi.Application.Dtos.Inventory;

namespace POSIMSWebApi.Application.Interfaces
{
    public interface IInventoryService
    {
        Task<ApiResponse<List<CurrentInventoryDto>>> GetCurrentStocks();
        Task<string> BeginningEntry(CreateBeginningEntryDto input);
    }
}