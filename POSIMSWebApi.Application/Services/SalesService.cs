using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using POSIMSWebApi.Application.Dtos.ProductDtos;
using POSIMSWebApi.Application.Dtos.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSIMSWebApi.Application.Services
{
    public class SalesService
    {
        private readonly IUnitOfWork _unitOfWork;
        public SalesService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CreateSales(CreateOrEditSalesDto input)
        {
            if(input.SalesHeaderId is null)
            {
                //return for now still haven't decided how to handle errors
                //planning to make result pattern
                return;
            }

            var listOfProductIds = input.CreateSalesDetailDtos.Select(e => e.ProductId).ToList();

            var product = await _unitOfWork.Product.GetQueryable().Where(e => listOfProductIds.Contains(e.Id)).Select(e => new CreateProductSales
            {
                Id = e.Id,
                Name = e.Name,
                Price = e.Price
            }).ToListAsync();

            if(product.Count <= 0)
            {
                return;
            }

            var customer = await _unitOfWork.Customer.FirstOrDefaultAsync(e => e.Id == input.CustomerId);
            if(customer == null)
            {
                return;
            }

            //create sales header
            var salesHeader = new SalesHeader
            {
                Id = Guid.NewGuid(),
                CustomerId = customer.Id,
                TotalAmount = 0,
                TransNum = await GenerateTransNum()
            };

            var salesDetails = new List<SalesDetail>();
            foreach(var sDetail in input.CreateSalesDetailDtos)
            {
                var currProduct = product.FirstOrDefault(e => e.Id == sDetail.ProductId);
                var currAmount = CalculateAmount(product, sDetail.ProductId, sDetail.Quantity);
                var saleDetail = new SalesDetail
                {
                    Id = Guid.NewGuid(),
                    ActualSellingPrice = sDetail.ActualSellingPrice,
                    Amount = currAmount,
                    Quantity = sDetail.Quantity,
                    ProductPrice = currProduct != null ? currProduct.Price : 0,
                    ProductId = sDetail.ProductId,
                    Discount = CalculateDiscount(sDetail.ActualSellingPrice, currAmount),
                    SalesHeaderId = salesHeader.Id
                };
                salesDetails.Add(saleDetail);
            }
            
        }

        private async Task<string> GenerateTransNum()
        {
            var currentDate = DateTime.UtcNow;
            //SGT
            var shortDate = currentDate.AddHours(8).ToString("yyyyMMdd");
            var salesHeaderCount = await _unitOfWork.SalesHeader.GetQueryable().Where(e => e.CreationTime.Date == currentDate.Date).CountAsync() + 1;
            var formattedCount = salesHeaderCount.ToString("D4");
            var transNum = $"T{shortDate}-{formattedCount}";
            return transNum;
        }

        private decimal CalculateAmount(List<CreateProductSales> product, int productId, decimal quantity)
        {
            var productPrice = product.FirstOrDefault(e => e.Id == productId).Price;
            if(productPrice == 0)
            {
                throw new ArgumentNullException("Error! Product Price not found", nameof(productPrice));
            }
            return productPrice * quantity;
        }

        private decimal CalculateDiscount(decimal asp, decimal amt)
        {
            if (asp == 0)
            {
                return 0;
            }

            decimal discountAmount = amt - asp;
            decimal disPercentage = (discountAmount / asp) * 100;
            return disPercentage;
        }
    }
}
