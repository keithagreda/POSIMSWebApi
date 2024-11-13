using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class ProductCategory : AuditedEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Product>? Products { get; set; }
    }
}
