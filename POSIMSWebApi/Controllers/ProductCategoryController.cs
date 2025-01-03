using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataAccess.EFCore;
using Domain.Entities;
using Domain.Interfaces;
using Domain.ApiResponse;
using POSIMSWebApi.Application.Dtos.ProductCategory;
using Microsoft.AspNetCore.Authorization;
using POSIMSWebApi.Authentication;

namespace POSIMSWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCategoryController : ControllerBase
    {
        private readonly ApplicationContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public ProductCategoryController(ApplicationContext context, IUnitOfWork unitOfWork)
        {
            _context = context;
            _unitOfWork = unitOfWork;
        }

        [Authorize(Roles = UserRole.Admin + "," + UserRole.Inventory)]
        [HttpGet("GetProductCategory")]
        public async Task<ActionResult<ApiResponse<IList<ProductCategoryDto>>>> GetProductCategory()
        {
            var data = await _unitOfWork.ProductCategory.GetAllAsync();

            //convert into dto
            var productCategoriesDto = new List<ProductCategoryDto>();
            foreach (var item in data)
            {
                var res = new ProductCategoryDto
                {
                    Id = item.Id,
                    Name = item.Name,
                };
                productCategoriesDto.Add(res);
            }

            if(data.Count <= 0)
            {
                throw new ArgumentNullException("No Products Found", nameof(data));
            }
            return Ok(ApiResponse<IList<ProductCategoryDto>>.Success(productCategoriesDto));
        }

        [Authorize(Roles = UserRole.Admin + "," + UserRole.Inventory)]
        [HttpPost("AddProductCategory")]
        public ActionResult AddProductCategory()
        {
            try
            {
                var productCategory = new ProductCategory
                {
                    Name = "Ice",
                    CreationTime = DateTimeOffset.Now,
                    CreatedBy = 1,
                    ModifiedBy = 0,
                    DeletedBy = 0,
                };
                _unitOfWork.ProductCategory.Add(productCategory);
                _unitOfWork.Complete();
                return Ok();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
       
    }
}
