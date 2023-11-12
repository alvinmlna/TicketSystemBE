using System.ComponentModel.DataAnnotations;

namespace Core.DTO.Request
{
	public class RegisterUserRequest
	{
		public int? UserId { get; set; }

        [Required]
		public string Email { get; set; }

		public string Name { get; set; }

		[Required]
		public string Password { get; set; }

		[Required]
		public int RoleId { get; set; }
    }
}
