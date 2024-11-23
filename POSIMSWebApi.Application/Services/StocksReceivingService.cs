using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using POSIMSWebApi.Application.Dtos.Stocks;
using POSIMSWebApi.Application.Dtos.StocksReceiving;
using POSIMSWebApi.Application.Interfaces;
using System;
using System.Collections.Generic;
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

        public async Task<string> ReceiveStocks(CreateStocksReceivingDto input)
        {
            //have to create stocks first
            var createStocks = new CreateStocks
            {
                ProductId = input.ProductId,
                Quantity = input.Quantity,
            };
            //generate transnum based on product id and current products received this day
            var transNum = TransNumGenerator(input.ProductId);
            //get stocks header id for receiving
            var stocksHeaderId = await _stockDetailService.AutoCreateStocks(createStocks, transNum);
            //get currently opened inventory for tagging 
            var currentlyOpenedInv = await _unitOfWork.InventoryBeginning.CreateOrGetInventoryBeginning();

            var stocksReceiving = new StocksReceiving
            {
                StocksHeaderId = stocksHeaderId,
                TransNum = transNum,
                Quantity = input.Quantity,
                InventoryBeginningId = currentlyOpenedInv,
            };

            await _unitOfWork.StocksReceiving.AddAsync(stocksReceiving);
            return "Success!";
        }

        private string TransNumGenerator(int productId)
        {
            var prodCode =  _unitOfWork.Product.GetQueryable().Where(e => e.Id == productId).Select(e => e.ProdCode).FirstOrDefault();
            var stockReceiving = _unitOfWork.StocksReceiving.GetQueryable();
            var currentTransCount = stockReceiving.Count() + 1;
            string datePart = DateTime.Now.ToString("yyMMdd");
            return $"{prodCode}-{datePart}-{currentTransCount}";
        }
        //public async Task<string> ReceiveStocks()
        //{
        //    var data = _unitOfWork.StocksReceiving.GetQueryable()
        //}

        //public async Task 
    }
}
