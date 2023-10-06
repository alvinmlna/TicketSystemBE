using Core.Entities;
using Core.Interfaces.Repository;
using Core.Interfaces.Services;

namespace BusinessLogic.Services
{
	public class PriorityService : IPriorityService
	{
		private readonly IUnitOfWork _unitOfWork;

		public PriorityService(IUnitOfWork unitOfWork)
        {
			_unitOfWork = unitOfWork;
		}

        public async Task<IReadOnlyList<Priority>> GetAllAsync()
		{
			return await _unitOfWork.Repository<Priority>().ListAllAsync();
		}
	}
}
