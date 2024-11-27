﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class SalesDetail : AuditedEntity
    {
        public Guid Id { get; set; }
        public decimal Quantity { get; set; }
        public decimal ActualSellingPrice { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal Discount { get; set; }
        public string TransNum { get; set; }
        public Guid SalesHeaderId { get; set; }
        [ForeignKey("SalesHeaderId")]
        public SalesHeader SalesHeaderFk { get; set; }
        
    }
}
