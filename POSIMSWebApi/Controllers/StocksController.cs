using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        public async Task<IActionResult> ReceiveStocks(CreateStocksReceivingDto input)
        {
            try
            {
                var result = await _stockReceivingService.ReceiveStocks(input);
                _unitOfWork.Complete();
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                return Ok(result);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
