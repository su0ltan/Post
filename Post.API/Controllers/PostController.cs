using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Post.Application.Interfaces;
using Post.Application.Logging;
using Post.Common.DTOs.Post;
using Post.Common.Models;

namespace Post.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class PostController : BaseApiController
    {

        private readonly IAppLogger<PostDto> _logger;
        private readonly IPostService _postService;

        public PostController(IAppLogger<PostDto> _logger, IPostService _postRepository)
        {
            this._postService = _postRepository;
            this._logger = _logger;
        }


        [HttpPost("AddPost")]
        public async Task<IActionResult> AddPost(AddPostDto post)
        {
            var result = await _postService.AddAsync(post);
            return Ok(ApiResponse<AddPostDto>.SuccessResponse(post, "Post Added Successfully"));
        }


        [HttpGet("GetUserPosts/{userId}")]
        public async Task<IActionResult> GetUserPosts(Guid userId)
        {
            var result = await _postService.GetUserPostsAsync(userId);
            return Ok(ApiResponse<List<PostDto>>.SuccessResponse(result, "Posts Retrieved Successfully"));
        }

        [HttpGet("GetLatestPosts")]
        public async Task<IActionResult> GetLatestPosts([FromQuery] QueryParameters parameters)
        {
            var dtos = await _postService.GetLatestPosts(parameters);
            return Ok(ApiResponse<List<PostDto>>.SuccessResponse(dtos, "Success retrieving data"));
        }



    }
}
