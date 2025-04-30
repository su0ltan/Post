using Post.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Application.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IGenricRepository<T> Repository<T>() where T : class;

        IPostRepository PostRepository { get; }
        IReplyRepository ReplyRepository { get; }

        Task<int> Save(CancellationToken cancellationToken);
        Task<int> SaveChangesAsync();
        Task<int> SaveAndRemoveCache(CancellationToken cancellationToken, params string[] cacheKeys);
        Task Rollback();
    }
}
