using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class SalesStockDetail : AuditedEntity
    {
        public Guid Id { get; set; }
        public Guid SalesDetailId { get; set; }
        [ForeignKey("SalesDetailId")]
        public SalesDetail SalesDetailFk { get; set; }
        public Guid StockDetailId { get; set; }
        [ForeignKey("StockDetailId")]
        public StocksDetail StockDetailFk { get; set; }
    }
}
