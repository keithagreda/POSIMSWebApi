﻿using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using POSIMSWebApi.Application.Dtos.ProductDtos;
using POSIMSWebApi.Application.Interfaces;

namespace POSIMSWebApi.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IList<ProductWithCategDto>> GetAllProductsWithCategory()
        {
            var data = _unitOfWork.Product.GetQueryable().Include(e => e.ProductCategories).Select(e => new ProductWithCategDto
            {
                ProductId = e.Id,
                ProductName = e.Name,
                ProductCategories = e.ProductCategories.Select(e => e.Name).ToList()
            }).AsNoTracking().AsSplitQuery();

            return await data.ToListAsync();
        }

        public async Task<string> CreateProduct(CreateProductDto input)
        {
            //data
            //validation
            if(await ValidateProductName(input.Name))
            {
                //if(input.ProductCategoryId != 0)
                //{
                //    var getProductCategories = await _unitOfWork.Product.GetQueryable().Where(e => e.Name == input.Name)
                //    .Include(e => e.ProductCategories)
                //    .Select(e => e.ProductCategories.Select(e => e.Id)).FirstOrDefaultAsync();
                //    if (getProductCategories.Contains(input.ProductCategoryId))
                //    {
                //        return "Invalid Action! Product with this category already exists!";
                //    }

                //    if (!getProductCategories.Contains(input.ProductCategoryId))
                //    {
                //        //save categ
                //        var product = await _unitOfWork.Product.FirstOrDefaultAsync(e => e.Name == input.Name);
                //        var addNewCateg = await _unitOfWork.ProductCategory.FirstOrDefaultAsync(e => e.Id == input.ProductCategoryId);
                //        if (addNewCateg is null) return "Product Creation failed inputted Category doesn't exist!";
                //        product.ProductCategories.Add(addNewCateg);

                //    }
                //}
                
                return "Invalid action! Product name already exists!";
            }
            //getCateg
            ProductCategory? categ = null;
            if(input.ProductCategoryId != 0)
            {
                categ = await _unitOfWork.ProductCategory.FirstOrDefaultAsync(e => e.Id == input.ProductCategoryId);
                if (categ is null) return "Product Creation failed inputted Category doesn't exist!";
            }
            var newProduct = new Product
            {
                Name = input.Name,
                Price = input.Price,
                ProductCategories = new List<ProductCategory>() { categ }
            };

            _unitOfWork.Product.Add(newProduct);
            _unitOfWork.Complete();
            _unitOfWork.Dispose();
            return $"Successfully added {input.Name} in the products list!";
        }

        private async Task<bool> ValidateProductName(string productName)
        {
            var isExist = await _unitOfWork.Product.FirstOrDefaultAsync(e => e.Name == productName);
            if(isExist is not null) return true;
            return false;
        }
        //TO DO MAKE ADD PRODUCT CATEGORY INSTEAD OF PUTTING ON CREATE

    }
}
