using Microsoft.EntityFrameworkCore;
using Post.Application.Repositories;
using Post.Common.Helpers;
using Post.Common.Models;
using Post.Domain.Entities;
using Post.Infrastructure.Data;
using System.Linq;


namespace Post.Infrastructure.Repository
{
    public class PostRepository : GenricRepository<Domain.Entities.Post>, IPostRepository
    {
        public PostRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        public async Task<List<Domain.Entities.Post>> GetLatestPosts(QueryParameters parameters)
        {

            var query = _context.Set<Domain.Entities.Post>().AsQueryable();

            if (!string.IsNullOrWhiteSpace(parameters.SearchTerm))
            {
                query = query.Where(p => p.description.Contains(parameters.SearchTerm));
            }

            if (!string.IsNullOrWhiteSpace(parameters.SortBy))
            {
                query = query.OrderBy(r => r.CreatedAt.ToString() == parameters.SortBy);
            }

            query = query.ApplyPaging(parameters);

            return await query.Include(q => q.User).ToListAsync();
        }
     

        public async Task<List<Domain.Entities.Post>> GetUserPosts(Guid userId)
        {
          return await _context.Set<Domain.Entities.Post>().Where(p => p.UserId == userId).Include(p => p.User).ToListAsync();
        }
    }
}
