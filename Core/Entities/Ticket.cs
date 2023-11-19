using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
	public class Ticket
	{
        public Ticket()
        {
            Attachments = new HashSet<Attachment>();   
        }
        public int TicketId { get; set; }

        public string Summary { get; set; }

        public string? Description { get; set; }

        public DateTime RaisedDate { get; set; }

        public DateTime ExpectedDate { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

        public DateTime? LockedDate { get; set; }

        public int? LockedUserId { get; set; }

        [ForeignKey(nameof(LockedUserId))]
        public User? LockedUser { get; set; }


        public int? AssignedToId { get; set; }

		[ForeignKey(nameof(AssignedToId))]
		public User AssignedTo { get; set; }

        public int UserId { get; set; }

		[ForeignKey(nameof(UserId))]
		public User User { get; set; }

        public int ProductId { get; set; }
		[ForeignKey(nameof(ProductId))]
		public Product Product { get; set; }

		public int CategoryId { get; set; }

		[ForeignKey(nameof(CategoryId))]
		public Category Category { get; set; }


		public int PriorityId { get; set; }
		[ForeignKey(nameof(PriorityId))]
		public Priority Priority { get; set; }


		public int StatusId { get; set; }
		[ForeignKey(nameof(StatusId))]
		public Status Status { get; set; }


		public virtual ICollection<Attachment> Attachments { get; set; }
	}
}
