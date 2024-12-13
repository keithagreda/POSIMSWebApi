using Domain.ApiResponse;
using Domain.Entities;
using Domain.Interfaces;
using LanguageExt.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using POSIMSWebApi.Application.Dtos;
using POSIMSWebApi.Application.Dtos.ProductCategory;
using POSIMSWebApi.Application.Dtos.ProductDtos;
using POSIMSWebApi.Application.Interfaces;

namespace POSIMSWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductService _productService;
        public ProductController(IUnitOfWork unitOfWork, IProductService productService)
        {
            _unitOfWork = unitOfWork;
            _productService = productService;
        }

        [HttpPost("CreateProduct")]
        public async Task<ActionResult<ApiResponse<string>>> CreateProduct(CreateProductDto input)
        {
            try
            {
                var result = await _productService.CreateProduct(input);
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                return Ok(result);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        [HttpGet("GetProducts")]
        public async Task<ActionResult<ApiResponse<IList<ProductDto>>>> GetProducts()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var data = await _unitOfWork.Product.GetAllProductsAsync();

            var result = new List<ProductDto>();
            foreach (var product in data)
            {
                var res = new ProductDto
                {
                    DaysTillExpiration = product.DaysTillExpiration,
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    ProdCode = product.ProdCode,
                };
                result.Add(res);
            }
            return Ok(ApiResponse<IList<ProductDto>>.Success(result));
        }

        [HttpGet("GetAllProductsWithCateg")]
        public async Task<ActionResult<ApiResponse<IList<ProductWithCategDto>>>> GetAllProductsWithCateg()
        {
            var data = await _productService.GetAllProductsWithCategory();
            
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                return Ok(data);

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        [HttpGet("GetProductWithCateg/{id}")]
        public async Task<IActionResult> GetProductWithCateg(int id)
        {
            var data = await _unitOfWork.Product.GetQueryable().Include(e => e.ProductCategories).FirstOrDefaultAsync(e => e.Id == id);
            if (data is null) throw new ArgumentNullException($"Product with id: \"{id}\" not found!", nameof(data));
            var result = new ProductWithCategDto
            {
                ProductId = data.Id,
                ProductName = data.Name,
                ProductCategories = data.ProductCategories.Select(e => e.Name).ToList() ?? new List<string>()
            };
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(result);
        }
        [HttpGet("GetProductForEdit/{id}")]
        public async Task<ActionResult<ApiResponse<CreateProductDto>>> GetProductForEdit(int id)
        {
            if (id == 0)
            {
                return ApiResponse<CreateProductDto>.Fail("Invalid action! Id can't be null");
            }
            var data = await _unitOfWork.Product.GetQueryable().Include(e => e.ProductCategories)
                .Select(e => new CreateProductDto
                {

                    DaysTillExpiration = e.DaysTillExpiration,
                    Name = e.Name,
                    Price = e.Price,
                    ProductCategories = e.ProductCategories.Select(e => new ProductCategoryDto
                    {
                        Name = e.Name,
                        Id = e.Id
                    }).ToList()
                }).FirstOrDefaultAsync();

            if(data is null)
            {
                return ApiResponse<CreateProductDto>.Fail("Error! Product Not Found.");
            }

            return Ok(ApiResponse<CreateProductDto>.Success(data));
        }
    }
}
