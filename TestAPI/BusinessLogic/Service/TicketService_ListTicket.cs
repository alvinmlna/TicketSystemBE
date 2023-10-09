using BusinessLogic.Services;
using Core.DTO.Request;
using Core.Entities;
using Core.Interfaces.Repository;
using Core.Interfaces.Services;
using DataAccess.Repository;
using Newtonsoft.Json.Linq;
using TestAPI.Helpers;

namespace TestAPI.BusinessLogic.Service
{
	[TestClass]
	public class TicketService_ListTicket
	{
		static UnitOfWork _unitOfWork = UnitOfWorkHelpers.GetInMemories();
		private readonly ITicketServices _ticketServices = new TicketService(_unitOfWork, null);

		[TestInitialize]
		public void Initialize()
		{
			UnitOfWorkHelpers.InitializeData();
			UnitOfWorkHelpers.InitializeTicketData();
		}

		//No Filter
		[TestMethod]
		public void LIST_TICKET_NO_FILTER()
		{
			var result = _ticketServices.ListTicketResponse(new ListTicketRequest
			{

			}).Result;

			Assert.AreEqual(3, result.Count);
		}

		//Filter by summary only
		[TestMethod]
		public void LIST_TICKET_BY_SUMMARY_1()
		{
			var result = _ticketServices.ListTicketResponse(new ListTicketRequest
			{
				Summary = "two"
			}).Result;

			Assert.AreEqual(1, result.Count);
		}


		//Filter by summary only
		[TestMethod]
		public void LIST_TICKET_BY_SUMMARY_DIFFERENT_CHARACTER_CASE()
		{
			var result = _ticketServices.ListTicketResponse(new ListTicketRequest
			{
				Summary = "TWO"
			}).Result;

			Assert.AreEqual(1, result.Count);
		}

		//Filer by category 1 & 2
		[TestMethod]
		public void LIST_TICKET_BY_CATEGORY()
		{
			var result = _ticketServices.ListTicketResponse(new ListTicketRequest
			{
				CategoryId = new int[] { 1, 2 }
			}).Result;

			Assert.AreEqual(2, result.Count);
		}

		//Filer by prioriy id 1 
		[TestMethod]
		public void LIST_TICKET_BY_PRIORITY()
		{
			var result = _ticketServices.ListTicketResponse(new ListTicketRequest
			{
				PriorityId = new int[] { 1 }
			}).Result;

			Assert.AreEqual(1, result.Count);
		}

		//filter by status id 3 & 2
		[TestMethod]
		public void LIST_TICKET_BY_STATUS()
		{
			var result = _ticketServices.ListTicketResponse(new ListTicketRequest
			{
				StatusId = new int[] { 2, 3 }
			}).Result;

			Assert.AreEqual(2, result.Count);
		}

		//filter by raised by 1
		[TestMethod]
		public void LIST_TICKET_BY_RAISEDBY()
		{
			var result = _ticketServices.ListTicketResponse(new ListTicketRequest
			{
				RaisedBy = 1
			}).Result;

			Assert.AreEqual(3, result.Count);
		}


		//all filter 1
		[TestMethod]
		public void LIST_TICKET_BY_ALL_FILTER_1()
		{
			var result = _ticketServices.ListTicketResponse(new ListTicketRequest
			{
				Summary = "three",
				ProductId = new int[] {1},
				CategoryId = new int[] { 1 },
				PriorityId = new int[] { 1 },
				StatusId = new int[] { 1 },
				RaisedBy = 1
			}).Result;

			Assert.AreEqual(0, result.Count);
		}


		//all filter 2
		[TestMethod]
		public void LIST_TICKET_BY_ALL_FILTER_2()
		{
			var result = _ticketServices.ListTicketResponse(new ListTicketRequest
			{
				Summary = "one",
				ProductId = new int[] { 1 },
				CategoryId = new int[] { 1 },
				PriorityId = new int[] { 1 },
				StatusId = new int[] { 1 },
				RaisedBy = 1
			}).Result;

			Assert.AreEqual(1, result.Count);
		}



		//all filter 3
		[TestMethod]
		public void LIST_TICKET_BY_ALL_FILTER_3()
		{
			var result = _ticketServices.ListTicketResponse(new ListTicketRequest
			{
				ProductId = new int[] { 1 },
				CategoryId = new int[] { 1 },
				PriorityId = new int[] { 1 },
				StatusId = new int[] { 1 },
				RaisedBy = 1
			}).Result;

			Assert.AreEqual(1, result.Count);
		}

	}
}
