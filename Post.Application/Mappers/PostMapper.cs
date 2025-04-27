using Post.Common.DTOs.Post;
using Post.Domain.Entities;


namespace Post.Application.Mappers
{
    public static class PostMapper
    {
        public static PostDto Map(Post1 post)
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

        public static Post1 Map(this PostDto postDto)
        {
            return new Post1
            {
                Id = postDto.Id,
                UserId = postDto.UserId,
                topic = postDto.topic,
                description = postDto.description,
                CreatedAt = DateTime.Now,
            };
        }

        public static Post1 Map(this AddPostDto postDto)
        {
            return new Post1
            {
                UserId = postDto.UserId,
                topic = postDto.topic,
                description = postDto.description,
                CreatedAt = DateTime.Now,
            };
        }
    }
}
