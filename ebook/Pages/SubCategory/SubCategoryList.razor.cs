using ebook.Models.SubCategory;
using ebook.Services.Interface;
using ebook.Shared.Component;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace ebook.Pages.SubCategory
{
    public partial class SubCategoryList
    {
        private List<SubCategoryModel> subCategoryModel { get; set; } = new();
        [Inject] private ISubCategoryService subCategoryService { get; set; }

        private string searchString1 = "";

        protected override async Task OnInitializedAsync()
        {
            await GetSubCategory();
        }
        private async Task GetSubCategory()
        {
            subCategoryModel = await subCategoryService.getSubCategoryAsync();
        }

        private async Task AddSubCategory()
        {
            SubCategoryModel subCategory = new SubCategoryModel() { subcategory_id = 0, subcategory_name = "", description = "", category_id = 0 };
            await MenuDialog(subCategory);
        }

        private async Task EditSubCategory(SubCategoryModel subCategory)
        {
            if (subCategory.subcategory_id != null)
            {
                await MenuDialog(subCategory);
            }
        }

        private async Task DeleteSubCategory(int id)
        {
            var parameters = new DialogParameters();
            parameters.Add("ContentText", "Do you really want to delete Sub Category? This process cannot be undone.");
            parameters.Add("ButtonText", "Delete");
            parameters.Add("Color", Color.Error);

            var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

            var dialog = _dialogService.Show<Confirmation>("Delete", parameters, options);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                await subCategoryService.deleteSubCategoryAsync(id);
                await GetSubCategory();
                _snackBar.Add("SubCategory Deleted Successfully.", Severity.Success);
            }
        }

        private async Task MenuDialog(SubCategoryModel subCategory)
        {
            var dialogOptions = new DialogOptions() { MaxWidth = MaxWidth.Small, FullWidth = true, CloseButton = true };
            var parameters = new DialogParameters { ["subCategoryModel"] = subCategory };

            var dialog = _dialogService.Show<SubCategorySave>("Add SubCategory", parameters, dialogOptions);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                await GetSubCategory();
                _snackBar.Add("SubCategory Saved Successfully.", Severity.Success);
            }
        }

        private bool FilterFunc1(SubCategoryModel element) => FilterFunc(element, searchString1);
        private bool FilterFunc(SubCategoryModel element, string searchString)
        {
            if (string.IsNullOrWhiteSpace(searchString))
                return true;
            if (element.subcategory_name.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            else if (element.category_name.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            return false;
        }
    }
}
