using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Stocks : AuditedEntity
    {
        public int Id { get; set; }
        public string StockNum { get; set; }
        public DateTimeOffset ExpirationDate { get; set; }
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product ProductFK { get; set; }
    }
}
