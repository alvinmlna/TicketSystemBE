
namespace Core.DTO.Request
{
	public class ListTicketRequest
	{
        public string Summary { get; set; }
        public int[] ProductId { get; set; }
        public int[] CategoryId { get; set; }
        public int[] PriorityId { get; set; }
        public int[] StatusId { get; set; }
        public int? RaisedBy { get; set; }
    }
}
