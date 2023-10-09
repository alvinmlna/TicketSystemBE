using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Core.DTO.Request
{
	public class EditTicketRequest
	{
		[Required]
		public int TicketId { get; set; }

		[Required]
		public int AssignedToId { get; set; }

		[Required]

		public int ProductId { get; set; }

		[Required]

		public int CategoryId { get; set; }

		[Required]

		public int PriorityId { get; set; }

		[Required]

		public int StatusId { get; set; }
	}
}
