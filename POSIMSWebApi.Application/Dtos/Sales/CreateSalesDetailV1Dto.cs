using System.ComponentModel.DataAnnotations;

namespace POSIMSWebApi.Application.Dtos.Sales
{
    public class CreateSalesDetailV1Dto
    {
        [Required]
        public int ProductId { get; set; }
        public decimal Quantity { get; set; }
        public decimal ProductPrice { get; set; }
        public decimal ActualSellingPrice { get; set; }
        public decimal Discount { get; set; }
    }

}
