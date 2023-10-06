using Core.Constants;
using Core.Entities;
using Core.Interfaces.Repository;
using Core.Interfaces.Services;

namespace BusinessLogic.Services
{
	public class UserService : IUserService
	{
		private readonly IUnitOfWork _unitOfWork;

		public UserService(IUnitOfWork unitOfWork)
        {
			_unitOfWork = unitOfWork;
		}

        public async Task<IReadOnlyList<User>> GetAllAdminAsync()
		{
			var result = await _unitOfWork.Repository<User>().ListAllAsync();
			return result.Where(x => x.RoleId == RoleConstants.Admin).ToList();
		}

		public async Task<IReadOnlyList<User>> GetAllAsync()
		{
			return await _unitOfWork.Repository<User>().ListAllAsync();
		}
	}
}
