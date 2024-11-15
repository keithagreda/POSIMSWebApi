using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class StocksReceiving : AuditedEntity
    {
        public Guid Id { get; set; }
        public decimal Quantity { get; set; }
        public string TransNum { get; set; }
        public int StocksId { get; set; }
        [ForeignKey("StocksId")]
        public Stocks StockFk { get; set; }
    }
}
