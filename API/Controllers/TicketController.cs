using API.DTO;
using AutoMapper;
using Core.Entities;
using Core.Interfaces.Services;
using eCommerce.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
	public class TicketController : BaseApiController
	{
		private readonly IMapper _mapper;
		private readonly ITicketServices _ticketServices;

		private string WebRootPath = "G:\\Portofolio\\TicketManagementSystem\\BackEnd\\TicketManagement\\API\\";

		public TicketController(IMapper mapper, ITicketServices ticketServices)
        {
			_mapper = mapper;
			_ticketServices = ticketServices;
		}

        [HttpGet]
		public async Task<ActionResult> AddTicket([FromForm] TicketDTO ticket)
		{
			var ticketData = _mapper.Map<TicketDTO, Ticket>(ticket);
			var result = await _ticketServices.AddTicket(ticketData);

			if (ticket.Attachments != null)
			{
				foreach (var item in ticket.Attachments)
				{

					if (item.FileName == null || item.FileName.Length == 0)
					{
						return Content("File not selected");
					}
					var path = Path.Combine(WebRootPath, "Images/", item.FileName);

					using (FileStream stream = new FileStream(path, FileMode.Create))
					{
						await item.CopyToAsync(stream);
						stream.Close();

					}
				}
			}


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
