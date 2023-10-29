using Core.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace API.DTO
{
    public class DiscussionAttachmentDTO
    {
        public int AttachmentId { get; set; }
        public string Filename { get; set; }
        public string ServerFileName { get; set; }
        public int FileSize { get; set; }
        public DateTime DateAdded { get; set; }
        public int DiscussionId { get; set; }
    }
}
