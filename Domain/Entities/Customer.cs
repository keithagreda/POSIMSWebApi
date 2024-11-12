using Domain.Enums;

namespace Domain.Entities
{
    public class Customer : AuditedEntity
    {
        public Guid Id { get; set; }
        public string Firstname { get; set; }
        public string Middlename { get; set; }
        public string Lastname { get; set; }
        public CustomerType CustomerType { get; set; }
        //public Guid UserId { get; set; }
    }
}
