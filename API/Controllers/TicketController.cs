﻿using API.DTO;
using API.Helpers;
using API.Helpers.Validations;
using AutoMapper;
using BusinessLogic.Services;
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
		private readonly IConfigurationService _configurationService;
		private FileUploaderHelper _fileUploader;

		public TicketController(IMapper mapper, ITicketServices ticketServices, IConfigurationService configurationService)
        {
			_mapper = mapper;
			_ticketServices = ticketServices;
			_configurationService = configurationService;
			_fileUploader = new FileUploaderHelper("G:\\Portofolio\\TicketManagementSystem\\BackEnd\\TicketManagement\\API\\Images\\");
		}

        [HttpGet]
		public async Task<ActionResult> AddTicket([FromForm] TicketDTO ticket)
		{
			//Validate
			var ticketValidation = new TicketValidations(_configurationService);
			var isValid = ticketValidation.Validate(ticket);


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
