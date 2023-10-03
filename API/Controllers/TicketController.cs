using API.DTO;
using AutoMapper;
using Core.Entities;
using Core.Interfaces.Services;
using eCommerce.API.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	public class TicketController : BaseApiController
	{
		private readonly IMapper _mapper;
		private readonly ITicketServices _ticketServices;

		public TicketController(IMapper mapper, ITicketServices ticketServices)
        {
			_mapper = mapper;
			_ticketServices = ticketServices;
		}

        [HttpGet]
		public async Task<ActionResult> AddTicket(TicketDTO ticket)
		{
			var ticketData = _mapper.Map<TicketDTO, Ticket>(ticket);
			var result = await _ticketServices.AddTicket(ticketData);

			if (result == true)
			{
				return Ok(ticket);
			} 
				else
			{
				return BadRequest(ticket);
			}
		}
	}
}
