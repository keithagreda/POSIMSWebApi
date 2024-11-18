using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class StocksReceiving : AuditedEntity
    {
        public Guid Id { get; set; }
        public decimal Quantity { get; set; }
        public string TransNum { get; set; }
        public int StocksHeaderId { get; set; }
        [ForeignKey("StocksHeaderId")]
        public StocksHeader StocksHeaderFk { get; set; }
    }
}
