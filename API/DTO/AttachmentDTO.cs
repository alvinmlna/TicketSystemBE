namespace API.DTO
{
	public class AttachmentDTO
	{
		public int AttachmentId { get; set; }
		public string Filename { get; set; }
        public string ServerFileName { get; set; }
        public int FileSize { get; set; }
        public DateTime DateAdded { get; set; }
		public int TicketId { get; set; }

	}
}
