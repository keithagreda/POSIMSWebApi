namespace Domain.Entities
{
    public class StockDamageHeader : AuditedEntity
    {
        public Guid Id { get; set; }
        public string Remarks { get; set; }
        public string TransNum { get; set; }
        public ICollection<StockDamageDetail> StockDamageDetails { get; set; }
    }
}
