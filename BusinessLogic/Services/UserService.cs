using Core.Constants;
using Core.DTO.Response;
using Core.Entities;
using Core.Interfaces.Repository;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace BusinessLogic.Services
{
	public class UserService : IUserService
	{
		private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
			_unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IReadOnlyList<User>> GetAllAdminAsync()
		{
			var result = await _unitOfWork.Repository<User>().ListAllAsync();
			return result.Where(x => x.RoleId == (int)RoleEnum.Admin).ToList();
		}

		public async Task<IReadOnlyList<User>> GetAllAsync()
		{
			return await _unitOfWork.Repository<User>().ListAllAsync();
        }

        public async Task<LoginResponse> GetCurrentUser()
        {
            var userClaim = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier);
            if (userClaim == null) return null;

            var user = await _unitOfWork.Repository<User>().GetByIdAsync(int.Parse(userClaim.Value));
            if (user == null) return null;

            return new LoginResponse
            {
                IsSuccess = true,
                Email = user.Email,
                Name = user.Name,
                UserId = user.UserId
            };
        }

        public async Task<User> GetUserById(int id)
        {
            return await _unitOfWork.Repository<User>().GetByIdAsync(id);
        }
    }
}
