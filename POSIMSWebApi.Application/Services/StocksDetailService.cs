using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using POSIMSWebApi.Application.Dtos.Stocks;
using POSIMSWebApi.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSIMSWebApi.Application.Services
{
    public class StocksDetailService : IStockDetailService
    {
        private readonly IUnitOfWork _unitOfWork;
        public StocksDetailService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Auto create stocks based on stocks receiving
        /// automatically determines expiration date of products
        /// each stock details is reflected based on the quantity on stocks receiving
        /// </summary>
        /// <param name="input"></param>
        /// <returns>string</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<int> AutoCreateStocks(CreateStocks input, string transNum)
        {
            var productQ = await _unitOfWork.Product.FindAsyncQueryable(e => e.Id == input.ProductId);
            var stock = _unitOfWork.StocksDetail.GetQueryable().Include(e => e.StocksHeaderFk)
                .Where(e => e.StocksHeaderFk.ProductId == input.ProductId);
            var dateToday = DateTime.UtcNow;
            var yearNow = dateToday.Year;
            //starting number
            var stockNum = 0;
            var prevStockNum = await stock.Where(e => e.CreationTime.Year == yearNow).Select(e => new
            {
                e.StockNumInt,
                e.CreationTime
            }).OrderByDescending(e => e.CreationTime).FirstOrDefaultAsync();

            if (prevStockNum is not null)
            {
                stockNum = prevStockNum.StockNumInt;
            }

            var prod = await productQ.Select(e => new { e.ProdCode, e.DaysTillExpiration }).FirstOrDefaultAsync();
            if (prod is null) throw new ArgumentNullException("Error! Product not found.", nameof(prod));
            var daysTillExp = dateToday.AddDays(prod.DaysTillExpiration);
            var stocksCreated = await ListOfStocksToBeSaved(input, stockNum, transNum, daysTillExp);
            await _unitOfWork.StocksDetail.AddRangeAsync(stocksCreated.StockDetails);
            return stocksCreated.HeaderId;
        }

        private async Task<ReturnListOfStocksToBeSaved> ListOfStocksToBeSaved(CreateStocks input, int prevStockNum, string transNum, DateTimeOffset daysTillExp)
        {
            var stocksDetails = new List<StocksDetail>();
            var qty = input.Quantity;
            //CREATE HEADER FOR STOCKS
            var header = new StocksHeader
            {
                ProductId = input.ProductId,
                ExpirationDate = daysTillExp,
            };
            var headerId = await _unitOfWork.StocksHeader.InsertAndGetIdAsync(header);
            var pStockNum = prevStockNum;
            for (int i = 0; i < qty; i++)
            {
                pStockNum++;
                var res = new StocksDetail
                {
                    StockNumInt = pStockNum,
                    StockNum = $"{transNum}-{pStockNum}",
                    StocksHeaderId = headerId,
                };
                stocksDetails.Add(res);
            }

            var result = new ReturnListOfStocksToBeSaved
            {
                HeaderId = headerId,
                StockDetails = stocksDetails
            };

            return result;
        }
    }
}
