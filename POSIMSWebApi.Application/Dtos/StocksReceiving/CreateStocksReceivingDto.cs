using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSIMSWebApi.Application.Dtos.StocksReceiving
{
    public class CreateStocksReceivingDto
    {
        [Required]
        public decimal Quantity { get; set; }
        [Required]
        public int ProductId { get; set; }
    }

    public class GetAllStocksReceivingDto
    {

    }
}
