using Domain.Entities;
using Domain.Error;
using Domain.Interfaces;
using LanguageExt.Common;
using Microsoft.EntityFrameworkCore;
using POSIMSWebApi.Application.Dtos.ProductDtos;
using POSIMSWebApi.Application.Dtos.Sales;
using POSIMSWebApi.Application.Dtos.Stocks;
using POSIMSWebApi.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
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
            try
            {
                //access product stock details
                var stocksReceiving = _unitOfWork.StocksReceiving.GetQueryable()
                    .Include(e => e.StocksHeaderFk);

                var transNumReaderDto = input.CreateSalesDetailDtos.Select(e => e.TransNumReaderDto).ToList();

                //projection to get details product id and loc
                var productStockDetails = await stocksReceiving.Where(e => transNumReaderDto.Select(e => e.TransNum).Contains(e.TransNum)).Select(e => new
                {
                    e.TransNum,
                    e.StocksHeaderFk.ProductId,
                    e.StocksHeaderFk.StorageLocationId,
                }).ToListAsync();

                var transDetails = transNumReaderDto.Select(e => new TransDetails
                {
                    ProductId = productStockDetails.FirstOrDefault(p => p.TransNum == e.TransNum)?.ProductId ?? 0,
                    StorageLocationId = productStockDetails.FirstOrDefault(s => s.TransNum == e.TransNum)?.StorageLocationId ?? 0,
                    Quantity = e.Quantity,
                    TransNum = e.TransNum
                }).ToList();

                //Validation for product details
                if (productStockDetails.Count <= 0)
                {
                    var error = new ArgumentNullException(nameof(input.CreateSalesDetailDtos));
                    return new Result<string>(error);
                }

                var product = await _unitOfWork.Product.GetQueryable().Where(e => transDetails.Select(e => e.ProductId).Contains(e.Id)).Select(e => new CreateProductSales
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

                var resGetStocks = new List<GetStockDetailsDto>();
                var saleDetails = new List<SalesDetail>();
                //TO DO FIGURE OUT HOW TO DEDUCT QTY IF STOCKS ARE NOT ENOUGH
                foreach (var item in transDetails)
                {
                    //to deduct items from stocks
                    var res = await stocksReceiving.Include(e => e.StocksHeaderFk.StocksDetails)
                        .Where(e => e.StocksHeaderFk.ProductId == item.ProductId
                        && e.StocksHeaderFk.StorageLocationId == item.StorageLocationId)
                        .OrderByDescending(e => e.StocksHeaderFk.ExpirationDate)
                        .Select(e => new GetStockDetailsDto
                        {
                            ProductId = e.StocksHeaderFk.ProductId,
                            StorageLocationId = e.StocksHeaderFk.StorageLocationId != null ? (int)e.StocksHeaderFk.StorageLocationId : 0,
                            OverallStock = e.StocksHeaderFk.StocksDetails.Count(),
                            StocksDetails = new List<StocksDetail>(e.StocksHeaderFk.StocksDetails
                            .Where(e => e.Unavailable == false).Take(item.Quantity))
                        }).FirstOrDefaultAsync();
                    if (res is null)
                    {
                        return new Result<string>(new ArgumentNullException("Error! A Product can't be found..."));
                    }
                    resGetStocks.Add(res);
                    //to create stock details
                    var currAmount = CalculateAmount(product, item.ProductId, item.Quantity);
                    var saleDetail = new SalesDetail
                    {
                        Id = Guid.NewGuid(),
                        ActualSellingPrice = 0, //TEMPORARY
                        Amount = currAmount,
                        Quantity = item.Quantity,
                        ProductPrice = product != null ? product.FirstOrDefault().Price : 0,
                        ProductId = item.ProductId,
                        Discount = 0, // TODO: Temporary CalculateDiscount(input.CreateSalesDetailDtos.ActualSellingPrice, currAmount),
                        SalesHeaderId = salesHeader.Id
                    };
                    saleDetails.Add(saleDetail);
                }

                var stocksToBeDeducted = resGetStocks.SelectMany(e => e.StocksDetails);
                var stocksDeductedCount = await _unitOfWork.StocksDetail.UpdateRangeAsync(stocksToBeDeducted, null, stockDetail =>
                {
                    stockDetail.Unavailable = true;
                });

                await _unitOfWork.SalesHeader.AddAsync(salesHeader);
                await _unitOfWork.SalesDetail.AddRangeAsync(saleDetails);
                _unitOfWork.Complete();
                return new Result<string>("Success!");
            }
            catch (Exception ex)
            {

                return new Result<string>(ex.Message);
            }
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
