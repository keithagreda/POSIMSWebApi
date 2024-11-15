using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSIMSWebApi.Application.Services
{
    public class StocksReceivingService
    {
        private readonly IUnitOfWork _unitOfWork;
        public StocksReceivingService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //public async Task<string> ReceiveStocks()
        //{
        //    var data = _unitOfWork.StocksReceiving.GetQueryable()
        //}

        //public async Task 
    }
}
