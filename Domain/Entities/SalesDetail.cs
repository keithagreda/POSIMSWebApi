using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class SalesDetail : AuditedEntity
    {
        public Guid Id { get; set; }
        public decimal Quantity { get; set; }
        //can be used to override calculated price
        public decimal ActualSellingPrice { get; set; }
        //to log the current selling price of a product
        public decimal ProductPrice { get; set; }
        //total amount selling price based on system pricing
        public decimal Amount { get; set; }
        //calculation when theres a difference between actual selling price and amount
        public decimal Discount { get; set; }
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product ProductFk { get; set; }
        public Guid SalesHeaderId { get; set; }
        [ForeignKey("SalesHeaderId")]
        public SalesHeader SalesHeaderFk { get; set; }
    }
}
