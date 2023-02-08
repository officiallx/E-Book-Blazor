using ebook.Models.Author;
using ebook.Services.Impl;
using ebook.Services.Interface;
using ebook.Shared.Component;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace ebook.Pages.Author
{
    partial class AuthorList
    {
        private List<AuthorModel> authorModel { get; set; } = new();
        [Inject] private IAuthorService authorService { get; set; }
        private string searchString1 = "";
        protected override async Task OnInitializedAsync()
        {
            await GetAuthor();
        }
        private async Task GetAuthor()
        {
            authorModel = await authorService.getAuthorAsync();
        }

        private async Task AddAuthor()
        {
            AuthorModel author = new AuthorModel() { author_id = 0, author_name = "" };
            await MenuDialog(author);
        }

        private async Task EditAuthor(AuthorModel author)
        {
            if (author.author_id != null)
            {
                await MenuDialog(author);
            }
        }

        private async Task DeleteAuthor(int id)
        {
            var parameters = new DialogParameters();
            parameters.Add("ContentText", "Do you really want to delete Publisher? This process cannot be undone.");
            parameters.Add("ButtonText", "Delete");
            parameters.Add("Color", Color.Error);

            var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

            var dialog = _dialogService.Show<Confirmation>("Delete", parameters, options);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                await authorService.deleteAuthorAsync(id);
                await GetAuthor();
                _snackBar.Add("Author Deleted Successfully.", Severity.Success);
            }
        }

        private async Task MenuDialog(AuthorModel author)
        {
            var dialogOptions = new DialogOptions() { MaxWidth = MaxWidth.Small, FullWidth = true, CloseButton = true };
            var parameters = new DialogParameters { ["authorModel"] = author };

            var dialog = _dialogService.Show<AuthorSave>("Add Author", parameters, dialogOptions);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                await GetAuthor();
                _snackBar.Add("Author Saved Successfully.", Severity.Success);
            }
        }
        private bool FilterFunc1(AuthorModel element) => FilterFunc(element, searchString1);
        private bool FilterFunc(AuthorModel element, string searchString)
        {
            if (string.IsNullOrWhiteSpace(searchString))
                return true;
            if (element.author_name.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            return false;
        }
    }
}
