using Core.DTO.InternalDTO;
using Core.Entities;

namespace Core.Interfaces.Services
{
	public interface IUserService
    {
        Task<User> GetUserById(int id);
        Task<IReadOnlyList<User>> GetAllAsync();
		Task<IReadOnlyList<User>> GetAllAdminAsync();
	}
}
