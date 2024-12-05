namespace Domain.Entities
{
    public class Remarks : AuditedEntity
    {
        public Guid Id { get; set; }
        public string TransNum { get; set; }
        public string Description { get; set; }
    }

  
}
