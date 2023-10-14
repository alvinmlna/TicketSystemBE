using BusinessLogic.Services;
using Core.Constants;
using Core.Entities;
using Core.Interfaces.Services;
using DataAccess.Repository;
using TestAPI.Helpers;

namespace TestAPI.BusinessLogic.Service
{
	[TestClass]
	public class ShowStatusSummaryTest
	{
		static UnitOfWork _unitOfWork = UnitOfWorkHelpers.GetInMemories();
		private readonly ITicketServices _ticketServices = new TicketService(_unitOfWork, null);

		[TestInitialize]
		public void Initialize()
		{
			UnitOfWorkHelpers.InitializeData();
			UnitOfWorkHelpers.InitializeTicketData();
		}

		[TestMethod]
		public void SHOW_STATUS_SUMMARY()
		{
			var ticketSummary = _ticketServices.GetStatusSummaryResponses().Result;

			var countOfNew = _unitOfWork.TicketRepository.ListTicket(null).Result.Where(x => x.Status.StatusGroupId == StatusGroupContants.NEW).Count();
			var countOfOpen = _unitOfWork.TicketRepository.ListTicket(null).Result.Where(x => x.Status.StatusGroupId == StatusGroupContants.OPEN).Count();
			var countOfExpired = _unitOfWork.TicketRepository.ListTicket(null).Result.Where(x => x.Status.StatusGroupId == StatusGroupContants.OPEN && x.ExpectedDate < DateTime.Now).Count();


			Assert.AreEqual(countOfNew, ticketSummary.FirstOrDefault(x => x.Status == "New")?.Count);
			Assert.AreEqual(countOfOpen, ticketSummary.FirstOrDefault(x => x.Status == "Open")?.Count);
			Assert.AreEqual(countOfExpired, ticketSummary.FirstOrDefault(x => x.Status == "Expired")?.Count);
		
		}
	}
}
