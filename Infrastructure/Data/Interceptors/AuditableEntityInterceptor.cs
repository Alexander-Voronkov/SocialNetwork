using Domain.Common;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Application.Common.Interfaces;

namespace Infrastructure.Data.Interceptors
{
    public class AuditableEntityInterceptor : SaveChangesInterceptor
    {
        private readonly IUser _user;

        public AuditableEntityInterceptor(
            IUser user)
        {
            _user = user;
        }

        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            SoftDelete(eventData.Context);
            UpdateEntities(eventData.Context);

            return base.SavingChanges(eventData, result);
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            SoftDelete(eventData.Context);
            UpdateEntities(eventData.Context);            

            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        public void SoftDelete(DbContext? context)
        {
            if (context == null)
                return;

            foreach (var item in context.ChangeTracker.Entries<ISoftDeletable>())
            {
                if(item.State == EntityState.Deleted)
                {
                    item.Entity.IsDeleted = true;
                    item.State = EntityState.Modified;
                }
            }
        }

        public void UpdateEntities(DbContext? context)
        {
            if (context == null) return;

            foreach (var entry in context.ChangeTracker.Entries<BaseAuditableEntity<int>>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedBy = _user.Id;
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                }

                if (entry.State == EntityState.Added || entry.State == EntityState.Modified || entry.HasChangedOwnedEntities())
                {
                    entry.Entity.LastModifiedBy = _user.Id;
                    entry.Entity.LastModifiedAt = DateTime.UtcNow;
                }
            }
        }
    }

    public static class Extensions
    {
        public static bool HasChangedOwnedEntities(this EntityEntry entry) =>
            entry.References.Any(r =>
                r.TargetEntry != null &&
                r.TargetEntry.Metadata.IsOwned() &&
                (r.TargetEntry.State == EntityState.Added || r.TargetEntry.State == EntityState.Modified));
    }
}
