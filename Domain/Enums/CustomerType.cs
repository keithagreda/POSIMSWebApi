using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{
    public enum CustomerType
    {
        Normal = 0,
        Distributor = 1,
        Supplier = 2
    }

    public enum InventoryStatus
    {
        Open = 0,
        Closed = 1
    }

    public enum TransactionEnum
    {
        Receiving = 0,
        Return = 1,
        Sales = 2,
        Damage = 3,
    }
}
