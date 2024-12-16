using Domain.ApiResponse;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using POSIMSWebApi.Application.Dtos.Sales;
using POSIMSWebApi.Application.Interfaces;
using POSIMSWebApi.Application.Services;

namespace POSIMSWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly ISalesService _salesService;
        private readonly IUnitOfWork _unitOfWork;
        public SalesController(ISalesService salesService, IUnitOfWork unitOfWork)
        {
            _salesService = salesService;
            _unitOfWork = unitOfWork;
        }

        [HttpPost("CreateSalesFromTransNum")]
        public async Task<ActionResult<ApiResponse<string>>> CreateSalesFromTransNum(CreateOrEditSalesDto input)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                var result = await _salesService.CreateSalesFromTransNum(input);
                _unitOfWork.Complete();

                return result;
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
            
        }

        [HttpPost("CreateSales")]
        public async Task<IActionResult> CreateSales(CreateOrEditSalesDto input)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _salesService.CreateSales(input);
            _unitOfWork.Complete();

            return result.Match<IActionResult>(
           success => CreatedAtAction(nameof(CreateSales), new { id = input.CreateSalesDetailDtos }, success),
           error => BadRequest(error));
        }
    }
}
