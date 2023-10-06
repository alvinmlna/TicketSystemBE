using Core.Entities;

namespace Core.Interfaces.Services
{
	public interface IStatusService
	{
		Task<IReadOnlyList<Status>> GetAllAsync();
	}
}
