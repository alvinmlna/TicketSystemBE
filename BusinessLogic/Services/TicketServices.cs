using Core.Entities;
using Core.Interfaces.Repository;
using Core.Interfaces.Services;

namespace BusinessLogic.Services
{
	public class TicketServices : ITicketServices
	{
		private readonly IUnitOfWork _unitOfWork;

		public TicketServices(IUnitOfWork unitOfWork)
        {
			_unitOfWork = unitOfWork;
		}

        public async Task<bool> AddTicket(Ticket ticket)
		{
			ticket.RaisedDate = DateTime.Now;

			_unitOfWork.Repository<Ticket>().Add(ticket);

			int result = await _unitOfWork.SaveChanges();
			if (result > 0)
				return true;

			return false;
		}
	}
}
