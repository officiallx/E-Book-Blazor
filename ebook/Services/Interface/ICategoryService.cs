using ebook.Models;
using ebook.Models.Category;

namespace ebook.Services.Interface
{
    public interface ICategoryService
    {
        Task<MessageResponse> saveCategoryAsync(CategoryModel category);
        Task<List<CategoryModel>> getCategoryAsync();
        Task deleteCategoryAsync(long Id);
    }
}
