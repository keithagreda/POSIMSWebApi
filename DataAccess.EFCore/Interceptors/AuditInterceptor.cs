using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace POSIMSWebApi.Interceptors
{
    public class AuditInterceptor : SaveChangesInterceptor
    {
        public override InterceptionResult<int> SavingChanges(
            DbContextEventData eventData,
            InterceptionResult<int> result
        )
        {
            if (eventData.Context == null) return result;
            foreach(var entry in eventData.Context.ChangeTracker.Entries())
            {
                if(entry.Entity is AuditedEntity auditableEntity)
                {
                    if (entry.State == EntityState.Added)
                    {
                        auditableEntity.CreationTime = DateTimeOffset.UtcNow;
                        //TODO: change it once auth is implemented
                        auditableEntity.CreatedBy = 1;
                    }
                    if (entry.State == EntityState.Modified)
                    {
                        auditableEntity.ModifiedBy = 1;
                        auditableEntity.ModifiedTime = DateTimeOffset.UtcNow;
                    }
                }
            }
            return base.SavingChanges(eventData, result);
        }
    }
}
