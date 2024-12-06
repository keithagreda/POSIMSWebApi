using Domain.Entities;
using Domain.Error;
using Domain.Interfaces;
using LanguageExt.Common;
using Microsoft.EntityFrameworkCore;
using POSIMSWebApi.Application.Dtos.ProductDtos;
using POSIMSWebApi.Application.Dtos.Sales;
using POSIMSWebApi.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace POSIMSWebApi.Application.Services
{
    public class SalesService : ISalesService
    {
        private readonly IUnitOfWork _unitOfWork;
        public SalesService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// FOR SINGLE TRANSNUM FIRST
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<Result<string>> CreateSalesFromTransNum(CreateOrEditSalesDto input)
        {
            //access product stock details
            var stocksReceiving = _unitOfWork.StocksReceiving.GetQueryable()
                .Include(e => e.StocksHeaderFk);

            //projection to get details product id and loc
            var productStockDetails = await stocksReceiving.Where(e => e.TransNum == input.CreateSalesDetailDtos.TransNumReaderDto.TransNum).Select(e => new
            {
                e.StocksHeaderFk.ProductId,
                e.StocksHeaderFk.StorageLocationId
            }).FirstOrDefaultAsync();

            //TO DO: Validation for product details

            //get overall stocks for the same product in the same location
            var getStocks = stocksReceiving.ThenInclude(e => e.StocksDetails)
                .Where(e => e.StocksHeaderFk.ProductId == productStockDetails.ProductId
                && e.StocksHeaderFk.StorageLocationId == productStockDetails.StorageLocationId)
                .OrderByDescending(e => e.StocksHeaderFk.ExpirationDate)
                .SelectMany(e => e.StocksHeaderFk.StocksDetails).Where(e => e.Unavailable == false);

            var processedStocks = new List<StocksDetail>();
            var stocksCount = await getStocks.CountAsync();
            if (stocksCount < input.CreateSalesDetailDtos.TransNumReaderDto.Quantity)
            {
                //TO DO::
                //make remarks for sales that stocks were in a deficit but still continue the transaction
                //since stocks were on hand
            }

            var getStocksQty = await getStocks.ToListAsync();

            for(var i = 0; i < input.CreateSalesDetailDtos.TransNumReaderDto.Quantity; i++)
            {
                getStocksQty[i].Unavailable = true; 
            }

            //proceed to creating sales detail
            var product = await _unitOfWork.Product.GetQueryable().Where(e => e.Id == productStockDetails.ProductId).Select(e => new CreateProductSales
            {
                Id = e.Id,
                Name = e.Name,
                Price = e.Price
            }).ToListAsync();

            if (product.Count <= 0)
            {
                var error = new ValidationException("Error! Product not found!");
                return new Result<string>(error);
            }

            //create sales header
            var salesHeader = new SalesHeader()
            {
                Id = Guid.NewGuid(),
                TotalAmount = 0,
                TransNum = await GenerateTransNum()
            };

            if (input.CustomerId is not null)
            {
                var customer = await _unitOfWork.Customer.FirstOrDefaultAsync(e => e.Id == input.CustomerId);

                if (customer is null)
                {
                    var error = new ValidationException("Error! Customer not found.");
                    return new Result<string>(error);
                }

                salesHeader.CustomerId = customer.Id;
            }

            //figure out a way to deplete stocks based on batchnumber

            var currAmount = CalculateAmount(product, productStockDetails.ProductId, input.CreateSalesDetailDtos.TransNumReaderDto.Quantity);
            var saleDetail = new SalesDetail
            {
                Id = Guid.NewGuid(),
                ActualSellingPrice = input.CreateSalesDetailDtos.ActualSellingPrice,
                Amount = currAmount,
                Quantity = input.CreateSalesDetailDtos.TransNumReaderDto.Quantity,
                ProductPrice = product != null ? product.FirstOrDefault().Price : 0,
                ProductId = productStockDetails.ProductId,
                Discount = CalculateDiscount(input.CreateSalesDetailDtos.ActualSellingPrice, currAmount),
                SalesHeaderId = salesHeader.Id
            };

            

            await _unitOfWork.SalesHeader.AddAsync(salesHeader);
            await _unitOfWork.SalesDetail.AddAsync(saleDetail);
            _unitOfWork.Complete();
            return new Result<string>("Success!");
        }

        public async Task<Result<string>> CreateSales(CreateOrEditSalesDto input)
        {
            //if (input.SalesHeaderId is null)
            //{
            //    var error = new ValidationException("Error! SalesHeaderId Can't be null.");
            //    return new Result<string>(error);
            //    //return for now still haven't decided how to handle errors
            //    //planning to make result pattern
            //}

            //var listOfProductIds = input.CreateSalesDetailDtos.Select(e => e.ProductId).ToList();

            //var product = await _unitOfWork.Product.GetQueryable().Where(e => listOfProductIds.Contains(e.Id)).Select(e => new CreateProductSales
            //{
            //    Id = e.Id,
            //    Name = e.Name,
            //    Price = e.Price
            //}).ToListAsync();

            //if (product.Count <= 0)
            //{
            //    var error = new ValidationException("Error! Product not found!");
            //    return new Result<string>(error);
            //}

            ////create sales header
            //var salesHeader = new SalesHeader()
            //{
            //    Id = Guid.NewGuid(),
            //    TotalAmount = 0,
            //    TransNum = await GenerateTransNum()
            //};

            //if (input.CustomerId is not null)
            //{
            //    var customer = await _unitOfWork.Customer.FirstOrDefaultAsync(e => e.Id == input.CustomerId);

            //    if (customer is null) 
            //    {
            //        var error = new ValidationException("Error! Customer not found.");
            //        return new Result<string>(error);
            //    }

            //    salesHeader.CustomerId = customer.Id;
            //}

            ////figure out a way to deplete stocks based on batchnumber

            //var salesDetails = new List<SalesDetail>();
            //foreach (var sDetail in input.CreateSalesDetailDtos)
            //{
            //    var currProduct = product.FirstOrDefault(e => e.Id == sDetail.ProductId);
            //    var currAmount = CalculateAmount(product, sDetail.ProductId, sDetail.Quantity);
            //    var saleDetail = new SalesDetail
            //    {
            //        Id = Guid.NewGuid(),
            //        ActualSellingPrice = sDetail.ActualSellingPrice,
            //        Amount = currAmount,
            //        Quantity = sDetail.Quantity,
            //        ProductPrice = currProduct != null ? currProduct.Price : 0,
            //        ProductId = sDetail.ProductId,
            //        Discount = CalculateDiscount(sDetail.ActualSellingPrice, currAmount),
            //        SalesHeaderId = salesHeader.Id
            //    };
            //    salesDetails.Add(saleDetail);
            //}

            //await _unitOfWork.SalesHeader.AddAsync(salesHeader);
            //await _unitOfWork.SalesDetail.AddRangeAsync(salesDetails);
            //_unitOfWork.Complete();
            return new Result<string>("Success!");
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
            if (productPrice == 0)
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
