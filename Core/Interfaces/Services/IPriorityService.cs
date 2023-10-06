
using Core.Entities;

namespace Core.Interfaces.Services
{
	public interface IPriorityService
	{
		Task<IReadOnlyList<Priority>> GetAllAsync();
	}
}
