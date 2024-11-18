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
    public class StocksDetailService : IStocksService
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
        public async Task<string> AutoCreateStocks(CreateStocks input)
        {
            var productQ = await _unitOfWork.Product.FindAsyncQueryable(e => e.Id == input.ProductId);
            var stock = _unitOfWork.StocksDetail.GetQueryable().Include(e => e.StocksHeaderFk)
                .Where(e => e.StocksHeaderFk.ProductId == input.ProductId);
            var dateToday = DateTime.Now;
            var yearNow = dateToday.Year;
            var monthNow = dateToday.Month;
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
            var stocksCreated = await ListOfStocksToBeSaved(input, stockNum, prod.ProdCode, monthNow, yearNow, daysTillExp);
            var result = await _unitOfWork.StocksDetail.AddRangeAsync(stocksCreated);
            _unitOfWork.Complete();
            _unitOfWork.Dispose();
            return result;
        }

        private async Task<List<StocksDetail>> ListOfStocksToBeSaved(CreateStocks input, int prevStockNum, string prodCode, int month, int year, DateTimeOffset daysTillExp)
        {
            var result = new List<StocksDetail>();
            var qty = input.Quantity;
            //CREATE HEADER FOR STOCKS
            var header = new StocksHeader
            {
                ProductId = input.ProductId,
                ExpirationDate = daysTillExp,
            };
            var headerId = await _unitOfWork.StocksHeader.InsertAndGetIdAsync(header);
            for (int i = 0; i < qty; i++)
            {
                var res = new StocksDetail
                {
                    StockNumInt = prevStockNum + 1,
                    StockNum = $"{prodCode}-{month}{year}-{prevStockNum + 1}",
                    StocksHeaderId = headerId,
                };
                result.Add(res);
            }
            return result;
        }
    }
}
