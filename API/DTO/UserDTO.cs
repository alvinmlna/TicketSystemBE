using Core.Constants;

namespace API.DTO
{
	public class UserDTO
	{
		public int UserId { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public int RoleId { get; set; }

		public string RoleName { get 
			{
				var result = Enum.GetName(typeof(RoleEnum), RoleId);
				if (string.IsNullOrEmpty(result)) return "Undefined";

				return result;
            }
		}
	}
}
