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
        public async Task<IActionResult> CreateSalesFromTransNum(CreateOrEditSalesDto input)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _salesService.CreateSalesFromTransNum(input);
            _unitOfWork.Complete();

            return result.Match<IActionResult>(
           success => CreatedAtAction(nameof(CreateSalesFromTransNum), new { id = input.CreateSalesDetailDtos.TransNumReaderDto.TransNum }, success),
           error => BadRequest(error));
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
