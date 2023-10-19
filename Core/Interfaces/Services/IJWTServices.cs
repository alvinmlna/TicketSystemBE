using Core.Entities;

namespace Core.Interfaces.Services
{
	public interface IJWTServices
	{
		string CreateToken(User user);
    }
}
