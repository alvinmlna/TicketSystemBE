using Core.Entities;

namespace Core.Interfaces.Repository
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> GetUserById(int id);
        Task<IReadOnlyList<User>> ListAllUsers(string search = "");
    }
}
