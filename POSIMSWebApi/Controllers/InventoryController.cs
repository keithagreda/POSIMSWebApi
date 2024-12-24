using Domain.ApiResponse;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using POSIMSWebApi.Application.Dtos.Inventory;
using POSIMSWebApi.Application.Interfaces;

namespace POSIMSWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IInventoryService _inventoryService;
        public InventoryController(IInventoryService inventoryService, IUnitOfWork unitOfWork)
        {
            _inventoryService = inventoryService;
            _unitOfWork = unitOfWork;
        }
        [HttpGet("GetCurrentStocks")]
        public async Task<IActionResult> GetCurrentStocks()
        {
            try
            {
                var data = await _inventoryService.GetCurrentStocks();
                return Ok(data);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpPost("BeginningEntry")]
        public async Task<IActionResult> BeginningEntry(CreateBeginningEntryDto input)
        {
            var data = await _inventoryService.BeginningEntry(input);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(data);
        }
        [HttpPost("CloseInventory")]
        public async Task<ActionResult<ApiResponse<string>>> CloseInventory()
        {
            var data = await _unitOfWork.InventoryBeginning.CloseInventory();
            if(data is null)
            {
                return ApiResponse<string>.Fail("Failed");
            }
            return Ok(data);
        }
    }
}
