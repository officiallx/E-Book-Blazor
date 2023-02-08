using ebook.Models.Category;
using ebook.Services.Interface;
using ebook.Shared.Component;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace ebook.Pages.Category
{
    partial class CategoryList
    {
        private List<CategoryModel> categoryModel { get; set; } = new();
        [Inject] private ICategoryService categoryService { get; set; }
        private string searchString1 = "";

        protected override async Task OnInitializedAsync()
        {
            await GetCategory();
        }
        private async Task GetCategory()
        {
            categoryModel = await categoryService.getCategoryAsync();
        }

        private async Task AddCategory()
        {
            CategoryModel category = new CategoryModel() { category_id = 0, category_name = "", description = "" };
            await MenuDialog(category);
        }

        private async Task EditCategory(CategoryModel category)
        {
            if (category.category_id != null)
            {
                await MenuDialog(category);
            }
        }

        private async Task DeleteCategory(int id)
        {
            var parameters = new DialogParameters();
            parameters.Add("ContentText", "Do you really want to delete Category? This process cannot be undone.");
            parameters.Add("ButtonText", "Delete");
            parameters.Add("Color", Color.Error);

            var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

            var dialog = _dialogService.Show<Confirmation>("Delete", parameters, options);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                await categoryService.deleteCategoryAsync(id);
                await GetCategory();
                _snackBar.Add("Category Deleted Successfully.", Severity.Success);
            }
        }

        private async Task MenuDialog(CategoryModel category)
        {
            var dialogOptions = new DialogOptions() { MaxWidth = MaxWidth.Small, FullWidth = true, CloseButton = true };
            var parameters = new DialogParameters { ["categoryModel"] = category };

            var dialog = _dialogService.Show<CategorySave>("Add Category", parameters, dialogOptions);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                await GetCategory();
                _snackBar.Add("Category Saved Successfully.", Severity.Success);
            }
        }
        private bool FilterFunc1(CategoryModel element) => FilterFunc(element, searchString1);
        private bool FilterFunc(CategoryModel element, string searchString)
        {
            if (string.IsNullOrWhiteSpace(searchString))
                return true;
            if (element.category_name.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            return false;
        }
    }
}
