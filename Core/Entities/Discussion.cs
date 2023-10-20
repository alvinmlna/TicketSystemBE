using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class Discussion
    {
        [Key]
        public int DiscussionId { get; set; }
        public DateTime DateSending { get; set; }
        public string Message { get; set; }


        public int UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }


        [ForeignKey(nameof(Ticket))]
        public int TicketId { get; set; }
        public Discussion Ticket { get; set; }
    }
}
