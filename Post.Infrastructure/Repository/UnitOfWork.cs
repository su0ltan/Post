using Microsoft.EntityFrameworkCore;
using Post.Application.Repositories;
using Post.Domain.Shared;
using Post.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Post.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private readonly Dictionary<Type, object> _repositories = new();
        private readonly IPostRepository _postRepository;
        private readonly IReplyRepository _replyRepo;

        public UnitOfWork(AppDbContext context, IPostRepository postRepository, IReplyRepository replyRepo)
        {
            _context = context;
            _postRepository = postRepository;
            _replyRepo = replyRepo;
        }

        public IPostRepository PostRepository => _postRepository;
        public IReplyRepository ReplyRepository => _replyRepo;

        public void Dispose()
        {
            _context.Dispose();
        }

        public IGenricRepository<T> Repository<T>() where T : class
        {
            if (!_repositories.ContainsKey(typeof(T)))
            {
                _repositories[typeof(T)] = new GenricRepository<T>(_context);
            }

            return (IGenricRepository<T>)_repositories[typeof(T)];
        }

        public Task Rollback()
        {
            throw new NotImplementedException();
        }

        public async Task<int> Save(CancellationToken cancellationToken)
        {
          await _context.SaveChangesAsync(cancellationToken);
            return 1;
        }

        public Task<int> SaveAndRemoveCache(CancellationToken cancellationToken, params string[] cacheKeys)
        {
            throw new NotImplementedException();
        }

        public async Task<int> SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
            return 1;
        }
    }
}
