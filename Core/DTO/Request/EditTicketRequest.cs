using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Core.DTO.Request
{
	public class EditTicketRequest
	{
		public int TicketId { get; set; }
		public int AssignedToId { get; set; }

		public int ProductId { get; set; }

		public int CategoryId { get; set; }

		public int PriorityId { get; set; }

		public int StatusId { get; set; }
	}
}
