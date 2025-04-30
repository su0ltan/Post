using Post.Common.DTOs.Reply;

namespace Post.Application.Interfaces
{
    public interface IReplyService
    {
        Task<bool> AddAsync(AddReplyDto dto);
        Task<List<ReplyDto>> GetUserRepliesAsync(Guid userId);
        Task<bool> RemoveAsync(Guid replyId);
        Task<bool> UpdateAsync(ReplyDto dto);
        Task<List<ReplyDto>> GetPostRepliesAsync(Guid postId);
    }
}
