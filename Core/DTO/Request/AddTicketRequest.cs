using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Core.DTO.Request
{
	public class AddTicketRequest
	{
		[Required]
		public int UserId { get; set; }

		[Required]

		public int ProductId { get; set; }

		[Required]

		public int CategoryId { get; set; }

		[Required]

		public int PriorityId { get; set; }

		[Required]
		public string Summary { get; set; } 

		[Required]
		public string Description { get; set; }

        [DataType(DataType.Upload)]
        public List<IFormFile>? Attachments { get; set; }
    }
}
