using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
	public class Ticket
	{
        public string TicketId { get; set; }

        public string Summary { get; set; }

        public string Status { get; set; }
        public DateTime RaisedDate { get; set; }

        public DateTime ExpectedDate { get; set; }
        public string AssignedTo { get; set; }


        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        public User User { get; set; }

        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }
        public Product Product { get; set; }

		[ForeignKey(nameof(Category))]
		public int CategoryId { get; set; }
		public Category Category { get; set; }


		[ForeignKey(nameof(Priority))]
		public int PriorityId { get; set; }
		public Priority Priority { get; set; }
    }
}
