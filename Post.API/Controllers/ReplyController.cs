using Microsoft.AspNetCore.Mvc;
using Post.Application.Interfaces;
using Post.Application.Logging;
using Post.Common.DTOs.Reply;
using Post.Common.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Post.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReplyController : ControllerBase
    {
        private readonly IAppLogger<ReplyDto> _logger;
        private readonly IReplyService _replyService;

        public ReplyController(
            IAppLogger<ReplyDto> logger,
            IReplyService replyService)
        {
            _logger = logger;
            _replyService = replyService;
        }

        [HttpPost("AddReply")]
        public async Task<IActionResult> AddReply(AddReplyDto dto)
        {
            var success = await _replyService.AddAsync(dto);
            if (!success)
                return BadRequest(ApiResponse<ReplyDto>.ErrorResponse("Failed to add reply"));

            return Ok(ApiResponse<AddReplyDto>.SuccessResponse(dto, "Reply added successfully"));
        }

        [HttpGet("GetUserReplies")]
        public async Task<IActionResult> GetUserReplies(Guid userId)
        {
            var replies = await _replyService.GetUserRepliesAsync(userId);
            return Ok(ApiResponse<List<ReplyDto>>.SuccessResponse(
                replies,
                $"Replies for user {userId} retrieved successfully"
            ));
        }

     /*   [HttpGet("GetRepliesByPost")]
        public async Task<IActionResult> GetRepliesByPost(Guid postId)
        {
            var replies = await _replyService.GetRepliesByPostAsync(postId);
            return Ok(ApiResponse<List<ReplyDto>>.SuccessResponse(
                replies,
                $"Replies for post {postId} retrieved successfully"
            ));
        }*/
    }
}
