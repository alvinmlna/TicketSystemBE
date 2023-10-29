
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

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
		public DbSet<Discussion> Discussions { get; set; }
		public DbSet<DiscussionAttachment> DiscussionAttachments { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);
			//optionsBuilder.UseSqlServer("Server=localhost\\sqlexpress;Database=ticketdb;Trusted_Connection=True;TrustServerCertificate=true;");
		}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Discussion>()
				.HasOne(m => m.User)
				.WithMany()
				.HasForeignKey(x => x.UserId)
				.OnDelete(DeleteBehavior.NoAction);
        }
    }
}
