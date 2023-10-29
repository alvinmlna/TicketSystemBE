using System.ComponentModel.DataAnnotations;

namespace API.DTO
{
    public class DiscussionDTO
    {
        public int? DiscussionId { get; set; }
        public DateTime? DateSending { get; set; }
        public string Message { get; set; }
        public int UserId { get; set; }
        public string? Name { get; set; }
        public int TicketId { get; set; }


        [DataType(DataType.Upload)]
        public List<IFormFile>? Attachments { get; set; }

        public List<DiscussionAttachmentDTO>? AttachmentViews { get; set; }
    }
}
