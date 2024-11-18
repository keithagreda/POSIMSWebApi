namespace Domain.Entities
{
    public class Product : AuditedEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ProdCode { get; set; }
        public int DaysTillExpiration { get; set; }
        public ICollection<ProductCategory> ProductCategories { get; set; }
    }
}
