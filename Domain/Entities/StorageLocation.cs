namespace Domain.Entities
{
    public class StorageLocation : AuditedEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

  
}
