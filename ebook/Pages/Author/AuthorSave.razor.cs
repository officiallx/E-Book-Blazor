using ebook.Models.Author;
using ebook.Services.Impl;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace ebook.Pages.Author
{
    public partial class AuthorSave
    {
        [Parameter] public AuthorModel? authorModel { get; set; }
        [Inject] private IAuthorService authorService { get; set; }
        [CascadingParameter] MudDialogInstance MudDialog { get; set; }

        private async Task Save()
        {

            if (authorModel.author_name == "")
            {
                _snackBar.Add("Author Name Required!", Severity.Error);
            }
            else
            {
                var Message = await authorService.saveAuthorAsync(authorModel);
                MudDialog.Close(DialogResult.Ok(true));
            }
        }
    }
}
