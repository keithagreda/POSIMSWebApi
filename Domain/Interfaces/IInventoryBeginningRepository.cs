﻿using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IInventoryBeginningRepository : IGenericRepository<InventoryBeginning>
    {
        Task<Guid> CreateOrGetInventoryBeginning();
    }
}