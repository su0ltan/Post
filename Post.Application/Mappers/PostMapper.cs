using Post.Common.DTOs.Post;
using Post.Domain.Entities;


namespace Post.Application.Mappers
{
    public static class PostMapper
    {
        public static PostDto Map(Domain.Entities.Post post)
        {
            return new PostDto
            {
                Id = post.Id,
                UserId = post.UserId,
                topic = post.topic,
                description = post.description,
                UserName = post.User.UserName
            };
        }

        public static Domain.Entities.Post Map(this PostDto postDto)
        {
            return new Domain.Entities.Post
            {
                Id = postDto.Id,
                UserId = postDto.UserId,
                topic = postDto.topic,
                description = postDto.description,
                CreatedAt = DateTime.Now,
            };
        }

        public static Domain.Entities.Post Map(this AddPostDto postDto)
        {
            return new Domain.Entities.Post
            {
                UserId = postDto.UserId,
                topic = postDto.topic,
                description = postDto.description,
                CreatedAt = DateTime.Now,
            };
        }
    }
}
