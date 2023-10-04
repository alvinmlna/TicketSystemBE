using DataAccess.Data;
using DataAccess.Repository;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace TestAPI.Helpers
{
	public static class UnitOfWorkHelpers
	{
		public static UnitOfWork Get()
		{
			DbContextOptions options = new DbContextOptionsBuilder<TicketDBContext>()
			.UseSqlServer(new SqlConnection("Server=localhost\\sqlexpress;Database=ticketdb;Trusted_Connection=True;TrustServerCertificate=true;"))
			.Options;

			TicketDBContext dBContext = new TicketDBContext(options);

			return new UnitOfWork(dBContext);
		}
	}
}
