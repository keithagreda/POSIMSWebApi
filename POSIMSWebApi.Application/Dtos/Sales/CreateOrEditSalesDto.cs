using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSIMSWebApi.Application.Dtos.Sales
{
    public class CreateOrEditSalesDto
    {
        public Guid? SalesHeaderId { get; set; }
        public Guid? CustomerId { get; set; }
        [Required]
        public int ProductId { get; set; }
        public decimal Qty { get; set; }
        public decimal ActualSellingPrice { get; set; }
        public decimal Amount { get; set; }
        public decimal Discount { get; set; }
        public string TransNum { get; set; }
    }
}
