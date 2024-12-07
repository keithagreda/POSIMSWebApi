using Domain.Entities;
using Domain.Error;
using Domain.Interfaces;
using LanguageExt.Common;
using Microsoft.EntityFrameworkCore;
using POSIMSWebApi.Application.Dtos.Stocks;
using POSIMSWebApi.Application.Dtos.StocksReceiving;
using POSIMSWebApi.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSIMSWebApi.Application.Services
{
    public class StocksReceivingService : IStockReceivingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStockDetailService _stockDetailService;
        public StocksReceivingService(IUnitOfWork unitOfWork,
            IStockDetailService stockDetailService)
        {
            _unitOfWork = unitOfWork;
            _stockDetailService = stockDetailService;
        }

        public async Task<Result<string>> ReceiveStocks(CreateStocksReceivingDto input)
        {
            //have to create stocks first
            var createStocks = new CreateStocks
            {
                ProductId = input.ProductId,
                Quantity = input.Quantity,
                StorageLocationId = input.StorageLocationId
            };
            //generate transnum based on product id and current products received this day
            var transNum = TransNumGenerator(input.ProductId, input.StorageLocationId);
            //get stocks header id for receiving
            var stocksHeaderIdResult = await _stockDetailService.AutoCreateStocks(createStocks, transNum);

            // Handle potential error in stocksHeaderIdResult
            return await stocksHeaderIdResult.Match(
                async stocksHeaderId =>
                {
                    // Step 4: Get currently opened inventory for tagging
                    var currentlyOpenedInv = await _unitOfWork.InventoryBeginning.CreateOrGetInventoryBeginning();

                    // Step 5: Prepare the StocksReceiving entity
                    var stocksReceiving = new StocksReceiving
                    {
                        StocksHeaderId = stocksHeaderId,
                        TransNum = transNum,
                        Quantity = input.Quantity,
                        InventoryBeginningId = currentlyOpenedInv
                    };

                    // Step 6: Save to the database
                    await _unitOfWork.StocksReceiving.AddAsync(stocksReceiving);
                    return new Result<string>("Success!");
                },
                error =>
                {
                    // Handle the fault case
                    return Task.FromResult(new Result<string>(error));
                }
            );
        }


        private string TransNumGenerator(int productId, int storageId)
        {
            var dateNow = DateTime.Now.Date;
            var prodCode =  _unitOfWork.Product.GetQueryable().Where(e => e.Id == productId && e.CreationTime.Date == dateNow).Select(e => e.ProdCode ).FirstOrDefault();
            var stockReceiving = _unitOfWork.StocksReceiving.GetQueryable();
            var currentTransCount = stockReceiving.Count() + 1;
            string datePart = DateTime.Now.ToString("yyMMdd");
            return $"{prodCode}-{datePart}-{currentTransCount}-{storageId}";
        }
        //public async Task<string> ReceiveStocks()
        //{
        //    var data = _unitOfWork.StocksReceiving.GetQueryable()
        //}

        //public async Task 
    }
}
