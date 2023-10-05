using API.DTO;
using API.Helpers;
using API.Helpers.ValidationsHelper;
using AutoMapper;
using Core.Constants;
using Core.Entities;
using Core.Interfaces.Services;
using eCommerce.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Sockets;

namespace API.Controllers
{
	public class TicketController : BaseApiController
	{
		private readonly IMapper _mapper;
		private readonly ITicketServices _ticketServices;
		private readonly IConfigurationService _configurationService;
		private readonly ILoggingService _log;
		private FileUploaderHelper _fileUploader;

		private readonly TicketValidations _ticketValidations;

		public TicketController(IMapper mapper, 
			ITicketServices ticketServices, 
			IConfigurationService configurationService,
			ILoggingService log
			)
		{
			_mapper = mapper;
			_ticketServices = ticketServices;
			_configurationService = configurationService;
			_log = log;
			_ticketValidations = new TicketValidations(_configurationService);

			_fileUploader = new FileUploaderHelper(AppContext.BaseDirectory + "/images");
		}

        [HttpPost]
		public async Task<ActionResult> AddTicket([FromForm] TicketDTO ticket)
		{
			//Validate
			var validationResult = _ticketValidations.Validate(ticket);
			if (!validationResult.IsValid)
				return ApiResponseHelpers.ValidationError(validationResult);

			var ticketData = _mapper.Map<TicketDTO, Ticket>(ticket);

			//Upload files and map to attachments class
			var attachments = await UploadFile(ticket.Attachments);

			var result = await _ticketServices.AddTicket(ticketData, attachments);
			if (result == false) 
				return ApiResponseHelpers.ActionFailed(ticket);

			return Ok(ticket);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<TicketDTO>> GetTicket(int id)
		{
			var ticket = await _ticketServices.GetTicketById(id);
			if (ticket == null) return NotFound(id);

			var ticketDTO = _mapper.Map<Ticket, TicketDTO>(ticket);
			return Ok(ticketDTO);
		}



		private async Task<List<Attachment>> UploadFile(List<IFormFile>? iformfiles)
		{
			var fileUploadResult = await _fileUploader.UploadFile(iformfiles);
			if (!fileUploadResult.Status)
			{
			}

			List<Attachment> attachments = new List<Attachment>();

			foreach (var item in fileUploadResult.FileResults)
			{
				attachments.Add(new Attachment
				{
					Filename = item.NewFileName,
				});
			}

			return attachments;
		}
	}
}
