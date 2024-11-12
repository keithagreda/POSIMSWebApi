using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class AuditedEntity
    {
        public DateTimeOffset CreationTime { get; set; }
        public int CreatedBy { get; set; }
        public DateTimeOffset ModifiedTime { get; set; }
        public int ModifiedBy { get; set; }
        public bool IsDeleted { get; set; }
        public DateTimeOffset DeletionTime { get; set; }
        public int DeletedBy { get; set; }
    }
}
