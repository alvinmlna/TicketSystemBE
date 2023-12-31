﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Mail;

namespace Core.Entities
{
    public class Discussion
    {
        public Discussion()
        {
            Attachments = new HashSet<DiscussionAttachment>();
        }

        [Key]
        public int DiscussionId { get; set; }
        public DateTime DateSending { get; set; }
        public string Message { get; set; }

        public int UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public User User { get; set; }

        [ForeignKey(nameof(Ticket))]
        public int TicketId { get; set; }
        public Ticket Ticket { get; set; }

        public virtual ICollection<DiscussionAttachment> Attachments { get; set; }
    }
}
