using Microsoft.AspNetCore.Mvc;
using Post.Application.Interfaces;
using Post.Application.Logging;
using Post.Common.DTOs.Post;
using Post.Common.Models;

namespace Post.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PostController : ControllerBase
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


        [HttpGet("GetPosts")]
        public async Task<IActionResult> GetPosts(Guid userId)
        {
            var result = await _postService.GetUserPostsAsync(userId);
            return Ok(ApiResponse<List<PostDto>>.SuccessResponse(result, "Posts Retrieved Successfully"));
        }



    }
}
