using API.DTO;
using API.Helpers;
using API.Helpers.ValidationsHelper;
using AutoMapper;
using Core.DTO.Request;
using Core.DTO.Response;
using Core.Entities;
using Core.Interfaces.Services;
using eCommerce.API.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	[Authorize]
	public class TicketController : BaseApiController
	{
		private readonly IMapper _mapper;
		private readonly ITicketServices _ticketServices;
		private readonly IConfigurationService _configurationService;
        private readonly IChartService _chartService;
		private readonly TicketValidations _ticketValidations;
        private FileUploaderHelper _fileUploader;

        public TicketController(IMapper mapper,
			ITicketServices ticketServices,
			IConfigurationService configurationService,
			IChartService chartService
		)
		{

            _fileUploader = new FileUploaderHelper(AppContext.BaseDirectory + "/images");
            _mapper = mapper;
			_ticketServices = ticketServices;
			_configurationService = configurationService;
            _chartService = chartService;
            _ticketValidations = new TicketValidations(_configurationService);

		}

		[HttpPost]
		public async Task<ActionResult> AddTicket([FromForm] AddTicketRequest ticket)
		{
			var result = await _ticketServices.AddTicket(ticket);
			if (result == null)
				return ApiResponseHelpers.BadRequest(ticket);

			if(ticket.Attachments != null && ticket.Attachments.Count > 0)
			{
                var upload = await UploadFile(ticket.Attachments);
                var uploadResult = await _ticketServices.UploadFile(upload, result.TicketId);
            }

            var mappedResult = _mapper.Map<Ticket, TicketDTO>(result);
			return Ok(mappedResult);
		}

		[HttpPut]
		public async Task<ActionResult> UpdateTicket(EditTicketRequest ticket)
		{
			var result = await _ticketServices.Edit(ticket);
			if (result == false) return ApiResponseHelpers.BadRequest(ticket);

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

		[HttpGet]
		public async Task<ActionResult<List<ListTicketResponse>>> GetTickets([FromQuery] ListTicketRequest request)
		{
			return await _ticketServices.ListTicketResponse(request);
		}


		[HttpGet("statussummary")]
		public async Task<ActionResult<List<StatusSummaryResponse>>> GetStatusSummary()
		{
			return await _chartService.GetStatusSummaryResponses();
		}



		[HttpGet("myticket")]
		public async Task<ActionResult<List<ListTicketResponse>>> GetMyTickets()
		{
			return await _ticketServices.ListOfMyTickets();
		}


        private async Task<List<Attachment>> UploadFile(List<IFormFile> iformfiles)
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
                    Filename = item.ServerFileName,
                });
            }

            return attachments;
        }
    }
}
