using ebook.Models.Category;
using ebook.Services.Interface;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace ebook.Pages.Category
{
    public partial class CategorySave
    {
        [Parameter] public CategoryModel? categoryModel { get; set; }
        [Inject] private ICategoryService categoryService { get; set; }
        [CascadingParameter] MudDialogInstance MudDialog { get; set; }

        private async Task Save()
        {

            if (categoryModel.category_name == "")
            {
                _snackBar.Add("Category Name Required!", Severity.Error);
            }
            else
            {
                var Message = await categoryService.saveCategoryAsync(categoryModel);
                MudDialog.Close(DialogResult.Ok(true));
            }
        }
    }
}
