using Domain.Entities.UserEntity;
using System.Linq.Expressions;

namespace API.Services.SenderReceiverServices
{
    public interface ISenderReceiverService
    {
        Task Create(SenderReceiver senderReceiver);
        Task<List<SenderReceiver>> GetAllBy(Expression<Func<SenderReceiver,bool>> exp);
        Task SaveChanges();
    }
}
