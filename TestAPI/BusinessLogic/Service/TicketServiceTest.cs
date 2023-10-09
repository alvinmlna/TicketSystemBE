using BusinessLogic.Services;
using Core.DTO.Request;
using Core.Interfaces.Services;
using TestAPI.Helpers;

namespace TestAPI.BusinessLogic.Service
{
	[TestClass]
	public class TicketServiceTest
	{
		private readonly ITicketServices _ticketServices = new TicketService(UnitOfWorkHelpers.Get(), null);


		[TestMethod]
		public void ListTicketResponse_TEST()
		{
			var result = _ticketServices.ListTicketResponse(new ListTicketRequest
			{
				RaisedBy = 3

			}).Result;

			Assert.IsTrue(true);
		}
    }
}
