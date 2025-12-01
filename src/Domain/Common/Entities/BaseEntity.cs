using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Common.Entities
{
    public abstract class BaseEntity<T> : IEntity
    {
        public T Id { get; set; }
    }
}
