using ebook.Models;
using ebook.Models.SubCategory;
using ebook.Repository;
using ebook.Services.Interface;
using System.Data.SqlClient;

namespace ebook.Services.Impl
{
    public class SubCategoryService : DapperRepoBase, ISubCategoryService
    {
        private readonly IConfiguration _configuration;
        public SubCategoryService(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
        }

        public async Task deleteSubCategoryAsync(long Id)
        {
            await ExecuteAsync("exec spSubCategory @Flag='D',@SubCategoryId=@SubCategoryId", new { SubCategoryId = Id });
        }

        public async Task<List<SubCategoryModel>> getSubCategoryAsync()
        {
            var response = await this.GetQueryResultAsync<SubCategoryModel>("exec spSubCategory @Flag=@Flag", new { Flag = 'G' });
            return response;
        }

        public async Task<MessageResponse> saveSubCategoryAsync(SubCategoryModel subCategory)
        {
            string Message = "";
            try
            {
                if (subCategory.subcategory_id == 0)
                {
                    var response = await this.GetQueryFirstOrDefaultResultAsync<MessageResponse>("exec spSubCategory @Flag='S', @SubCategoryName = @SubCategoryName, @CategoryId=@CategoryId, @Description=@Description",
                       new { SubCategoryName = subCategory.subcategory_name, CategoryId = subCategory.category_id, Description = subCategory.description });
                    return response;
                }
                else
                {
                    var response = await this.GetQueryFirstOrDefaultResultAsync<MessageResponse>("exec spSubCategory @Flag='U', @SubCategoryName = @SubCategoryName, @SubCategoryId = @SubCategoryId, @CategoryId=@CategoryId,@Description=@Description",
                       new { SubCategoryName = subCategory.subcategory_name, SubCategoryId = subCategory.subcategory_id, CategoryId = subCategory.category_id, Description = subCategory.description });
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
