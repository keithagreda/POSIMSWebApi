using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace POSIMSWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost("AddProduct")]
        public async Task<IActionResult> AddProductCategory()
        {
            try
            {
                var categ = await _unitOfWork.ProductCategory.FirstOrDefaultAsync(2);
                var product = new Product
                {
                    Name = "Ice Block",
                    Price = 100,
                    CreatedBy = 1,
                    ProductCategories = new List<ProductCategory>() {categ}
                };
                //var productCategory = new ProductCategory
                //{
                //    Name = "Ice",
                //    CreationTime = DateTimeOffset.Now,
                //    CreatedBy = 1,
                //    ModifiedBy = 0,
                //    DeletedBy = 0,
                //};
                _unitOfWork.Product.Add(product);
                _unitOfWork.Complete();
                return Ok();
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
    }
}
