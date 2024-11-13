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
        [HttpGet("GetProductCategory")]
        public async Task<ActionResult> GetProductCategory()
        {
            var data = await _unitOfWork.ProductCategory.GetAllAsync();
            if(data.Count <= 0)
            {
                throw new ArgumentNullException("No Products Found", nameof(data));
            }
            return Ok(data);
        }
        [HttpPost]
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
