using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class StocksDetail : AuditedEntity
    {
        public int Id { get; set; }
        public string StockNum { get; set; }
        public int StockNumInt { get; set; }
        public int StocksHeaderId { get; set; }
        [ForeignKey("StocksHeaderId")]
        public StocksHeader StocksHeaderFk { get; set; }
    }

}
