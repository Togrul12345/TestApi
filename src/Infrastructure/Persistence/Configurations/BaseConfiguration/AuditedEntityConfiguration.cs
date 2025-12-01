using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations.BaseConfiguration
{
    public class AuditedEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
          where TEntity : class
    {
        public void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.Property<DateTime>("CreatedDate").IsRequired();
            builder.Property<Guid>("CreatedUserId").IsRequired();
            builder.Property<DateTime?>("LastModifiedDate");
            builder.Property<Guid?>("LastModifiedUserId");
            builder.Property<DateTime?>("DeletedDate");
            builder.Property<Guid?>("DeletedUserId");
            builder.Property<bool>("IsDeleted").IsRequired();
        }
    }
}
