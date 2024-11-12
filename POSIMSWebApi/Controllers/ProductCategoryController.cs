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
        [HttpPost]
        public ActionResult AddProductCateogry()
        {
            var productCategory = new ProductCategory
            {
                Name = "Ice",
                CreationTime = DateTime.Now,
            };
            _unitOfWork.ProductCategory.Add(productCategory);
            _unitOfWork.Complete();
            return Ok();
        }
       
    }
}
