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
        public List<CreateSalesDetailDto> CreateSalesDetailDtos { get; set; }
    }

    public class CreateSalesDetailDto
    {
        [Required]
        public TransNumReaderDto TransNumReaderDto { get; set; }
        public decimal ActualSellingPrice { get; set; }
        public decimal ProductPrice { get; set; }
        public decimal Amount { get; set; }
        public decimal Discount { get; set; }
    }

    public class TransDetails
    {
        public string TransNum { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public int StorageLocationId { get; set; }
    }

}
