using Domain.Entities;
using Domain.Interfaces;
using POSIMSWebApi.Application.Dtos.Stocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSIMSWebApi.Application.Services
{
    public class StocksService
    {
        private readonly IUnitOfWork _unitOfWork;
        public StocksService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //public async Task<IList<Stocks>> AutoCreateStocks(CreateStocks input)
        //{

        //}
    }
}
