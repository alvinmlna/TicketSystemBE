using API.Helpers.Validations;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;

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
        public ProductDTO Product { get; set; }

		public int CategoryId { get; set; }
		public CategoryDTO Category { get; set; }

		public int PriorityId { get; set; }
		public PriorityDTO Priority { get; set; }

		public int StatusId { get; set; }
		public StatusDTO Status { get; set; }


		[DataType(DataType.Upload)]
		public List<IFormFile>? Attachments { get; set; }

		public List<AttachmentDTO> AttachmentViews { get; set; }
	}
}
