using System.ComponentModel.DataAnnotations;

namespace POSIMSWebApi.Application.Dtos.Sales
{
    public class CreateSalesDetailDto
    {
        [Required]
        public TransNumReaderDto TransNumReaderDto { get; set; }
        public decimal ActualSellingPrice { get; set; }
        public decimal ProductPrice { get; set; }
        public decimal Amount { get; set; }
        public decimal Discount { get; set; }
    }

}
