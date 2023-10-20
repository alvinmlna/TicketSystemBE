using API.DTO;
using API.Helpers;
using AutoMapper;
using Core.Entities;
using Core.Interfaces.Services;
using eCommerce.API.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    public class DiscussionController : BaseApiController
    {
        private readonly IDiscussionService _discussionService;
        private readonly IMapper _mapper;

        public DiscussionController(IDiscussionService discussionService,
            IMapper mapper)
        {
            _discussionService = discussionService;
            _mapper = mapper;
        }

        [HttpGet("ticket/{ticketId}")]
        public async Task<ActionResult<IReadOnlyList<Discussion>>> GetDiscussionByTicket(int ticketId)
        {
            var result = await _discussionService.GetDiscussionByTicketId(ticketId);
            return Ok(result);
        }

        [HttpGet("{discussionId}")]
        public async Task<ActionResult<IReadOnlyList<Discussion>>> FindDiscussion(int discussionId)
        {
            var result = await _discussionService.GetDiscussionById(discussionId);
            if (result == null) return ApiResponseHelpers.NotFound(discussionId);

            return Ok(result);
        }


        [HttpPost]
        public async Task<ActionResult> Add(DiscussionDTO discussion)
        {
            var mappedResult = _mapper.Map<DiscussionDTO, Discussion>(discussion);
            if (mappedResult == null) return ApiResponseHelpers.BadRequest(discussion);

            var result = await _discussionService.Add(mappedResult);
            if (result == false) return ApiResponseHelpers.BadRequest(discussion);

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
    }
}
