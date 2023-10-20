namespace API.DTO
{
    public class DiscussionDTO
    {
        public int DiscussionId { get; set; }
        public DateTime DateSending { get; set; }
        public string Message { get; set; }
        public int UserId { get; set; }
        public int TicketId { get; set; }
    }
}
