using DataAccess.Repository;
using TestAPI.Helpers;

namespace TestAPI.Repository
{
	[TestClass]
	public class TicketRepositoryTest
	{
		TicketRepository _ticketRepository = new TicketRepository(UnitOfWorkHelpers.GetActualSqlServerDbContext());

		[TestMethod]
		public void Test()
		{
			var check = _ticketRepository.GetLast12MonthTickets().Result;
		}
	}
}
