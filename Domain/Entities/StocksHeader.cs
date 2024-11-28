using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class StocksHeader : AuditedEntity
    {
        public int Id { get; set; }
        public DateTimeOffset ExpirationDate { get; set; }
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product ProductFK { get; set; }

        public int StorageLocationId { get; set; }
        [ForeignKey("StorageLocationId")]
        public StorageLocation StorageLocationFk { get; set; }
        public ICollection<StocksDetail> StocksDetails { get; set; } = new List<StocksDetail>();
    }
}
