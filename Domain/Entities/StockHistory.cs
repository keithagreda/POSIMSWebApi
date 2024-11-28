using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class StockHistory : AuditedEntity
    {
        public Guid Id { get; set; }
        public string TransNum { get; set; }
        public Guid StockDetailId { get; set; }
        [ForeignKey("StockDetailId")]
        public StocksDetail StockDetailFk { get; set; }
    }
}
