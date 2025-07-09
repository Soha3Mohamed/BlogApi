using System.Runtime.CompilerServices;
using UserPostApi.Models.Data;
using UserPostApi.Services.Implementation;
using UserPostApi.Services.Interfaces;

namespace UserPostApi.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<ICommentService, CommentService>();
            return services;
        }

    }
}
