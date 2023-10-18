using Core.DTO.Request;
using Core.DTO.Response;

namespace Core.Interfaces.Services
{
	public interface IAuthService
	{
		Task<DefaultResponse> Login(AuthRequest request);
		Task<DefaultResponse> Register(RegisterUserRequest request);
	}
}
