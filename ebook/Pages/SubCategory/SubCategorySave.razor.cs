using ebook.Models.SubCategory;
using ebook.Services.Interface;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace ebook.Pages.SubCategory
{
    public partial class SubCategorySave
    {
        [Parameter] public SubCategoryModel? subCategoryModel { get; set; }
        [Inject] private ISubCategoryService subCategoryService { get; set; }
        [CascadingParameter] MudDialogInstance MudDialog { get; set; }

        private async Task Save()
        {

            if (subCategoryModel.subcategory_name == "")
            {
                _snackBar.Add("SubCategory Name Required!", Severity.Error);
            }
            else
            {
                var Message = await subCategoryService.saveSubCategoryAsync(subCategoryModel);
                MudDialog.Close(DialogResult.Ok(true));
            }
        }
    }
}
