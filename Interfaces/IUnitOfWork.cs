using System.Threading.Tasks;

namespace hamitsarmis.activitywebsite.backend.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        IEventRepository EventRepository { get; }
        IMealRepository MealRepository { get; }
    }
}