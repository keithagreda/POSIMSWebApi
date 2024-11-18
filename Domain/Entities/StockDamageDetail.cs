using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class StockDamageDetail : AuditedEntity
    {
        public Guid Id { get; set; }
        public Guid StockDamageHeaderId { get; set; }
        [ForeignKey("StockDamageHeaderId")]
        public StockDamageHeader StockDamageHeaderFk { get; set; }
        public int StockDetailId { get; set; }
        [ForeignKey("StockDetailId")]
        public StocksDetail StockDetailFk { get; set; }
    }
}
