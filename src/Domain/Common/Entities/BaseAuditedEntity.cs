namespace Domain.Common.Entities
{
    public abstract class BaseAuditedEntity<TKey> : BaseEntity<TKey> ,IHasCreationTime, IHasModificationTime, IHasDeletionTime,
    ICreationAudited, IModificationAudited, IDeletionAudited, ISoftDelete
    {
        public DateTime? DeletedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public Guid? LastModifiedUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid CreatedUserId { get; set; }
        public Guid? DeletedUserId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
