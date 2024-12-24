using Domain.ApiResponse;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using POSIMSWebApi.Application.Dtos.Pagination;
using POSIMSWebApi.Application.Dtos.ProductDtos;
using POSIMSWebApi.Application.Dtos.Sales;
using POSIMSWebApi.Application.Interfaces;
using POSIMSWebApi.Application.Services;
using POSIMSWebApi.QueryExtensions;

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
        [HttpGet("GetSales")]
        public async Task<ActionResult<ApiResponse<PaginatedResult<SalesHeaderDto>>>> GetSales([FromQuery]FilterSales input)
        {
            //dapat naay created by
            var data = await _unitOfWork.SalesHeader.GetQueryable().Include(e => e.SalesDetails)
                .ThenInclude(e => e.ProductFk)
                .Include(e => e.CustomerFk)
                .ToPaginatedResult(input.PageNumber, input.PageSize)
                .OrderByDescending(e => e.CreationTime)
                .Select(e => new SalesHeaderDto
                {
                    Id = e.Id,
                    TotalAmount = e.TotalAmount,
                    TransactionDate = e.CreationTime.ToString("f"),
                    TransNum = e.TransNum,
                    SoldBy = "",
                    CustomerName = e.CustomerFk != null ? string.Format("{0} {1}", e.CustomerFk.Firstname, e.CustomerFk.Lastname) : "N/A",
                    SalesDetailsDto = e.SalesDetails.Select(e => new SalesDetailDto
                    {
                        ActualSellingPrice = e.ActualSellingPrice,
                        Amount = e.Amount,
                        Discount = e.Discount,
                        ProductName = e.ProductFk.Name,
                        Quantity = e.Quantity,
                        ProductPrice = e.ProductPrice
                    }).ToList()
                }).ToListAsync();

            var result = new PaginatedResult<SalesHeaderDto>(data, data.Count, (int)input.PageNumber, (int)input.PageSize);

            return ApiResponse<PaginatedResult<SalesHeaderDto>>.Success(result);
                
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
        public async Task<ActionResult<ApiResponse<string>>> CreateSales(CreateOrEditSalesV1Dto input)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _salesService.CreateSales(input);
            _unitOfWork.Complete();
            return result;
        }

        [HttpGet("GetTotalSales")]
        public async Task<ActionResult<ApiResponse<GetTotalSalesDto>>> GetTotalSales()
        {
            var result = await _salesService.GetTotalSales();
            return Ok(result);
        }

        [HttpGet("GetTotalMonthlySales")]
        public async Task<ActionResult<ApiResponse<List<PerMonthSalesDto>>>> GetPerMonthSales(int? year)
        {
            var result = await _salesService.GetPerMonthSales(year);
            return Ok(result);
        }
    }
}
