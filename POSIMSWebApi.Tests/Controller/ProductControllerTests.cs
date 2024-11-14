using Domain.Entities;
using Domain.Interfaces;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using POSIMSWebApi.Application.Interfaces;
using POSIMSWebApi.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSIMSWebApi.Tests.Controller
{
    public class ProductControllerTests
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductService _productService;
        public ProductControllerTests()
        {
            _productRepository = A.Fake<IProductRepository>();
            _unitOfWork = A.Fake<IUnitOfWork>();
            _productService = A.Fake<IProductService>();
        }

        [Fact]
        public async Task ProductController_GetProducts_ReturnOK()
        {
            //Arrange
            var products = A.Fake<ICollection<Product>>();
            var productList = A.Fake<List<Product>>();
            var controller = new ProductController(_unitOfWork, _productService);
            //Act
            var result = await controller.GetProducts();
            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
        }
    }
}
