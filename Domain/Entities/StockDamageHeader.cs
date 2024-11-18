using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class StockDamageHeader : AuditedEntity
    {
        public Guid Id { get; set; }
        public string Remarks { get; set; }
        public int Quantity { get; set; }
        public ICollection<StockDamageDetail> StockDamageDetails { get; set; }
    }
}
