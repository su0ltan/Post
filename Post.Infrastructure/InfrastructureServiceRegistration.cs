using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Post.Application.Logging;
using Post.Application.Repositories;
using Post.Infrastructure.Data;
using Post.Infrastructure.Logging;
using Post.Infrastructure.Repository;

namespace Post.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {

        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                 options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IGenricRepository<>), typeof(GenricRepository<>));
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IReplyRepository, ReplyRepository>();


            services.AddSingleton(typeof(IAppLogger<>), typeof(LoggerAdapter<>));


            return services;
        }
    }
}
