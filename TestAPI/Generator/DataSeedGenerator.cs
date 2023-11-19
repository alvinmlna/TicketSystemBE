using BusinessLogic.Services;
using Core.DTO.Request;
using Core.Entities;
using Core.Interfaces.Services;
using DataAccess.Repository;
using System.Net.Sockets;
using TestAPI.Helpers;

namespace TestAPI.Generator
{
	[TestClass]
	public class DataSeedGenerator
	{
		static UnitOfWork _unitOfWork = UnitOfWorkHelpers.GetActualSqlServer();
		private readonly ITicketServices _ticketServices = new TicketService(_unitOfWork, null);

		[TestMethod]
        [Ignore]
        public void Generate()
		{
			//Generate Ticket
			Random rand = new Random();
			RandomDateTime date = new RandomDateTime();

			for (int i = 0; i < 100; i++)
			{
				var raisedDate = date.Next();
                Ticket newTicket = new Ticket()
				{
					UserId = 1,
					ProductId = rand.Next(1, 4),
					CategoryId = rand.Next(1, 4),
					PriorityId = rand.Next(1, 4),
					Summary = "Sample summary",
					Description = "Sample Description",
					RaisedDate = raisedDate,
					StatusId = rand.Next(0, 3),
				};

				int SLA =  GetSLA(newTicket.PriorityId);
				newTicket.ExpectedDate = raisedDate.AddHours(SLA);
				_unitOfWork.TicketRepository.Add(newTicket);
			}
			_unitOfWork.TicketRepository.SaveChanges();
		}


		private int GetSLA(int priorityId)
		{
			switch (priorityId)
			{
				case 1:
					return 240;
				case 2:
					return 120;
				case 3:
					return 4;
				default:
					return 0;
			}
		}
	}

	class RandomDateTime
	{
		DateTime start = DateTime.Now.AddMonths(-12);
		Random gen;
		int range;

		public RandomDateTime()
		{
			gen = new Random();
			range = (DateTime.Now - start).Days;
		}

		public DateTime Next()
		{
			return start.AddDays(gen.Next(range)).AddHours(gen.Next(0, 24)).AddMinutes(gen.Next(0, 60)).AddSeconds(gen.Next(0, 60));
		}
	}
}
