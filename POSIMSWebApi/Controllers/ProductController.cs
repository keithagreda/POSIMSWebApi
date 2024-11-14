using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using POSIMSWebApi.Application.Dtos;
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

        [HttpPost("AddProduct")]
        public async Task<IActionResult> AddProductCategory([FromQuery]CreateProductDto input)
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
        public async Task<IActionResult> GetProducts()
        {
            var data = await _unitOfWork.Product.GetAllProductsAsync();

            if (!ModelState.IsValid) 
                return BadRequest(ModelState);
            return Ok(data);
        }

        [HttpGet("GetAllProductsWithCateg")]
        public async Task<IActionResult> GetAllProductsWithCateg()
        {
            var data = await _productService.GetAllProductsWithCategory();
            if(!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(data);
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
    }
}
