using API.DTO;
using API.Helpers;
using AutoMapper;
using BusinessLogic.Services;
using Core.Entities;
using Core.Interfaces.Services;
using eCommerce.API.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Sockets;

namespace API.Controllers
{
    [Authorize]
    public class DiscussionController : BaseApiController
    {
        private readonly IDiscussionService _discussionService;
        private readonly IMapper _mapper;
        private FileUploaderHelper _fileUploader;

        public DiscussionController(IDiscussionService discussionService,
            IMapper mapper)
        {
            _discussionService = discussionService;
            _mapper = mapper;
            _fileUploader = new FileUploaderHelper(AppContext.BaseDirectory + "/images");
        }

        [HttpGet("ticket/{ticketId}")]
        public async Task<ActionResult<List<DiscussionDTO>>> GetDiscussionByTicket(int ticketId)
        {
            var result = await _discussionService.GetDiscussionByTicketId(ticketId);

            var mappedResult = _mapper.Map< IReadOnlyList<Discussion>, List<DiscussionDTO>>(result);

            return Ok(mappedResult);
        }

        [HttpGet("{discussionId}")]
        public async Task<ActionResult<IReadOnlyList<Discussion>>> FindDiscussion(int discussionId)
        {
            var result = await _discussionService.GetDiscussionById(discussionId);
            if (result == null) return ApiResponseHelpers.NotFound(discussionId);

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromForm] DiscussionDTO discussion)
        {
            var mappedResult = _mapper.Map<DiscussionDTO, Discussion>(discussion);
            if (mappedResult == null) return ApiResponseHelpers.BadRequest(discussion);

            var result = await _discussionService.Add(mappedResult);
            if (result == null) return ApiResponseHelpers.BadRequest(discussion);

            if (discussion.Attachments != null && discussion.Attachments.Count > 0)
            {
                var upload = await UploadFile(discussion.Attachments);
                var uploadResult = await _discussionService.UploadFile(upload, result.DiscussionId);
            }
            return Ok(discussion);
        }

        [HttpPut]
        public async Task<ActionResult> Edit(DiscussionDTO discussion)
        {
            var mappedResult = _mapper.Map<DiscussionDTO, Discussion>(discussion);
            if (mappedResult == null) return ApiResponseHelpers.BadRequest(discussion);

            var result = await _discussionService.Edit(mappedResult);
            if (result == false) return ApiResponseHelpers.BadRequest(discussion);

            return Ok(discussion);
        }

        [HttpDelete("{discussionId}")]
        public async Task<ActionResult> Delete(int discussionId)
        {
            var result = await _discussionService.Delete(discussionId);
            if (result == false) return ApiResponseHelpers.BadRequest(discussionId);
            return Ok(discussionId);
        }

        private async Task<List<DiscussionAttachment>> UploadFile(List<IFormFile> iformfiles)
        {
            var fileUploadResult = await _fileUploader.UploadFile(iformfiles);
            if (!fileUploadResult.Status)
            {
            }

            List<DiscussionAttachment> attachments = new List<DiscussionAttachment>();

            foreach (var item in fileUploadResult.FileResults)
            {
                attachments.Add(new DiscussionAttachment
                {
                    Filename = item.Filename,
                    ServerFileName = item.ServerFileName,
                    FileSize = item.FileSize,
                });
            }

            return attachments;
        }
    }
}
