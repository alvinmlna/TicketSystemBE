using System.ComponentModel.DataAnnotations;

namespace API.DTO
{
	public class TicketDTO
	{
		[Required]
		public string Summary { get; set; }

		public string Description { get; set; } = string.Empty;

		public string AssignedTo { get; set; } = string.Empty;

		public int UserId { get; set; }
		public int ProductId { get; set; }
		public int CategoryId { get; set; }
		public int PriorityId { get; set; }
		public int StatusId { get; set; }

		public List<IFormFile>? Attachments { get; set; }
	}
}
