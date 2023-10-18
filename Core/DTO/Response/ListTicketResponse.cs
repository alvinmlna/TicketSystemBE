namespace Core.DTO.Response
{
	public class ListTicketResponse
	{
		public int TicketId { get; set; }
		public string TicketIdView { get
			{
				return "T" + TicketId.ToString().PadLeft(5, '0');
			}
		}
		public bool? isExpired
		{
			get
			{
				if (ExpectedDate < DateTime.Now && (Status == "Open" || Status == "New"))
				{
					return true;
				} 
					else
				{
					return false;
				}
			}
		}

		public string Summary { get; set; }
		public string Product { get; set; }

		public string Category { get; set; }
		public string Priority { get; set; }
		public string Status { get; set; }
		public DateTime RaisedDate { get; set; }
		public string RaisedBy { get; set; }
		public DateTime ExpectedDate { get; set; }
	}
}
