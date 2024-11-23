using Domain.Enums;

namespace Domain.Entities
{
    public class InventoryBeginning : AuditedEntity
    {
        public Guid Id { get; set; }
        public InventoryStatus Status { get; set; }
        public DateTimeOffset? TimeClosed { get; set; }
    }
}
