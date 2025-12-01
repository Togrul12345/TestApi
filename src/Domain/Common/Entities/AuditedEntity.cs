namespace Domain.Common.Entities
{
    public interface IAudited
    {
    }
    public interface IHasCreationTime : IAudited
    {
        DateTime CreatedDate { get; set; }
    }
    public interface ICreationAudited : IHasCreationTime
    {
        public Guid CreatedUserId { get; set; }
    }
    public interface IHasModificationTime : IAudited
    {
        public DateTime? LastModifiedDate { get; set; }
    }
    public interface IModificationAudited : IHasModificationTime
    {
        public Guid? LastModifiedUserId { get; set; }
    }
    public interface IHasDeletionTime : IAudited
    {
        DateTime? DeletedDate { get; set; }
    }
    public interface IDeletionAudited : IHasDeletionTime
    {
        Guid? DeletedUserId { get; set; }
    }
}
