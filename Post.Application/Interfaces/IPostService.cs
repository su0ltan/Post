using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Post.Common.DTOs.Post;
using Post.Common.Models;

namespace Post.Application.Interfaces
{
    public interface IPostService
    {
        Task<bool> AddAsync(AddPostDto dto);
        Task<List<PostDto>> GetUserPostsAsync(Guid userId);
        Task<bool> RemoveAsync(Guid postId);
        Task<bool> UpdateAsync(PostDto dto);
        Task<List<PostDto>> GetLatestPosts(QueryParameters parameters);
    }
}
