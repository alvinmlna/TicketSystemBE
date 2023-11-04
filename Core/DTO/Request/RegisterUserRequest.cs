using System.ComponentModel.DataAnnotations;

namespace Core.DTO.Request
{
	public class RegisterUserRequest
	{
		[Required]
		public string Email { get; set; }

		public string Name { get; set; }

		[Required]
		public string Password { get; set; }

		[Required]
		public int Role { get; set; }

        public string ImagePath { get; set; } = string.Empty;
    }
}
