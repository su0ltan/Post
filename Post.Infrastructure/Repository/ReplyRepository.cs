using Microsoft.EntityFrameworkCore;
using Post.Application.Repositories;
using Post.Domain.Entities;
using Post.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Infrastructure.Repository
{
    public class ReplyRepository : GenricRepository<Reply>, IReplyRepository
    {
        public ReplyRepository(AppDbContext context)
            : base(context)
        {
        }

        public async Task<List<Reply>> GetUserReplies(Guid userId)
        {
            return await _context
                         .Set<Reply>()
                         .Where(r => r.UserId == userId)
                         .Include(r => r.User)
                         .ToListAsync()
                         .ConfigureAwait(false);
        }
    }
}
