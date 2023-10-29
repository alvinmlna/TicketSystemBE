namespace API.DTO
{
	public class AttachmentDTO
	{
		public int AttachmentId { get; set; }
		public string Filename { get; set; }
		public DateTime DateAdded { get; set; }
		public int TicketId { get; set; }

	}
}
