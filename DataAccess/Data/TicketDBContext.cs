
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Data
{
	public class TicketDBContext : DbContext
	{
		public TicketDBContext(DbContextOptions options) : base(options)
		{
		}

		public DbSet<Category> Categories { get; set; }
        public DbSet<Priority> Priorities{ get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<Ticket> Tickets { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<Configuration> Configurations { get; set; }
		public DbSet<Status> Statuses { get; set; }

		public DbSet<Attachment> Attachments { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);
			//optionsBuilder.UseSqlServer("Server=localhost\\sqlexpress;Database=ticketdb;Trusted_Connection=True;TrustServerCertificate=true;");
		}
	}
}
