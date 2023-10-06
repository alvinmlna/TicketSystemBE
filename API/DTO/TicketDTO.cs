using System.ComponentModel.DataAnnotations;

namespace API.DTO
{
	public class TicketDTO
	{
        public int TicketId { get; set; }
        public string TicketIdView { get; set; } = string.Empty;

		[Required]
		public string Summary { get; set; }

		public string Description { get; set; } = string.Empty;

		public DateTime RaisedDate { get; set; }
		public DateTime ExpectedDate { get; set; }


		public int? AssignedToId { get; set; }
		public string AssignedTo { get; set; } = string.Empty;

		public int UserId { get; set; }
		public string RaisedBy { get; set; } = string.Empty;

		public int ProductId { get; set; }
        public ProductDTO? Product { get; set; }

		public int CategoryId { get; set; }
		public CategoryDTO? Category { get; set; }

		public int PriorityId { get; set; }
		public PriorityDTO? Priority { get; set; }

		public int StatusId { get; set; }
		public StatusDTO? Status { get; set; }


		[DataType(DataType.Upload)]
		public List<IFormFile>? Attachments { get; set; }

		public List<AttachmentDTO>? AttachmentViews { get; set; }
	}
}
