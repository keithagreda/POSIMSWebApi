﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class SalesReturn : AuditedEntity
    {
        public Guid Id { get; set; }
        public Guid SalesHeaderId { get; set; }
        [ForeignKey("SalesHeaderId")]
        public SalesHeader SalesHeaderFk { get; set; }
    }
}
