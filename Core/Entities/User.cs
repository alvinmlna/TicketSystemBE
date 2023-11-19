
using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
	public class User
	{
		public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int RoleId { get; set; }
        public string ImagePath { get; set; }
        public byte[] PasswordHash { get; set; }
		public byte[] PasswordSalt { get; set; }
        public bool IsRemoved { get; set; }

        public Ticket? LockedTicket { get; set; }
    }
}
