
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
	public class Attachment
	{
        public int AttachmentId { get; set; }
        public string Filename { get; set; }
        public string ServerFileName { get; set; }
        public int FileSize { get; set; }
        public DateTime DateAdded { get; set; }

		[ForeignKey(nameof(Ticket))]
		public int TicketId { get; set; }
		public Ticket Ticket { get; set; }
	}
}
