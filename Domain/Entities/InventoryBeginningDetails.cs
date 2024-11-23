using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class InventoryBeginningDetails : AuditedEntity
    {
        public Guid Id { get; set; }
        public decimal Qty { get; set; }
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product ProductFK { get; set; }
        public Guid InventoryBeginningId { get; set; }
        [ForeignKey("InventoryBeginningId")]
        public InventoryBeginning InventoryBeginningFk { get; set; }
    }
}
