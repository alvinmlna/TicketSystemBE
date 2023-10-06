using Core.Entities;
using Core.Interfaces.Repository;
using Core.Interfaces.Services;

namespace BusinessLogic.Services
{
	public class StatusService : IStatusService
	{
		private readonly IUnitOfWork _unitOfWork;

		public StatusService(IUnitOfWork unitOfWork)
        {
			_unitOfWork = unitOfWork;
		}


		public async Task<IReadOnlyList<Status>> GetAllAsync()
		{
			return await _unitOfWork.Repository<Status>().ListAllAsync();
		}
	}
}
