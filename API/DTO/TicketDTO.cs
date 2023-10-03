using System.ComponentModel.DataAnnotations;

namespace API.DTO
{
	public class TicketDTO
	{
		[Required]
		public string Summary { get; set; }

		[Required]
		public string Status { get; set; }

		public string AssignedTo { get; set; }

		public int UserId { get; set; }
		public int ProductId { get; set; }
		public int CategoryId { get; set; }
		public int PriorityId { get; set; }
	}
}
