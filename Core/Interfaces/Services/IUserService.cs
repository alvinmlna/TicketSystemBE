using Core.DTO.InternalDTO;
using Core.DTO.Request;
using Core.DTO.Response;
using Core.Entities;

namespace Core.Interfaces.Services
{
	public interface IUserService
    {
        Task<LoginResponse> GetCurrentUser();
        Task<User> GetUserById(int id);
        Task<IReadOnlyList<User>> GetAllAsync();
		Task<IReadOnlyList<User>> GetAllAdminAsync();

        Task<DefaultResponse> Register(RegisterUserRequest request);
        Task<DefaultResponse> UpdateUser(UpdateUserRequest request);
        Task<DefaultResponse> RemoveUser(int userId);
        Task<DefaultResponse> ChangePassword(ChangePasswordRequest changePassword);
    }
}
