using Domain.Common.Entities;

namespace API.Models
{
    public class ApiResponse<T>
    {
        public bool IsSuccess { get; set; }

        public Domain.Common.Entities.DomainSuccess<T> Success { get; set; }

        public Domain.Common.Entities.DomainError Error { get; set; }
    }
}
