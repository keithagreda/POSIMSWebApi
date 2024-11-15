using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class StockDamageHeader : AuditedEntity
    {
        public Guid Id { get; set; }
        public string Remarks { get; set; }
        public Guid StocksId { get; set; }
        [ForeignKey("StocksId")]
        public Stocks StocksFk { get; set; }
        public ICollection<StockDamageDetail> StockDamageDetails { get; set; }
    }
}
