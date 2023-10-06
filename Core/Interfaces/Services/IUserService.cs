using Core.Entities;

namespace Core.Interfaces.Services
{
	public interface IUserService
	{
		Task<IReadOnlyList<User>> GetAllAsync();
		Task<IReadOnlyList<User>> GetAllAdminAsync();
	}
}
