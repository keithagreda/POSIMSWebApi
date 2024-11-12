using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class StockDamageDetail : AuditedEntity
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
        public Guid StockDamageHeaderId { get; set; }
        [ForeignKey("StockDamageHeaderId")]
        public StockDamageHeader StockDamageHeaderFk { get; set; }
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product ProductFK { get; set; }
    }
}
