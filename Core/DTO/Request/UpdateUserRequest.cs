using System.ComponentModel.DataAnnotations;

namespace Core.DTO.Request
{
    public class UpdateUserRequest
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Role { get; set; }

        public string ImagePath { get; set; } = string.Empty;
    }
}
