using Domain.ApiResponse;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using POSIMSWebApi.Application.Dtos.StocksReceiving;
using POSIMSWebApi.Application.Interfaces;

namespace POSIMSWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StocksController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStockReceivingService _stockReceivingService;
        public StocksController(IUnitOfWork unitOfWork, IStockReceivingService stockReceivingService)
        {
            _unitOfWork = unitOfWork;
            _stockReceivingService = stockReceivingService;
        }

        [HttpPost("ReceiveStocks")]

        public async Task<ActionResult<ApiResponse<string>>> ReceiveStocks(CreateStocksReceivingDto input)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _stockReceivingService.ReceiveStocks(input);
            _unitOfWork.Complete();

            return Ok(result);

           // return result.Match<IActionResult>(
           //success => CreatedAtAction(nameof(ReceiveStocks), new { id = input.ProductId, input.Quantity, input.StorageLocationId }, success),
           //error => BadRequest(error));
        }
        [HttpGet("GetReceivingStocks")]
        public async Task<ActionResult<ApiResponse<List<GetAllStocksReceivingDto>>>> GetReceiveStocks()
        {
            var result = await _unitOfWork.StocksReceiving.GetQueryable()
                .Include(e => e.StocksHeaderFk).ThenInclude(e => e.ProductFK)
                .Include(e => e.StocksHeaderFk.StorageLocationFk)
                .OrderByDescending(e => e.CreationTime)
                .Select(e => new GetAllStocksReceivingDto
                {
                    ProductId = e.StocksHeaderFk.ProductId,
                    ProductName = e.StocksHeaderFk.ProductFK.Name,
                    TransNum = e.TransNum,
                    Quantity = e.Quantity,
                    StorageLocation = e.StocksHeaderFk != null && e.StocksHeaderFk.StorageLocationFk != null
                    ? e.StocksHeaderFk.StorageLocationFk.Name
                    : "N/A",
                            StorageLocationId = e.StocksHeaderFk != null && e.StocksHeaderFk.StorageLocationId.HasValue
                    ? e.StocksHeaderFk.StorageLocationId.Value
                    : 0,
                    DateReceived = e.CreationTime.ToString("f"),
                    Id = e.Id
                }).ToListAsync();

            return Ok(ApiResponse<List<GetAllStocksReceivingDto>>.Success(result));
        }
    }
}
