using ebook.Models;
using ebook.Models.Category;
using ebook.Repository;
using ebook.Services.Interface;
using System.Data.SqlClient;

namespace ebook.Services.Impl
{
    public class CategoryService : DapperRepoBase, ICategoryService
    {
        private readonly IConfiguration _configuration;
        public CategoryService(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
        }

        public async Task deleteCategoryAsync(long Id)
        {
            await ExecuteAsync("exec spCategory @Flag='D',@CategoryId=@CategoryId", new { CategoryId = Id });
        }

        public async Task<List<CategoryModel>> getCategoryAsync()
        {
            var response = await this.GetQueryResultAsync<CategoryModel>("exec spCategory @Flag=@Flag", new { Flag = 'G' });
            return response;
        }

        public async Task<MessageResponse> saveCategoryAsync(CategoryModel category)
        {
            string Message = "";
            try
            {
                if (category.category_id == 0)
                {
                    var response = await this.GetQueryFirstOrDefaultResultAsync<MessageResponse>("exec spCategory @Flag='S', @CategoryName = @CategoryName, @Description=@Description",
                       new { CategoryName = category.category_name, Description = category.description });
                    return response;
                }
                else
                {
                    var response = await this.GetQueryFirstOrDefaultResultAsync<MessageResponse>("exec spCategory @Flag='U', @CategoryName = @CategoryName,@Description=@Description, @CategoryId = @CategoryId",
                       new { CategoryName = category.category_name, Description = category.description, CategoryId = category.category_id });
                    return response;
                }

            }
            catch (SqlException ex)
            {
                MessageResponse message = new();
                message.Message = ex.Message;
                return message;
            }
        }
    }
}
