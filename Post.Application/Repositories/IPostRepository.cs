using Post.Common.Models;
using Post.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Application.Repositories
{
    public interface IPostRepository : IGenricRepository<Domain.Entities.Post>
    {

        public Task<List<Domain.Entities.Post>> GetUserPosts(Guid userId);
        public Task<List<Domain.Entities.Post>> GetLatestPosts(QueryParameters parameters);

    }
}
