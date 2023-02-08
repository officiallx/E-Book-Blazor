using ebook.Models;
using ebook.Models.SubCategory;

namespace ebook.Services.Interface
{
    public interface ISubCategoryService
    {
        Task<MessageResponse> saveSubCategoryAsync(SubCategoryModel subCategory);
        Task<List<SubCategoryModel>> getSubCategoryAsync();
        Task deleteSubCategoryAsync(long Id);
    }
}
