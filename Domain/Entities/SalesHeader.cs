using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class SalesHeader : AuditedEntity
    {
        public Guid Id { get; set; }
        public decimal TotalAmount { get; set; }
        public string TransNum { get; set; }
        public Guid? CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public Customer CustomerFk { get; set; }
        public ICollection<SalesDetail> SalesDetails { get; set; }
    }
}
