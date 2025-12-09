using Domain.Common.Entities;
using Domain.Common.Utility;
using Domain.Entities.ChatEntity;
using Domain.Entities.UserEntity;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Linq.Expressions;
using System.Reflection;
namespace Infrastructure.Contexts;
public class TestDbContext : BaseDbContext
{
    private readonly IMediator _mediator;
    public TestDbContext(DbContextOptions<TestDbContext> options, IMediator mediator) : base(options) { _mediator = mediator; }
  

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
        modelBuilder.Entity<ChatUser>()
       .HasKey(cu => new { cu.ChatId, cu.ParticipantId });

        modelBuilder.Entity<ChatUser>()
            .HasOne(cu => cu.Chat)
            .WithMany(c => c.ChatUsers)
            .HasForeignKey(cu => cu.ChatId);

        modelBuilder.Entity<ChatUser>()
            .HasOne(cu => cu.Participant)
            .WithMany(c=>c.ChatUsers) // 🔥 User chat-i bilmir
            .HasForeignKey(cu => cu.ParticipantId);
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
    public DbSet<AppUser> AppUsers { get; set; }
    public DbSet<AppRole> AppRoles { get; set; }
    public DbSet<RoleUser> RoleUsers { get; set; }
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