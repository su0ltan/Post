using System;
using Post.Common.DTOs.Reply;
using Post.Domain.Entities;

namespace Post.Application.Mappers
{
    public static class ReplyMapper
    {
        public static ReplyDto Map(Reply reply)
        {
            return new ReplyDto
            {
                Id = reply.Id,
                UserId = reply.UserId,
                PostId = reply.PostId,
                replyContent = reply.replyContent,
                UserName = reply.User.UserName,
            };
        }

        public static Reply Map(this ReplyDto dto)
        {
            return new Reply
            {
                Id = dto.Id,
                UserId = dto.UserId,
                PostId = dto.PostId,
                replyContent = dto.replyContent,
                CreatedAt = DateTime.Now,
            };
        }


        public static Reply Map(this AddReplyDto dto)
        {
            return new Reply
            {
                UserId = dto.UserId,
                PostId = dto.PostId,
                replyContent = dto.replyContent,
                CreatedAt = DateTime.Now,
            };
        }
    }
}
