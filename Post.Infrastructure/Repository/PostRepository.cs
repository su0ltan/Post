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
    public class PostRepository : GenricRepository<Post1>, IPostRepository
    {
        public PostRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        public async Task<List<Post1>> GetUserPosts(Guid userId)
        {
          return await _context.Set<Post1>().Where(x => x.UserId == userId).ToListAsync();
        }
    }
}
