using Post.Common.DTOs.Reply;
using Post.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Application.Repositories
{
    public interface IReplyRepository : IGenricRepository<Reply>
    {
        Task<List<Reply>> GetUserReplies(Guid userId);
        Task<List<Reply>> GetPostReplies(Guid postId);
    }
}
