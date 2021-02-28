using System.Threading.Tasks;

namespace api.Interfaces
{
    public interface IUnitOfWork
    {
         IMessageRepository MessageRepository {get; }
         IUserRepository UserRepository {get; }
         ILikesRepository LikesRepository {get; }
         IMastersRepository MastersRepository {get; }
         Task<bool> Complete();
         bool HasChanges();
    }
}