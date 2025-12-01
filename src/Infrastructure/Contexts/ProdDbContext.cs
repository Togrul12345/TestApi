using Domain.Common.Entities;
using Domain.Common.Utility;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Linq.Expressions;
using System.Reflection;
using MediatR;
namespace Infrastructure.Contexts;
public class ProdDbContext : BaseDbContext
{
    private readonly IMediator _mediator;

    public ProdDbContext(DbContextOptions<ProdDbContext> options, IMediator mediator) : base(options) { _mediator = mediator; }

    #region SaveChangeMethods
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entry in ChangeTracker.Entries<IAudited>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    SetCreationAuditProperties(entry);
                    break;
                case EntityState.Modified:
                    SetModificationAuditProperties(entry);
                    break;
                case EntityState.Deleted:
                    CancelDeletionForSoftDelete(entry);
                    SetDeletionAuditProperties(entry);
                    break;
            }
        }
        int result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        // ignore events if no dispatcher provided
        if (_mediator == null) return result;

        return result;
    }
    #endregion

    #region OnModelCreating
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("", builder =>
            {
                builder.EnableRetryOnFailure(1, TimeSpan.FromSeconds(10), null);
            });
        }

        base.OnConfiguring(optionsBuilder);
    }

    #endregion

    #region Configure Global Filters
    protected void ConfigureGlobalFilters<TEntity>(ModelBuilder modelBuilder, IMutableEntityType entityType) where TEntity : class
    {
        if (ShouldFilterEntity<TEntity>(entityType))
        {
            var filterExpression = CreateFilterExpression<TEntity>();
            if (filterExpression != null)
                modelBuilder.Entity<TEntity>().HasQueryFilter(filterExpression);
        }
    }

    protected virtual bool ShouldFilterEntity<TEntity>(IMutableEntityType entityType) where TEntity : class
    {
        if (typeof(ISoftDelete).IsAssignableFrom(typeof(TEntity)))
        {
            return true;
        }

        return false;
    }

    protected virtual Expression<Func<TEntity, bool>> CreateFilterExpression<TEntity>() where TEntity : class
    {
        Expression<Func<TEntity, bool>> expression = null!;

        if (typeof(ISoftDelete).IsAssignableFrom(typeof(TEntity)))
        {
            Expression<Func<TEntity, bool>> softDeleteFilter = e => !((ISoftDelete)e).IsDeleted;
            expression = softDeleteFilter;
        }
        return expression!;
    }
    #endregion

    #region Configure Audit Properties
    protected virtual void SetCreationAuditProperties(EntityEntry entry)
    {
        if (!(entry.Entity is IHasCreationTime hasCreationTimeEntity)) return;

        if (hasCreationTimeEntity.CreatedDate == default)
            hasCreationTimeEntity.CreatedDate = Utilities.GetDateTimeNowByEnvironment();

        if (!(entry.Entity is ICreationAudited creationAuditedEntity)) return;
        creationAuditedEntity.CreatedUserId = Utilities.GetCurrentUserId();
    }

    protected virtual void SetModificationAuditProperties(EntityEntry entry)
    {
        if (entry.State != EntityState.Modified) return;

        if (entry.Entity is IHasModificationTime hasModificationTimeEntity)
        {
            if (hasModificationTimeEntity.LastModifiedDate == default)
                hasModificationTimeEntity.LastModifiedDate = Utilities.GetDateTimeNowByEnvironment();
        }

        if (entry.Entity is IModificationAudited modificationAuditedEntity)
            modificationAuditedEntity.LastModifiedUserId = Utilities.GetCurrentUserId();
    }

    protected virtual void SetDeletionAuditProperties(EntityEntry entry)
    {
        if (entry.Entity is IDeletionAudited deletionAuditedEntity)
        {
            deletionAuditedEntity.DeletedDate = Utilities.GetDateTimeNowByEnvironment();
            deletionAuditedEntity.DeletedUserId = Utilities.GetCurrentUserId();
        }

        // Soft-delete üçün
        if (entry.Entity is ISoftDelete softDeleteEntity)
        {
            softDeleteEntity.IsDeleted = true;
        }

        // Entry state-ini `Modified`-ə dəyişdirmədən əvvəl, açıq-aşkar bazaya yazmağı təmin et
        entry.Property(nameof(IDeletionAudited.DeletedDate)).IsModified = true;
        entry.Property(nameof(IDeletionAudited.DeletedUserId)).IsModified = true;
        entry.Property(nameof(ISoftDelete.IsDeleted)).IsModified = true;

        // Entity fizik olaraq silinməsin
        entry.State = EntityState.Modified;

    }

    protected virtual void CancelDeletionForSoftDelete(EntityEntry entry)
    {
        if (!(entry.Entity is ISoftDelete))
            return;

        entry.Reload();
        entry.State = EntityState.Modified;
        ((ISoftDelete)entry.Entity).IsDeleted = true;
    }
    #endregion
}
public class ProdDbContextFactory : IDesignTimeDbContextFactory<ProdDbContext>
{
    public ProdDbContext CreateDbContext(string[] args)
    {
        var options = new DbContextOptionsBuilder<ProdDbContext>()
            .UseSqlServer("")
            .Options;

        return new ProdDbContext(options, mediator: null);
    }
}