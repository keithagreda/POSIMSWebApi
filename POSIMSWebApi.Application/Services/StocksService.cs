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
    public class StocksService : IStocksService
    {
        private readonly IUnitOfWork _unitOfWork;
        public StocksService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<string> AutoCreateStocks(CreateStocks input)
        {
            var productQ = await _unitOfWork.Product.FindAsyncQueryable(e => e.Id == input.ProductId);
            var stock = await _unitOfWork.Stocks.FindAsyncQueryable(e => e.ProductId == input.ProductId);
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
            var stocksCreated = ListOfStocksToBeSaved(input, stockNum, prod.ProdCode, monthNow, yearNow, daysTillExp);
            var result = await _unitOfWork.Stocks.AddRangeAsync(stocksCreated);
            _unitOfWork.Complete();
            _unitOfWork.Dispose();
            return result;
        }

        private List<Stocks> ListOfStocksToBeSaved(CreateStocks input, int prevStockNum, string prodCode, int month, int year, DateTimeOffset daysTillExp)
        {
            var result = new List<Stocks>();
            var qty = input.Quantity;
            for (int i = 0; i < qty; i++)
            {
                var res = new Stocks
                {
                    StockNumInt = prevStockNum + 1,
                    StockNum = $"{prodCode}-{month}{year}-{prevStockNum + 1}",
                    ProductId = input.ProductId,
                    ExpirationDate = daysTillExp,
                };
                result.Add(res);
            }
            return result;
        }
    }
}
