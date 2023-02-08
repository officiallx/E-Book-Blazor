using ebook.Repository.Impl;
using ebook.Repository.Interface;
using ebook.Services.Impl;
using ebook.Services.Interface;

namespace ebook.Configuration
{
    public static class ServiceConfigurationExtensions
    {
        internal static IServiceCollection AddCustomServices(this IServiceCollection services)
        {
            services.AddTransient<ICommonService, CommonService>().
            AddTransient<ICommonRepository, CommonRepository>();
            services.AddTransient<IAuthorService, AuthorService>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<ISubCategoryService, SubCategoryService>();
            services.AddTransient<IBookService, BookService>();
            return services;
        }
    }
}
