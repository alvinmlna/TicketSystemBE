using API.Helpers;
using Core.Entities;
using Core.Interfaces.Services;
using eCommerce.API.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	public class UploadController : BaseApiController
	{
		private FileUploaderHelper _fileUploader;
		private readonly ITicketServices _ticketServices;

		public UploadController(ITicketServices ticketServices)
		{
			_fileUploader = new FileUploaderHelper(AppContext.BaseDirectory + "/images");
			_ticketServices = ticketServices;
		}

        [HttpPost("{ticketid}"), DisableRequestSizeLimit]
		public async Task<IActionResult> UploadById(int ticketid)
		{
			var formCollection = await Request.ReadFormAsync();
			var files = formCollection;
			var ticketId = formCollection.Keys;

			var attachments = await UploadFile(files);
			if (attachments == null) return ApiResponseHelpers.BadRequest(files);

			//submit file to service
			var result = await _ticketServices.UploadFile(attachments, ticketid);
			if (result == false)
				return ApiResponseHelpers.BadRequest(null);

			return Ok();
		}

		[HttpPost, DisableRequestSizeLimit]
		public async Task<IActionResult> Upload()
		{
			var formCollection = await Request.ReadFormAsync();
			var files = formCollection;
			var ticketId = formCollection.Keys;

			var attachments = await UploadFile(files);

			if (attachments == null) return ApiResponseHelpers.BadRequest(files);

			//submit file to service
			//var result = await _ticketServices.UploadFile(attachments, 2007);
			//if (result == false)
			//	return ApiResponseHelpers.ActionFailed(null);

			return Ok();
		}

		private async Task<List<Attachment>> UploadFile(IFormCollection iformfiles)
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
