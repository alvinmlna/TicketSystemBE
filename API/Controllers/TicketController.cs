using API.DTO;
using API.Helpers;
using API.Helpers.ValidationsHelper;
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
		private readonly IConfigurationService _configurationService;
		private FileUploaderHelper _fileUploader;

		private readonly TicketValidations _ticketValidations;

		public TicketController(IMapper mapper, ITicketServices ticketServices, IConfigurationService configurationService)
        {
			_mapper = mapper;
			_ticketServices = ticketServices;
			_configurationService = configurationService;
			_ticketValidations = new TicketValidations(_configurationService);

			_fileUploader = new FileUploaderHelper("G:\\Portofolio\\TicketManagementSystem\\BackEnd\\TicketManagement\\API\\Images\\");
		}

        [HttpGet]
		public async Task<ActionResult> AddTicket([FromForm] TicketDTO ticket)
		{
			//Validate
			var validationResult = _ticketValidations.Validate(ticket);
			if (!validationResult.IsValid)
				return ApiResponseHelpers.ValidationError(validationResult);

			throw new Exception("ayam");

			var ticketData = _mapper.Map<TicketDTO, Ticket>(ticket);
			var result = await _ticketServices.AddTicket(ticketData);
			await _fileUploader.UploadFile(ticket.Attachments);

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
