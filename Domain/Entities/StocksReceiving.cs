using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class StocksReceiving : AuditedEntity
    {
        public Guid Id { get; set; }
        public decimal Quantity { get; set; }
        public string TransNum { get; set; }
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product ProductFK { get; set; }
    }
}
